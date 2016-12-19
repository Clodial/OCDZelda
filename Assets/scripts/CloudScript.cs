using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour
{
    private float speed;

	// Use this for initialization
	void Start ()
    {
        speed = 0.1f + Random.Range(-0.05f, 0.05f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x > 2.5f) transform.position = new Vector3(-2.5f, transform.position.y);

        transform.Translate(speed * Vector3.right * Time.deltaTime);
	}
}
