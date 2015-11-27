using UnityEngine;
using System.Collections;

public class DestoryByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary")
			return;
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
