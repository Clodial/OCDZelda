using UnityEngine;
using System.Collections;

public class EnemyRoombaScript : MonoBehaviour 
{
    public float enemy;
    private float moveSpeed;
    public int direction;
    private int walkCt = 0;
    public int collCt = 0;
    private Animator animator;
    public Transform pickup;
    public Transform poof;
    private Transform clone1;
    private Transform clone2;

	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
        if (enemy == 0) moveSpeed = 0.4f;
        else moveSpeed = 0.6f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (walkCt == 0)
        {
            direction = Random.Range(0, 4);
            animator.SetInteger("Direction", direction);
            walkCt = Random.Range(30, 120);
        }
        else
        {
            switch (direction)
            { 
                case 0:
                    transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                    break;
                case 1:
                    transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                    break;
                case 2:
                    transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                    break;
                case 3:
                    transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                    break;
            }
            walkCt--;
        }
        collCt--;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
            clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone1.Translate(0, 0, 0);
            if (Random.Range(0, 99) > 69)
            {
                clone2 = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                clone2.Translate(0, 0, 0);
            }
        }

        direction += 2;
        if (direction > 3) direction -= 4;
        collCt += 10;
        animator.SetInteger("Direction", direction);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (collCt <= 0)
        {
            direction += 2;
            if (direction > 3) direction -= 4;
            if(coll.gameObject.tag == "Wall")collCt += 10;
            animator.SetInteger("Direction", direction);
        }
    }
}
