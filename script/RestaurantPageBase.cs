using UnityEngine;
using System.Collections;

abstract public class RestaurantPageBase : PageBase {

	protected MapRootRestaurant m_mapRoot;
	public MapRootRestaurant mapRoot{
		get{
			return m_mapRoot;
		}
	}
	public void Initialize( MapRootRestaurant _mapRoot ){
		m_mapRoot = _mapRoot;
		initialize ();
		PageEnd ();
	}
	public override void PageStart ()
	{
		gameObject.SetActive (true);
		base.PageStart ();
		pageStart ();
	}

	public override void PageEnd ()
	{
		base.PageEnd ();
		pageEnd ();
		gameObject.SetActive (false);
	}

	// こいつを呼ぶ
	abstract protected void initialize ();
	abstract protected void pageStart ();
	abstract protected void pageEnd ();

	

}
