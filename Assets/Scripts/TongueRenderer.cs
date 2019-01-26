using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TongueRenderer : MonoBehaviour
{

	[SerializeField] private Transform[] _targets;

	private LineRenderer _lineRenderer = null;

	private Vector3[] _tonguePoints;
	private int _numTargets = 0;
	private int _targetSlack = 10;

	// Public interface

	public void ResetTargets()
	{
		for (int index = 0; index < _numTargets; ++index)
		{
			_targets[index] = null;
		}
		_numTargets = 0;
	}

	public void AddTarget(Transform target)
	{
		_targets[_numTargets++] = target;
	}

	// Private interface

	private void UpdateTonguePositions()
	{
		_lineRenderer.positionCount = _numTargets;
		for (int index = 0; index < _numTargets; ++index)
		{
			_tonguePoints[index] -= (_tonguePoints[index] - _targets[index].position) * 0.5f;
			_lineRenderer.SetPosition(index, _tonguePoints[index]);
		}
	}

	// Unity overrides

	void Awake()
	{
		_numTargets = 0;
		for (int index = 0; index < _targets.Length; ++index)
		{
			if (_targets[index] != null)
			{
				_numTargets++;
			}
		}
		_tonguePoints = new Vector3[_targetSlack];
		_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		UpdateTonguePositions();
	}

}
