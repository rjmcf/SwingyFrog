using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChainLinkComponent : MonoBehaviour {

	private HingeJoint2D _hingeJoint = null;
	private Rigidbody2D _rigidbody = null;

	public HingeJoint2D HingeJoint
	{
		get
		{
			return _hingeJoint;
		}
	}

	public Rigidbody2D Rigidbody
	{
		get
		{
			return _rigidbody;
		}
	}

	private void Awake()
	{
		_hingeJoint = GetComponent<HingeJoint2D>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

}
