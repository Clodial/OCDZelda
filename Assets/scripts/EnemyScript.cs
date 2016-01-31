using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    private float moveSpeed = 3;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
        if (transform.position.x > 3) moveSpeed = -3;
        if (transform.position.x < -3) moveSpeed = 3;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
        }
    }
}
