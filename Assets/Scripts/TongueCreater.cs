using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCreater : MonoBehaviour
{
	public Rigidbody2D TongueAttachRigidbody;
	public List<Vector2> SegmentLocations;
	public Rigidbody2D Ceiling;

	[SerializeField] private GameObject ChainLinkOriginPrefab;
	[SerializeField] private float SegmentLength;

	public void CreateTongue(Vector2 FrogPoint, Vector2 AttachPoint)
	{
		Debug.Log("Creating Tongue");
		int Multiplier = AttachPoint.x > FrogPoint.x ? -1 : 1;
		Vector2 SurfaceToFrog = FrogPoint - AttachPoint;
		Vector2 SurfaceToFrogDir = SurfaceToFrog.normalized;
		float NumSegments = SurfaceToFrog.magnitude / SegmentLength;
		SegmentLocations = new List<Vector2>();
		for (int i = 0; i < NumSegments-1; i++)
		{
			SegmentLocations.Add(AttachPoint + SurfaceToFrogDir * SegmentLength * i);
		}
		GameObject LastSegment = null;
		GameObject CurrentSegment = null;
		foreach (Vector2 SegmentLocation in SegmentLocations)
		{
			float CosAngleToRotate = Vector2.Dot(Vector2.up, -SurfaceToFrogDir);
			CurrentSegment = GameObject.Instantiate(ChainLinkOriginPrefab, SegmentLocation, Quaternion.identity);
			CurrentSegment.transform.eulerAngles = new Vector3(0f, 0f, Multiplier * Mathf.Rad2Deg * Mathf.Acos(CosAngleToRotate));
			if (LastSegment)
			{
				CurrentSegment.GetComponentInChildren<HingeJoint2D>().connectedBody = LastSegment.GetComponentInChildren<Rigidbody2D>();
			}
			LastSegment = CurrentSegment;
		}
		TongueAttachRigidbody = CurrentSegment.GetComponentInChildren<Rigidbody2D>();
	}
}
