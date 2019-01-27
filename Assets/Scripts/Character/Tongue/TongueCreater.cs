using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCreater : MonoBehaviour
{
	public Rigidbody2D TongueAttachRigidbody;
	public List<Transform> TongueSegments;
	public Rigidbody2D Ceiling;

	//[SerializeField] private GameObject ChainLinkOriginPrefab;
	[SerializeField] private ChainLinkComponent ChainLinkPrefab;
	[SerializeField] private float SegmentLength;

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

		foreach (Vector2 SegmentLocation in SegmentLocations)
		{
			CurrentSegment = GameObject.Instantiate(ChainLinkPrefab, SegmentLocation, Quaternion.identity, transform);
			TongueSegments.Add(CurrentSegment.transform);
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

	/*public void CreateTongue(Transform FrogTransform, Vector2 AttachPoint)
	{
		Vector2 FrogPoint = FrogTransform.position;
		int Multiplier = AttachPoint.x > FrogPoint.x ? -1 : 1;
		Vector2 SurfaceToFrog = FrogPoint - AttachPoint;
		Vector2 SurfaceToFrogDir = SurfaceToFrog.normalized;
		float NumSegments = SurfaceToFrog.magnitude / SegmentLength;
		List<Vector2> SegmentLocations = new List<Vector2>();
		for (int i = 0; i < NumSegments-1; i++)
		{
			SegmentLocations.Add(AttachPoint + SurfaceToFrogDir * SegmentLength * i);
		}
		GameObject LastSegment = null;
		GameObject CurrentSegment = null;
		TongueSegments = new List<Transform>();
		foreach (Vector2 SegmentLocation in SegmentLocations)
		{
			float CosAngleToRotate = Vector2.Dot(Vector2.up, -SurfaceToFrogDir);
			CurrentSegment = GameObject.Instantiate(ChainLinkOriginPrefab, SegmentLocation, Quaternion.identity, transform);
			TongueSegments.Add(CurrentSegment.GetComponentInChildren<HingeJoint2D>().transform);
			CurrentSegment.transform.eulerAngles = new Vector3(0f, 0f, Multiplier * Mathf.Rad2Deg * Mathf.Acos(CosAngleToRotate));
			if (LastSegment)
			{
				CurrentSegment.GetComponentInChildren<HingeJoint2D>().connectedBody = LastSegment.GetComponentInChildren<Rigidbody2D>();
			}
			LastSegment = CurrentSegment;
		}
		TongueSegments.Add(FrogTransform);
		TongueSegments.Reverse();
		GetComponent<TongueRenderer>().SetTargets(TongueSegments);
		TongueAttachRigidbody = CurrentSegment.GetComponentInChildren<Rigidbody2D>();
	}*/
}
