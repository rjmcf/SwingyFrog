using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueManager : MonoBehaviour {

	// Destroy everything that enters the trigger
   void OnTriggerEnter(Collider other)
   {
	   Debug.Log("Collider Entered");
	   Debug.Log(gameObject);
   }
}
