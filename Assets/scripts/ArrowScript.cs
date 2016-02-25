using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour 
{

    private float moveSpeed;
    private int dir;
    private float time = 0;

	// Use this for initialization
	void Start () 
    {
	
	}

    void Set(int direction)
    {
        dir = direction;
        moveSpeed = 8;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(dir)
        {
            case 0:
                transform.Translate(0, moveSpeed * Time.deltaTime, 0, Space.World);
                break;
            case 1:
                transform.eulerAngles = new Vector3(0, 0, -90);
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
                break;
            case 2:
                transform.eulerAngles = new Vector3(0, 0, -180);
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0, Space.World);
                break;
            case 3:
                transform.eulerAngles = new Vector3(0, 0, -270);
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
                break;
        }
        time++;
        if (time > 200) DestroyObject(gameObject);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            DestroyObject(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DestroyObject(gameObject);
        }
    }
}
