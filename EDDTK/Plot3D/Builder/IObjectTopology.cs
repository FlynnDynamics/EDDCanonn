
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using EDDTK.Maths;

namespace EDDTK.Plot3D.Builder
{

	public interface IObjectTopology<O>
	{
		Coord3d getCoord(O obj);
		string getXAxisLabel();
		string getYAxisLabel();
		string getZAxisLabel();
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
