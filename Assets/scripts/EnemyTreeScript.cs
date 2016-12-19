using UnityEngine;
using System.Collections;

public class EnemyTreeScript : MonoBehaviour
{
    private float moveSpeed = 0.4f;
    private float mov;
    private float dir = 1;

    private int hit = 0;
    private int health = 7;
    private int invuln = 0;
    private float spawnTime;

    public int room;
    private int currentRoom;
    private int changeRoom;

    public Transform poof;
    public Transform spawn;
    private Transform clone;

    private Renderer healthBar;
    private Component healthBarScript;
    private SpriteRenderer rend;
    private GameObject gameData;

    // Use this for initialization
    void Start ()
    {
        healthBar = GameObject.Find("Boss Health Bar").GetComponent<Renderer>();
        healthBarScript = GameObject.Find("Boss Health Bar").GetComponent("HealthBarScript");
        rend = this.GetComponent<SpriteRenderer>();
        gameData = GameObject.Find("GameData");

        spawnTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        currentRoom = gameDataScript.roomNum;
        changeRoom = gameDataScript.changeRoom;

        if (currentRoom == room && changeRoom == 0)
        {
            healthBar.enabled = true;

            if (invuln > 0) mov = moveSpeed * 2;
            else mov = moveSpeed;

            if (invuln % 25 == 24) rend.enabled = false;
            else rend.enabled = true;

            transform.Translate(dir * mov * Time.deltaTime, 0, 0);

            if (Time.time - spawnTime > 0 && hit > 0) spawnBugs();
        }
        else
        {
            healthBar.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (invuln > 0) invuln--;
    } 

    void OnHit()
    {
        health--;
        healthBarScript.SendMessage("SetHealth", health);

        if (health <= 0)
        {
            DestroyObject(gameObject);
            clone = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone.Translate(0, 0, 0);
            gameData.SendMessage("LoadLevel", 4);
        }

        invuln = 250;
        hit = 7 - health;
        spawnTime = Time.time + 0.5f;
    }

    void spawnBugs()
    {
        float rot;

        clone = Instantiate(spawn, new Vector3(transform.position.x, transform.position.y - 0.3f), transform.rotation) as Transform;
        
        if (dir == 1) rot = Random.Range(150, 210);
        else rot = Random.Range(-30, 30);
        clone.eulerAngles = new Vector3(0, 0, rot);

        hit--;
        spawnTime = Time.time + 0.5f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall") dir *= -1;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack" && invuln == 0)
        {
            OnHit();
        }
    }
}
