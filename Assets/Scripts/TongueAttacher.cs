using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttacher : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		HingeJoint2D Hinge = gameObject.GetComponent<HingeJoint2D>();
		if (!Hinge.enabled && Input.GetMouseButtonDown(0))
		{
			Debug.Log("New Attachment");
		}
	}
}
