using UnityEngine;
using System.Collections;

public class PuzzleMoveScript : MonoBehaviour 
{
	private float moveSpeed;

	// Use this for initialization
	void Start()
	{
	
	}

	void SetSpeed(float speed)
	{
		moveSpeed = speed;
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Destroy (gameObject);
		}
	}
}
