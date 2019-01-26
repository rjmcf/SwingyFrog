using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TongueRenderer : MonoBehaviour
{

	[SerializeField] private float _animateSpeed = 1.0f;

	public List<Transform> targets;

	private LineRenderer _lineRenderer = null;

	private List<Vector3> _tonguePoints;

	// Public interface

	public void ResetTargets()
	{
		targets.Clear();
	}

	public void RemoveFirstTarget()
	{
		targets.RemoveAt(0);
		_tonguePoints.RemoveAt(0);
	}

	public void SetTargets(List<Transform> newTargets)
	{
		ResetTargets();
		foreach (Transform target in newTargets)
		{
			targets.Add(target);
			
			// start from the beginning for all points
			_tonguePoints.Add(newTargets[0].position);
		}
	}

	// Private interface

	private void UpdateTonguePositions()
	{
		_lineRenderer.positionCount = targets.Count;
		float step = 1.0f / targets.Count;
		for (int index = 0; index < targets.Count; ++index)
		{
			_tonguePoints[index] -= (_tonguePoints[index] - targets[index].position) * _animateSpeed * (1.2f - (index*step));
			_lineRenderer.SetPosition(index, _tonguePoints[index]);
		}
	}

	// Unity overrides

	void Awake()
	{
		targets = new List<Transform>();
		_tonguePoints = new List<Vector3>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		UpdateTonguePositions();
	}

}
