using UnityEngine;
using System.Collections;

public class DestoryByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
