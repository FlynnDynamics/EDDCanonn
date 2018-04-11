using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDDTK.Chart;
using EDDTK.Chart.Controllers.Thread.Camera;
using EDDTK.Colors;
using EDDTK.Colors.ColorMaps;
using EDDTK.Maths;
using EDDTK.Plot3D.Builder;
using EDDTK.Plot3D.Builder.Concrete;
using EDDTK.Plot3D.Primitives;
using EDDTK.Plot3D.Primitives.Axes.Layout;
using EDDTK.Plot3D.Rendering.Canvas;
using EDDTK.Plot3D.Rendering.View;

namespace eddtk
{
    public partial class demoForm : Form
    {
        private CameraThreadController t;
        private IAxeLayout axeLayout;

        public demoForm()
        {
            InitializeComponent();
            //InitRenderer();
        }

        class MyMapper : EDDTK.Plot3D.Builder.Mapper
        {

            public override double f(double x, double y)
            {
                return 10 * Math.Sin(x / 10) * Math.Cos(y / 20) * x;
            }

        }

        private void InitRenderer()
        {

            // Create the Renderer 3D control.
            //Renderer3D myRenderer3D = new Renderer3D();

            // Add the Renderer control to the panel
            // mainPanel.Controls.Clear();
            //mainPanel.Controls.Add(myRenderer3D);

            // Create a range for the graph generation
            Range range = new Range(-150, 150);
            int steps = 50;

            // Build a nice surface to display with cool alpha colors 
            // (alpha 0.8 for surface color and 0.5 for wireframe)
            Shape surface = Builder.buildOrthonomal(new OrthonormalGrid(range, steps, range, steps), new MyMapper());
            surface.ColorMapper = new ColorMapper(new ColorMapRainbow(), surface.Bounds.zmin, surface.Bounds.zmax, new EDDTK.Colors.Color(1, 1, 1, 0.8));
            surface.FaceDisplayed = true;
            surface.WireframeDisplayed = true;
            surface.WireframeColor = EDDTK.Colors.Color.CYAN;
            surface.WireframeColor.mul(new EDDTK.Colors.Color(1, 1, 1, 0.5));

            // Create the chart and embed the surface within
            Chart chart = new Chart(rendererEDDTK3D, Quality.Nicest);
            chart.Scene.Graph.Add(surface);
            axeLayout = chart.AxeLayout;

            // Create a mouse control
            EDDTK.Chart.Controllers.Mouse.Camera.CameraMouseController mouse = new EDDTK.Chart.Controllers.Mouse.Camera.CameraMouseController();
            mouse.addControllerEventListener(rendererEDDTK3D);
            chart.addController(mouse);

            // This is just to ensure code is reentrant (used when code is not called in Form_Load but another reentrant event)
            DisposeBackgroundThread();

            // Create a thread to control the camera based on mouse movements
            t = new EDDTK.Chart.Controllers.Thread.Camera.CameraThreadController();
            t.addControllerEventListener(rendererEDDTK3D);
            mouse.addSlaveThreadController(t);
            chart.addController(t);
            t.Start();

            // Associate the chart with current control
            rendererEDDTK3D.setView(chart.View);

            this.Refresh();
        }

        private void DisposeBackgroundThread()
        {
            if ((t != null))
            {
                t.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeBackgroundThread();
        }

        private void demoForm_Load_1(object sender, EventArgs e)
        {
            InitRenderer();
        }
    }
}
