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

	[Range(0.0f, 1.0f)]
	[SerializeField] private float _mouthOpenValue = 0.0f;

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
		foreach (MouthAnimationState state in _mouthAnimStates)
		{
			state.ObjectToAnimate.transform.localPosition = VectorLerp(state.MouthClosedLocalPosition, state.MouthOpenLocalPosition, _mouthOpenValue);
			state.ObjectToAnimate.transform.localEulerAngles = VectorLerp(state.MouthClosedLocalEulerAngles, state.MouthOpenLocalEulerAngles, _mouthOpenValue);
		}
	}

	Vector3 VectorLerp(Vector3 a, Vector3 b, float u)
	{
		return ((1 - u) * a) + (u * b);
	}


}
