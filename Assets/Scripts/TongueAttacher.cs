using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttacher : MonoBehaviour
{
	public GameObject TonguePrefab;
	public FrogMouthAnimation FrogMouthInstance;

	// Update is called once per frame
	void Update ()
	{
		HingeJoint2D Hinge = gameObject.GetComponent<HingeJoint2D>();
		if (!Hinge.enabled && Input.GetMouseButtonDown(0))
		{
			Vector3 FrogPos = gameObject.transform.position;
			Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameObject NewTongue = GameObject.Instantiate(TonguePrefab);
			TongueCreater TongueCreaterScript = NewTongue.GetComponent<TongueCreater>();
			TongueCreaterScript.CreateTongue(Hinge.transform, new Vector2(ClickPos.x, ClickPos.y));
			Hinge.enabled = true;
			Hinge.connectedBody = TongueCreaterScript.TongueAttachRigidbody;
			FrogMouthInstance.SetMouthOpenValue(1f);
		}
	}
}
