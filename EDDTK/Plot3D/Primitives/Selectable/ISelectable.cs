
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;
using EDDTK.Colors;
using EDDTK.Events;
using EDDTK.Maths;
using EDDTK.Plot3D.Rendering.View;
using EDDTK.Plot3D.Transform;

namespace EDDTK.Plot3D.Primitives
{
	public interface ISelectable
	{

		void Project(Camera cam);

		List<Coord3d> LastProjection { get; }
	}
}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
