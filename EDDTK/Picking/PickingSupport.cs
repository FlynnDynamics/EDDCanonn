using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using EDDTK.Maths;
using EDDTK.Plot3D.Primitives;
using EDDTK.Plot3D.Text;
using EDDTK.Plot3D.Rendering.Scene;
using EDDTK.Plot3D.Rendering.View;
using EDDTK.Plot3D.Rendering.View.Modes;
using EDDTK.Plot3D.Transform;
using EDDTK.Glut;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace EDDTK.Picking
{
	/// <summary>
	/// @see: http://www.opengl.org/resources/faq/technical/selection.htm
	/// 
	/// @author Martin Pernollet
	/// 
	/// </summary>
	public class PickingSupport
	{
		public static int BRUSH_SIZE = 10;
		public static int BUFFER_SIZE = 2048;

		public PickingSupport() : this(BRUSH_SIZE)
		{
		}

		public PickingSupport(int brushSize) : this(brushSize, BUFFER_SIZE)
		{
		}

		public PickingSupport(int brushSize, int bufferSize)
		{
			this.brushSize = brushSize;
			this.bufferSize = bufferSize;
		}

		/// <summary>
		///********************** </summary>

		public virtual bool addObjectPickedListener(IObjectPickedListener listener)
		{
			return verticesListener.Add(listener);
		}

		public virtual bool removeObjectPickedListener(IObjectPickedListener listener)
		{
			return verticesListener.Remove(listener);
		}

		protected internal virtual void fireObjectPicked<T1>(IList<T1> v) where T1 : object
		{
			foreach (IObjectPickedListener listener in verticesListener)
			{
				listener.objectPicked(v, this);
			}
		}

		/// <summary>
		///********************** </summary>

		public virtual void registerDrawableObject(AbstractDrawable drawable, object model)
		{
			if (drawable is Pickable)
			{
				registerPickableObject((Pickable)drawable, model);
			}
		}

		public virtual void registerPickableObject(Pickable pickable, object model)
		{
			lock (this)
			{
				pickable.PickingId = pickId++;
				pickables[pickable.PickingId] = pickable;
				pickableTargets[pickable] = model;
			}
		}

		public virtual void getPickableObject(int id)
		{
			lock (this)
			{
				pickables[id];
			}
		}

		/// <summary>
		///********************** </summary>

		protected internal TicToc perf = new TicToc();

		public virtual void pickObjects(GL gl, GL glu, View view, Graph graph, IntegerCoord2d pickPoint)
		{
			perf.tic();

			int[] viewport = new int[4];
			int[] selectBuf = new int[bufferSize]; // TODO: move @ construction
			IntBuffer selectBuffer = Buffers.newDirectIntBuffer(bufferSize);


			if (!gl.GL2)
			{
				throw new System.NotSupportedException();
			}

			// Prepare selection data
			GL.GetInteger(GL.GL_VIEWPORT, viewport, 0);
			GL.SelectBuffer(bufferSize, selectBuffer);
			GL.RenderMode(GL.GL_SELECT);
			GL.InitNames();
			GL.PushName(0);

			// Retrieve view settings
			Camera camera = view.Camera;
			CameraMode cMode = view.CameraMode;
			Coord3d viewScaling = view.LastViewScaling;
			Transform viewTransform = new Transform(new Plot3D.Transform.Scale(viewScaling));
			double xpick = pickPoint.x;
			double ypick = pickPoint.y;

			// Setup projection matrix
			GL.MatrixMode(GLMatrixFunc.GL_PROJECTION);
			GL.PushMatrix();
			{
				GL.LoadIdentity();
				// Setup picking matrix, and update view frustrum
				GL.gluPickMatrix(xpick, ypick, brushSize, brushSize, viewport, 0);
				camera.doShoot(cMode);

				// Draw each pickable element in select buffer
				GL.MatrixMode(GLMatrixFunc.GL_MODELVIEW);

				lock (this)
				{
					foreach (Pickable pickable in pickables.Values)
					{
						setCurrentName(gl, pickable);
						pickable.Transform = viewTransform;
						pickable.draw(gl, glu, camera);
						releaseCurrentName(gl);
					}
				}
				// Back to projection matrix
				GL.MatrixMode(GLMatrixFunc.GL_PROJECTION);
			}
			GL.PopMatrix();
			GL.Flush();

			// Process hits
			int hits = GL.RenderMode(GL2.GL_RENDER);
			selectBuffer.get(selectBuf);
			IList<Pickable> picked = processHits(hits, selectBuf);

			// Trigger an event
			IList<object> clickedObjects = new List<object>(hits);
			foreach (Pickable pickable in picked)
			{
				object vertex = pickableTargets[pickable];
				clickedObjects.Add(vertex);
			}
			perf.toc();

			fireObjectPicked(clickedObjects);
		}

		public virtual double LastPickPerfMs
		{
			get
			{
				return perf.elapsedMilisecond();
			}
		}

		protected internal virtual void setCurrentName(GL gl, Pickable pickable)
		{
			if (method == 0)
			{
				GL.LoadName(pickable.PickingId);
			}
			else
			{
				GL.PushName(pickable.PickingId);
			}
		}

		protected internal virtual void releaseCurrentName(GL gl)
		{
			if (method == 0)
			{
				;
			}
			else
			{
				GL.PopName();
			}
		}

		protected internal static int method = 0;

		/// <summary>
		///****************** </summary>

		/// <summary>
		/// Provides the number of picked object by a click. </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unused") protected java.util.List<org.jzy3d.plot3d.primitives.pickable.Pickable> processHits(int hits, int buffer[])
		protected internal virtual IList<Pickable> processHits(int hits, int[] buffer)
		{
			int names, ptr = 0;
			int z1, z2 = 0;

			IList<Pickable> picked = new List<Pickable>();

			for (int i = 0; i < hits; i++)
			{
				names = buffer[ptr];
				ptr++;
				z1 = buffer[ptr];
				ptr++;
				z2 = buffer[ptr];
				ptr++;

				for (int j = 0; j < names; j++)
				{
					int idj = buffer[ptr];
					ptr++;
					if (!pickables.ContainsKey(idj))
					{
						throw new Exception("internal error: pickable id not found in registry!");
					}
					picked.Add(pickables[idj]);
				}
			}
			return picked;
		}

		  public virtual void unRegisterAllPickableObjects()
		  {
			  lock (this)
			  {
				  pickables.Clear();
				  pickableTargets.Clear();
			  }
		  }

		/// <summary>
		///****************** </summary>

		protected internal static int pickId = 0;
		protected internal IDictionary<int, Pickable> pickables = new Dictionary<int, Pickable>();
		protected internal IList<IObjectPickedListener> verticesListener = new List<IObjectPickedListener>(1);
		protected internal IDictionary<Pickable, object> pickableTargets = new Dictionary<Pickable, object>();
		protected internal int brushSize;
		protected internal int bufferSize;
	}

}