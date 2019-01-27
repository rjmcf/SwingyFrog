using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class FrogLimbComponent : MonoBehaviour
{

	[SerializeField] private ChainLinkComponent HingePrefab;

	[Range(3, 20)]
	[SerializeField] private int NumBones;
	[SerializeField] private float ArmLength;
	[SerializeField] private float ArmThickness;
	[SerializeField] private Sprite AppendageSprite;

	private Rigidbody2D _rigidbody;
	private LineRenderer _lineRenderer;

	private Transform[] _bones;
	private LayerMask _characterLayer;

	private void Awake()
	{
		_characterLayer = LayerMask.NameToLayer("Tongue");
		_rigidbody = GetComponent<Rigidbody2D>();
		_lineRenderer = GetComponent<LineRenderer>();

		BuildLimb();
	}

	private GameObject MakeChain(string name)
	{
		GameObject obj = new GameObject(name);
		obj.layer = _characterLayer;
		Rigidbody2D rigidbody = obj.AddComponent<Rigidbody2D>();
		rigidbody.mass = 2.0f;
		rigidbody.sharedMaterial = _rigidbody.sharedMaterial;

		BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
		collider.size = Vector2.one * (ArmLength / NumBones);

		HingeJoint2D hinge = obj.AddComponent<HingeJoint2D>();
		hinge.autoConfigureConnectedAnchor = false;
		hinge.anchor = Vector2.up * collider.size.y * 0.5f;
		hinge.connectedAnchor = Vector2.down * collider.size.y * 0.5f;
		hinge.enableCollision = true;

		return obj;
	}

	void BuildLimb()
	{
		_bones = new Transform[NumBones];
		Rigidbody2D previousBody = null;
		_lineRenderer.positionCount = NumBones;
		for (int boneIndex = 0; boneIndex < NumBones; ++boneIndex)
		{

			if (boneIndex == 0)
			{
				_bones[boneIndex] = transform;
				previousBody = _rigidbody;
			}
			else
			{
				GameObject chain = MakeChain("Limb_" + boneIndex.ToString());
				Debug.Log(chain.layer);
				HingeJoint2D hinge = chain.GetComponent<HingeJoint2D>();
				_bones[boneIndex] = chain.transform;

				hinge.connectedBody = previousBody;
				previousBody = chain.GetComponent<Rigidbody2D>();
				_bones[boneIndex].transform.parent = _bones[boneIndex - 1].transform;

				_bones[boneIndex].transform.localPosition = Vector3.zero;
				_bones[boneIndex].transform.localRotation = Quaternion.identity;

				if (boneIndex == NumBones-1)
				{
					SpriteRenderer spriteRenderer = chain.AddComponent<SpriteRenderer>();
					spriteRenderer.sprite = AppendageSprite;
				}
			}

			_lineRenderer.SetPosition(boneIndex, _bones[boneIndex].transform.position);
		}
	}

	private void Update()
	{
		for (int boneIndex = 0; boneIndex < NumBones; ++boneIndex)
		{
			_lineRenderer.SetPosition(boneIndex, _bones[boneIndex].position);
		}
	}

}
