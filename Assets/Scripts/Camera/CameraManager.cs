using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	[SerializeField] private Transform TargetTransform;
	[SerializeField] private float CameraMoveSpeed;

	// Percentage of screen that is outside the deadzone rect.
	// Values between 0 and 1.
	// 0,0 is Bottom Left of screen
	[SerializeField] private float LeftXPercent;
	[SerializeField] private float RightXPercent;
	[SerializeField] private float TopYPercent;
	[SerializeField] private float BottomYPercent;

	private Camera ThisCamera;

	void Start()
	{
		ThisCamera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		Vector2 ViewportCoordsOfTarget = ThisCamera.WorldToViewportPoint(TargetTransform.position);
		Vector3 Offset = new Vector3();
		if (ViewportCoordsOfTarget.x < LeftXPercent)
		{
			Offset.x = -CameraMoveSpeed * (LeftXPercent - ViewportCoordsOfTarget.x) / LeftXPercent;
		}
		else if (ViewportCoordsOfTarget.x > (1 - RightXPercent))
		{
			Offset.x = CameraMoveSpeed * (ViewportCoordsOfTarget.x - (1 - RightXPercent)) / RightXPercent;
		}

		if (ViewportCoordsOfTarget.y < BottomYPercent)
		{
			Offset.y = -CameraMoveSpeed * (BottomYPercent - ViewportCoordsOfTarget.y) / BottomYPercent;
		}
		else if (ViewportCoordsOfTarget.y > (1 - TopYPercent))
		{
			Offset.y = CameraMoveSpeed * (ViewportCoordsOfTarget.y - (1 - RightXPercent)) / TopYPercent;
		}

		transform.position += Offset;
	}
}
