using UnityEngine;
using System.Collections;

public class RoomScript : MonoBehaviour
{
    float x, y;

	// Use this for initialization
	void Start ()
    {
        x = Mathf.Round(transform.position.x / 0.32f);
        y = Mathf.Round(transform.position.y / 0.32f);
        x *= 0.32f;
        y *= 0.32f;

        transform.position = new Vector3(x, y);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
