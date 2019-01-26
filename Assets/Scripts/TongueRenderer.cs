using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TongueRenderer : MonoBehaviour
{
	public List<Transform> targets;

	private LineRenderer _lineRenderer = null;

	private Vector3[] _tonguePoints;
	private int _targetSlack = 10;

	// Public interface

	public void ResetTargets()
	{
		targets.Clear();
	}

	public void SetTargets(List<Transform> newTargets)
	{
		ResetTargets();
		foreach (Transform target in newTargets)
		{
			targets.Add(target);
		}
		_tonguePoints = new Vector3[targets.Count];
	}

	// Private interface

	private void UpdateTonguePositions()
	{
		_lineRenderer.positionCount = targets.Count;
		for (int index = 0; index < targets.Count; ++index)
		{
			_tonguePoints[index] -= (_tonguePoints[index] - targets[index].position) * 0.5f;
			_lineRenderer.SetPosition(index, _tonguePoints[index]);
		}
	}

	// Unity overrides

	void Awake()
	{
		targets = new List<Transform>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		UpdateTonguePositions();
	}

}
