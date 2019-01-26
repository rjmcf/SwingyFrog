using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MouthAnimationState
{
	[SerializeField] public GameObject ObjectToAnimate;
	[SerializeField] public Vector3 MouthOpenLocalPosition;
	[SerializeField] public Vector3 MouthClosedLocalPosition;
	[SerializeField] public Vector3 MouthOpenLocalEulerAngles;
	[SerializeField] public Vector3 MouthClosedLocalEulerAngles;
}

public class FrogMouthAnimation : MonoBehaviour {

	[SerializeField] private MouthAnimationState[] _mouthAnimStates;
	[SerializeField] private float _mouthAnimateSpeed;

	[Range(0.0f, 1.0f)]
	[SerializeField] private float _mouthOpenValue = 0.0f;

	private float _currentMouthOpenValue = 0.0f;

	public void SetMouthOpenValue(float NewValue)
	{
		if (NewValue != _mouthOpenValue)
		{
			_mouthOpenValue = NewValue;
		}
	}

	void OnValidate()
	{
		UpdateMouthAnimation();
	}

	void Awake()
	{
		_mouthOpenValue = 0.0f;
	}

	void Update()
	{
		UpdateMouthAnimation();
	}

	void UpdateMouthAnimation()
	{
		if (_currentMouthOpenValue != _mouthOpenValue)
		{
			if (_currentMouthOpenValue < _mouthOpenValue - 0.01f)
			{
				_currentMouthOpenValue += Time.deltaTime*_mouthAnimateSpeed;
			}
			else if (_currentMouthOpenValue > _mouthOpenValue + 0.01f)
			{
				_currentMouthOpenValue -= Time.deltaTime*_mouthAnimateSpeed;
			}
			else
			{
				_currentMouthOpenValue = _mouthOpenValue;
			}
		}
		foreach (MouthAnimationState state in _mouthAnimStates)
		{
			state.ObjectToAnimate.transform.localPosition = VectorLerp(state.MouthClosedLocalPosition, state.MouthOpenLocalPosition, _currentMouthOpenValue);
			state.ObjectToAnimate.transform.localEulerAngles = VectorLerp(state.MouthClosedLocalEulerAngles, state.MouthOpenLocalEulerAngles, _currentMouthOpenValue);
		}
	}

	Vector3 VectorLerp(Vector3 a, Vector3 b, float u)
	{
		return ((1 - u) * a) + (u * b);
	}


}
