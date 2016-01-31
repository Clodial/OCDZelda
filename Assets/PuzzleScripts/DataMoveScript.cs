using UnityEngine;
using System.Collections;

public class DataMoveScript : MonoBehaviour 
{
	private bool moveUp, moveDown, moveLeft, moveRight, reset = false;
	private float accelUp, accelDown, accelLeft, accelRight = 0;
	private float moveSpeed = 3;
	private float accelSpeed = 0.015f;
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		//Reset position and movement
		if (reset) 
		{
			gameObject.transform.position = new Vector3 (-1, 0, 0);
			accelUp = 0; 
			accelDown = 0; 
			accelLeft = 0; 
			accelRight = 0;
			reset = false;
		}


		//Inputs
		if (Time.time > 3) 
		{
			if (Input.GetKey ("w"))
					moveUp = true;
				else
					moveUp = false;
			if (Input.GetKey ("s"))
					moveDown = true;
				else
					moveDown = false;
			if (Input.GetKey ("a"))
					moveLeft = true;
				else
					moveLeft = false;
			if (Input.GetKey ("d"))
					moveRight = true;
				else
					moveRight = false;
		}

		//Acceleration: Done like this so movement isn't instantaneous and there's some difficulty maneuvering
		if(moveUp) 
		{
			if(accelUp < moveSpeed)
				accelUp += accelSpeed;
		}
			else
				if(accelUp > 0)
					accelUp -= accelSpeed;
		if(moveDown) 
		{
			if(accelDown < moveSpeed)
				accelDown += accelSpeed;
		}
			else
			{
				if(accelDown > 0)
					accelDown -= accelSpeed;
			}
		if(moveLeft) 
		{
			if(accelLeft < moveSpeed)
				accelLeft += accelSpeed;
		}
			else
			{
				if(accelLeft > 0)
					accelLeft -= accelSpeed;
			}
		if(moveRight) 
		{
			if (accelRight < moveSpeed)
				accelRight += accelSpeed;
		}
			else
			{
				if(accelRight > 0)
					accelRight -= accelSpeed;
			}


		//Actual movement
		transform.Translate((accelRight - accelLeft) * Time.deltaTime, (accelUp - accelDown) * Time.deltaTime, 0, Space.World);


		//Don't go out of bounds
		if(transform.position.x < -2 || transform.position.y < -5.3 || transform.position.y > 5.3)
			reset = true;


		//Win condition ((not yet))
		if(transform.position.x > 11)
			reset = true;
	}

	void OnTriggerEnter(Collider other)
	{
		//If you hit anything, you start over
		reset = true;
	}
}
