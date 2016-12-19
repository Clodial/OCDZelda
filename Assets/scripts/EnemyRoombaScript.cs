using UnityEngine;
using System.Collections;

public class EnemyRoombaScript : MonoBehaviour 
{
    public float enemy;
    private float moveSpeed;
    private int direction;
    private int walkCt = 0;
    private int collCt = 0;
    public int room;
    private int currentRoom;
    private int changeRoom;
    private Animator animator;
    public Transform pickup;
    public Transform poof;
    private Transform clone1;
    private Transform clone2;
    private GameObject gameData;

	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
        if (enemy == 0) moveSpeed = 0.6f;
        else moveSpeed = 0.8f;
        gameData = GameObject.Find("GameData");
	}
	
	// Update is called once per frame
	void Update () 
    {
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        currentRoom = gameDataScript.roomNum;
        changeRoom = gameDataScript.changeRoom;

        if (currentRoom == room && changeRoom == 0)
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
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
            clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone1.Translate(0, 0, 0);
            if (Random.Range(0, 99) > 79)
            {
                clone2 = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                clone2.Translate(0, 0, 0);
            }
            direction += 2;
            if (direction > 3) direction -= 4;
            collCt += 15;
            animator.SetInteger("Direction", direction);
        }
        if(other.tag == "Respawn")
        {
            direction += 2;
            if (direction > 3) direction -= 4;
            collCt += 15;
            animator.SetInteger("Direction", direction);
        }
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

        if (coll.transform.tag == "Player") coll.transform.SendMessage("OnHit", transform.position);
    }
}
