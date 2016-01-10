using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin = -6.0f, xMax = 6.0f, zMin = -4.0f, zMax = 8.0f;
}

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float tilt = 4.0f;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5f;
	
	private Rigidbody rb;
	private AudioSource ad;
	private float nextFire = 0.0f;

	void Start() {
		rb = GetComponent<Rigidbody>();
		ad = GetComponent<AudioSource>();
	}

	void Update() {
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			if (Input.GetButton("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				ad.Play();
			}
		} else {
			if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				ad.Play();
			}
		}
	}

	void FixedUpdate() {
		Vector3 movement;
		if (SystemInfo.deviceType == DeviceType.Desktop) { 
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		} else {
			movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
		}

		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
