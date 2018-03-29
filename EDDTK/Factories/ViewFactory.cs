
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using EDDTK.Plot3D.Rendering.View;
using EDDTK.Plot3D.Rendering.Scene;
using EDDTK.Plot3D.Rendering.Canvas;
using EDDTK.Chart;

namespace EDDTK.Factories
{

	public class ViewFactory
	{

		public static View getInstance(Scene scene, ICanvas canvas, Quality quality)
		{
			return new ChartView(scene, canvas, quality);
		}

	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
