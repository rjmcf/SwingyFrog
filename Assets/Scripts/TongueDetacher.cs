using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueDetacher : MonoBehaviour
{
	public FrogMouthAnimation FrogMouthInstance;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(1))
		{
			HingeJoint2D Hinge = gameObject.GetComponent<HingeJoint2D>();
			Rigidbody2D Link = Hinge.connectedBody;
			Transform CurrentTransform = GetComponent<Transform>();
			GameObject LeftWeight = new GameObject();
			LeftWeight.GetComponent<Transform>().position = CurrentTransform.position;
			Rigidbody2D WeightRigidBody = LeftWeight.AddComponent<Rigidbody2D>();
			WeightRigidBody.mass = 20;
			HingeJoint2D WeightHingeJoint = LeftWeight.AddComponent<HingeJoint2D>();
			WeightHingeJoint.connectedBody = Link;
			Hinge.enabled = false;
			FrogMouthInstance.SetMouthOpenValue(0f);
		}
	}
}
