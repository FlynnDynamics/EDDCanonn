//////////////////////////////////////////////////////////////////////////
//
// EDDTK - EDDiscovery
// 
// ScatterPoint: Add a point to a scatter chart
//
//////////////////////////////////////////////////////////////////////////


using EDDTK.Colors;
using EDDTK.Events;
using EDDTK.Maths;
using EDDTK.Plot3D.Rendering.View;
using OpenTK.Graphics.OpenGL;

namespace EDDTK.Plot3D.Primitives
{
    public class ScatterPoint : AbstractDrawable, ISingleColorable
    {

        private Color[] _colors;
        private Color _color;
        private Coord3d _coordinates;
        private float _width;

        public ScatterPoint()
        {
            _bbox = new BoundingBox3d();
            Width = 1;
            Color = Color.RED;
        }

        public ScatterPoint(Coord3d coordinates) :
                this(coordinates, Color.RED)
        {
        }

        public ScatterPoint(Coord3d coordinates, Color rgb, float width = 1)
        {
            _bbox = new BoundingBox3d();
            Data = coordinates;
            Width = width;
            Color = rgb;
        }

        public ScatterPoint(Coord3d coordinates, Color[] colors, float width = 1)
        {
            _bbox = new BoundingBox3d();
            Data = coordinates;
            Width = width;
            Colors = colors;
        }

        public void Clear()
        {
            _coordinates = null;
            _bbox.reset();
        }

        public override void Draw(Camera cam)
        {

            _transform?.Execute();

            GL.PointSize(_width);
            GL.Begin(BeginMode.Points);
            if (_colors == null)
            {
                GL.Color4(_color.r, _color.g, _color.b, _color.a);
            }

            if (_coordinates != null)
            {
                int k = 0;
                if ((_colors != null))
                {
                    GL.Color4(_colors[k].r, _colors[k].g, _colors[k].b, _colors[k].a);
                    k++;
                }

                GL.Vertex3(_coordinates.x, _coordinates.y, _coordinates.z);
            }
            GL.End();

            // doDrawBounds (MISSING)

        }

        public override Transform.Transform Transform
        {
            get => _transform;
            set
            {
                _transform = value;
                UpdateBounds();
            }
        }

        private void UpdateBounds()
        {
            _bbox.reset();
            if (_coordinates != null)
            {
                _bbox.add(_coordinates);
            }
        }

        public Coord3d Data
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                UpdateBounds();
            }
        }

        private Color[] Colors
        {
            get => _colors;
            set
            {
                _colors = value;
                fireDrawableChanged(new DrawableChangedEventArgs(this, DrawableChangedEventArgs.FieldChanged.Color));
            }
        }

        private float Width
        {
            get => _width;
            set => _width = value;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }
    }
}