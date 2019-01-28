using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueManager : MonoBehaviour {
	public int MaxNumberOfTongues;

	private Queue<GameObject> ActiveTongues;

	public void RegisterNewTongue(GameObject NewTongue)
	{
		if (MaxNumberOfTongues < 0)
		{
			return;
		}
		
		ActiveTongues.Enqueue(NewTongue);
		if (ActiveTongues.Count > MaxNumberOfTongues)
		{
			GameObject TongueToKill = ActiveTongues.Dequeue();
			TongueToKill.GetComponent<TongueCreater>().DetachFromCeiling();
		}
	}

	void Awake()
	{
		ActiveTongues = new Queue<GameObject>();
	}

	// Destroy everything that enters the trigger
   	void OnTriggerEnter2D(Collider2D collider)
   	{
		GameObject.Destroy(collider.gameObject.transform.parent.gameObject);
	}
}
