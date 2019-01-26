using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogComponent : MonoBehaviour
{
	public GameObject TonguePrefab;
	public FrogMouthAnimation FrogMouthInstance;

	[SerializeField] private float DragScale;
	[SerializeField] private HingeJoint2D Hinge;

	private GameObject Tongue;

	void Start()
	{
		Hinge = gameObject.GetComponent<HingeJoint2D>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!Hinge.enabled && Input.GetMouseButtonDown(0))
		{
			AttachTongue();
		}
		else if (Hinge.enabled && Input.GetMouseButton(0))
		{
			HandleSwing();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			DetachTongue();
		}
	}

	void HandleSwing()
	{
		float ForceX = DragScale * Input.GetAxis("Mouse X");
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ForceX, 0.0f));
	}

	void AttachTongue()
	{
		Vector3 FrogPos = gameObject.transform.position;
		Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Tongue = GameObject.Instantiate(TonguePrefab);
		TongueCreater TongueCreaterScript = Tongue.GetComponent<TongueCreater>();
		TongueCreaterScript.CreateTongue(Hinge.transform, new Vector2(ClickPos.x, ClickPos.y));
		Hinge.enabled = true;
		Hinge.connectedBody = TongueCreaterScript.TongueAttachRigidbody;
		FrogMouthInstance.SetMouthOpenValue(1f);
	}

	void DetachTongue()
	{
		Hinge.enabled = false;
		FrogMouthInstance.SetMouthOpenValue(0f);
		Tongue.GetComponent<TongueRenderer>().RemoveFirstTarget();
	}
}
