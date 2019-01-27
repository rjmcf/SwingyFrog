using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueManager : MonoBehaviour {

	// Destroy everything that enters the trigger
   void OnTriggerEnter2D(Collider2D collider)
   {
	   Debug.Log("Collider Entered");
	   Debug.Log(gameObject);
   }
}
