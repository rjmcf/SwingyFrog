using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
	public float DragScale;

	// Update is called once per frame
	void Update ()
	{
		HingeJoint2D Hinge = gameObject.GetComponent<HingeJoint2D>();
		if (Hinge.enabled && Input.GetMouseButton(0))
		{
			float ForceX = DragScale * Input.GetAxis("Mouse X");
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ForceX, 0.0f));
		}
	}
}
