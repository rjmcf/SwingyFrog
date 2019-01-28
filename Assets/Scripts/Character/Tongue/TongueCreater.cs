using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCreater : MonoBehaviour
{
	public Rigidbody2D TongueAttachRigidbody;
	public List<Transform> TongueSegments;

	[SerializeField] private ChainLinkComponent ChainLinkPrefab;
	[SerializeField] private float SegmentLength;

	private List<GameObject> TongueObjects;
	private HingeJoint2D CeilingAttachPoint;
	private int FallingTongueLayer;

	void Awake()
	{
		FallingTongueLayer = LayerMask.NameToLayer("FallingTongue");
	}

	public void CreateTongue(Transform FrogTransform, Vector2 AttachPoint)
	{
		Vector2 FrogPoint = FrogTransform.position;
		int Multiplier = AttachPoint.x > FrogPoint.x ? -1 : 1;
		Vector2 SurfaceToFrog = FrogPoint - AttachPoint;
		Vector2 SurfaceToFrogDir = SurfaceToFrog.normalized;
		float NumSegments = SurfaceToFrog.magnitude / SegmentLength;
		float CosAngleToRotate = Vector2.Dot(Vector2.up, -SurfaceToFrogDir);

		List<Vector2> SegmentLocations = new List<Vector2>();
		for (int i = 0; i < NumSegments - 1; i++)
		{
			SegmentLocations.Add(AttachPoint + SurfaceToFrogDir * SegmentLength * i);
		}

		Rigidbody2D LastRigidbody = null;
		ChainLinkComponent CurrentSegment = null;
		TongueSegments = new List<Transform>();
		TongueObjects = new List<GameObject>();

		foreach (Vector2 SegmentLocation in SegmentLocations)
		{
			CurrentSegment = GameObject.Instantiate(ChainLinkPrefab, SegmentLocation, Quaternion.identity, transform);
			if (TongueSegments.Count == 0)
			{
				CeilingAttachPoint = CurrentSegment.HingeJoint;
			}
			TongueSegments.Add(CurrentSegment.transform);
			TongueObjects.Add(CurrentSegment.gameObject);
			CurrentSegment.transform.eulerAngles = new Vector3(0f, 0f, Multiplier * Mathf.Rad2Deg * Mathf.Acos(CosAngleToRotate));
			if (LastRigidbody)
			{
				CurrentSegment.HingeJoint.connectedBody = LastRigidbody;
			}
			LastRigidbody = CurrentSegment.Rigidbody;
		}

		TongueSegments.Add(FrogTransform);
		TongueSegments.Reverse();
		GetComponent<TongueRenderer>().SetTargets(TongueSegments);
		TongueAttachRigidbody = LastRigidbody;
	}

	public void DetachFromCeiling()
	{
		CeilingAttachPoint.enabled = false;
		foreach (GameObject TongueObject in TongueObjects)
		{
			TongueObject.layer = FallingTongueLayer;
		}
	}
}
