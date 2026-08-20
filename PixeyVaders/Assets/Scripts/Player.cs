using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 50;
	public float tiltAmt = 30;
	public float currentTilt = 0;

	private Rigidbody rb;
	private Vector3 moveDest = new Vector3(0,0,0);
	private int horizontal = 0;
	private float sqrRemainingDistance;

	public float leftBound = -70;
	public float rightBound = 70;

	public GameObject shot;
	public Transform shotSpawn;
	public float nextFire;
	public float fireRate = 2.0f;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow) && rb.transform.position.x >= leftBound) {
			horizontal += -10;
			moveDest.x = horizontal;
			rb.rotation = Quaternion.Euler(0f,0f, tiltAmt);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) && rb.transform.position.x <= rightBound) {
			horizontal += 10;
			moveDest.x = horizontal;
			rb.rotation = Quaternion.Euler (0f, 0f, -tiltAmt);
		}

		if (Input.GetButton("Jump") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
		sqrRemainingDistance = (rb.transform.position - moveDest).sqrMagnitude;

		if (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards(rb.transform.position, moveDest, Time.deltaTime * speed);

			rb.MovePosition (newPosition);

			sqrRemainingDistance = (rb.transform.position - moveDest).sqrMagnitude;
		}
			
		if(rb.rotation.z != 0)
			rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.Euler (0f,0f,0f), Time.deltaTime * (speed * 2));
	}

	void OnTriggerEnter()
	{
		
	}
}
