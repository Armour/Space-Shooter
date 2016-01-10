using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private GameController gameController;
	private Transform playerTransform;
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		if (!gameController.gameOver)
			playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		else
			playerTransform = null;
		currentSpeed = rb.velocity.z;
		StartCoroutine(Evade());
	}

	IEnumerator Evade() {
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true) {
			if (gameController.hardMode) {
				if (playerTransform != null)
					targetManeuver = playerTransform.position.x;
				else
					targetManeuver = transform.position.x;
			} else {
				targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			}
			yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}

	void FixedUpdate() {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}