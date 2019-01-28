using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttacher_NearestPointOnRay : TongueAttacher {

	public override GameObject AttachTongue(HingeJoint2D Hinge)
	{
		Vector2 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 Origin = TongueOrigin.position;
		RaycastHit2D FirstHit = Physics2D.Raycast(Origin, ClickPos - Origin, Mathf.Infinity, GeoMask);
		if (FirstHit.collider == null)
		{
			return null;
		}

		Vector2 HitInViewportCoords = Camera.main.WorldToViewportPoint(FirstHit.point);
		if (HitInViewportCoords.x < 0 || HitInViewportCoords.x > 1 || HitInViewportCoords.y < 0 || HitInViewportCoords.y > 1)
		{
			return null;
		}

		GameObject Tongue = GameObject.Instantiate(TonguePrefab);
		TongueCreater TongueCreaterScript = Tongue.GetComponent<TongueCreater>();
		TongueCreaterScript.CreateTongue(TongueOrigin, FirstHit.point);
		Hinge.enabled = true;
		Hinge.connectedBody = TongueCreaterScript.TongueAttachRigidbody;
		return Tongue;
	}
}
