using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogComponent : MonoBehaviour
{
	enum TongueAttachmentType { NearestPointOnRay };

	public GameObject TonguePrefab;
	public FrogMouthAnimation FrogMouthInstance;

	[SerializeField] private TongueAttachmentType CurrentTongueAttachmentType;
	[SerializeField] private float DragScale;
	[SerializeField] private HingeJoint2D Hinge;

	private GameObject Tongue;
	private Rigidbody2D _rigidbody;
	private TongueAttacher TongueAttacherInstance;
	private TongueManager TongueManagerInstance;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		switch (CurrentTongueAttachmentType)
		{
			case TongueAttachmentType.NearestPointOnRay:
				TongueAttacherInstance = gameObject.AddComponent<TongueAttacher_NearestPointOnRay>();
				break;
			default:
				Debug.LogError("Invalid TongueAttachmentType selection");
				break;
		}
		if (TongueAttacherInstance)
		{
			TongueAttacherInstance.TonguePrefab = TonguePrefab;
		}

		TongueManagerInstance = GameObject.Find("TongueKillerInstance").GetComponent<TongueManager>();

		Tongue = null;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!Hinge.enabled && Input.GetMouseButtonDown(0))
		{
			AttachTongue();
		}
		else if (Hinge.enabled && Input.GetMouseButtonDown(1))
		{
			DetachTongue();
		}
	}

	private void FixedUpdate()
	{
		if (Hinge.enabled && Input.GetMouseButton(0))
		{
			HandleSwing();
		}
	}

	void HandleSwing()
	{
		float ForceX = DragScale * Input.GetAxis("Mouse X");
		_rigidbody.AddForce(new Vector2(ForceX, 0.0f));
	}

	void AttachTongue()
	{
		Tongue = TongueAttacherInstance.AttachTongue(Hinge);
		if (Tongue != null)
		{
			FrogMouthInstance.SetMouthOpenValue(1f);
			TongueManagerInstance.RegisterNewTongue(Tongue);
		}
	}

	void DetachTongue()
	{
		Hinge.enabled = false;
		FrogMouthInstance.SetMouthOpenValue(0f);
		Tongue.GetComponent<TongueRenderer>().RemoveFirstTarget();
	}
}
