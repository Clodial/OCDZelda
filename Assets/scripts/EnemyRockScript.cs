using UnityEngine;
using System.Collections;

public class EnemyRockScript : MonoBehaviour {

    private float moveSpeed = 0.1f;
    private int direction = -1;
    private int ct = 0;
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
            if (ct == 0)
            {
                direction *= -1;
                animator.SetInteger("Direction", direction);
                ct = Random.Range(270, 330);
            }
            else ct--;

            transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0, Space.World);
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
            clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone1.Translate(0, 0, 0);
            if (Random.Range(0, 99) > 14)
            {
                clone2 = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                clone2.Translate(0, 0, 0);
            }
        }
    }
}
