using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TongueAttacher : MonoBehaviour {
	public GameObject TonguePrefab;

	protected Transform TongueOrigin;
	protected LayerMask GeoMask;

	void Start()
	{
		 GeoMask = LayerMask.GetMask("Geometry");
		 TongueOrigin = transform.Find("TongueOrigin").transform;
	}

	public abstract bool AttachTongue(HingeJoint2D Hinge, ref GameObject Tongue);
}
