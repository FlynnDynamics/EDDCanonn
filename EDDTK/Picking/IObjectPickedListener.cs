using System.Collections.Generic;

namespace EDDTK.Picking
{

	public interface IObjectPickedListener
	{
//ORIGINAL LINE: public void objectPicked(java.util.List<? extends Object> vertex, PickingSupport picking);
		void objectPicked<T1>(IList<T1> vertex, PickingSupport picking);
	}

}