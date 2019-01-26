using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttacher : MonoBehaviour
{
	public GameObject TonguePrefab;

	private TongueCreater TongueCreater;

	void Start()
	{
		GameObject TongueObject = GameObject.Instantiate(TonguePrefab, new Vector3(), Quaternion.identity);
		TongueCreater = TongueObject.GetComponent<TongueCreater>();
	}

	// Update is called once per frame
	void Update ()
	{
		HingeJoint2D Hinge = gameObject.GetComponent<HingeJoint2D>();
		if (!Hinge.enabled && Input.GetMouseButtonDown(0))
		{
			Vector3 FrogPos = gameObject.transform.position;
			Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			TongueCreater.CreateTongue(new Vector2(FrogPos.x, FrogPos.y) + Hinge.anchor, new Vector2(ClickPos.x, ClickPos.y));
			Hinge.enabled = true;
			Hinge.connectedBody = TongueCreater.TongueAttachRigidbody;
		}
	}
}
