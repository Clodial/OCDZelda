using UnityEngine;
using System.Collections;

public class EnemyBirdScript : MonoBehaviour
{
    private float moveSpeed = 1.2f;
    private float xSpeed = 0;
    private float ySpeed = 0;

    private float rot, rotSpeed = 0.0175f;
    private float dx, dy = 0;
    private float dir, direction = 0;

    private int flying = 1;
    private float flyTime;
    private float rand = 0;

    private int health = 3;
    private float hit = 0;
    private float hitTime;

    public int isCapt;
    public int room;
    private int currentRoom;
    private int changeRoom;
    
    public Transform poof;
    private Transform clone;

    private Vector3 player;
    private SpriteRenderer rend;
    private Animator anim;
    private BoxCollider col;

    private Renderer healthBar;
    private Component healthBarScript;
    public Transform bird;
    public Transform ulSpawn;
    public Transform urSpawn;
    public Transform dSpawn;
    private GameObject gameData;


    // Use this for initialization
    void Start ()
    {
        if (isCapt == 1)
        {
            moveSpeed = 1.4f;
            rotSpeed = 0.02f;
            health = 7;

            healthBar = GameObject.Find("Boss Health Bar").GetComponent<Renderer>();
            healthBarScript = GameObject.Find("Boss Health Bar").GetComponent("HealthBarScript");
        }
        rot = rotSpeed;
        flyTime = Time.time;
        hitTime = Time.time;

        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        dx = player.x - transform.position.x;
        dy = player.y - transform.position.y;

        dir = Mathf.Atan2(dy, dx);

        rend = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        col = this.GetComponent<BoxCollider>();
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
            if (isCapt == 1) healthBar.enabled = true;

            if (Time.time - flyTime > 3 + rand)     //Stop Flying
            {
                flying = 0;
                rot = rotSpeed * 2.5f;
            }
            if (Time.time - flyTime > 4 + rand)      //Start Flying
            {
                flying = 1;
                flyTime = Time.time;
                rot = rotSpeed;
                rand = Random.Range(-0.75f, 0.75f);
            }

            if (hit == 1 && Time.time - hitTime > 0.5f)
            {
                hit = 0;
                col.enabled = true;
            }

            player = GameObject.FindGameObjectWithTag("Player").transform.position;
            dx = player.x - transform.position.x;
            dy = player.y - transform.position.y;
            direction = Mathf.Atan2(dy, dx);

            if (Mathf.Abs(direction - dir) <= rot) dir = direction;

            if (hit == 0)
            {
                if (((direction - dir < 0) && (direction - dir > -Mathf.PI)) || (direction - dir >= Mathf.PI)) dir -= rot;
                else dir += rot;
            }
            else
            {
                if (((direction - dir < 0) && (direction - dir > -Mathf.PI)) || (direction - dir >= Mathf.PI)) dir += rot * 3;
                else dir -= rot * 3;
            }

            if (flying == 1)
            {
                xSpeed = Mathf.Cos(dir) * moveSpeed;
                ySpeed = Mathf.Sin(dir) * moveSpeed;

                if (hit == 1)
                {
                    xSpeed *= 1.1f;
                    ySpeed *= 1.1f;
                }

                transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);
            }

            if (rend.enabled == false && Time.time - hitTime > 0.1f) rend.enabled = true;

            if (xSpeed < 0) rend.flipX = true;
            if (xSpeed > 0) rend.flipX = false;
            anim.SetInteger("Flying", flying);

            if (dir > Mathf.PI) dir -= 2 * Mathf.PI;
            if (dir <= -Mathf.PI) dir += 2 * Mathf.PI;
        }
        else
        {
            if(isCapt == 1) healthBar.enabled = false;            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            if (health - 1 <= 0)
            {
                DestroyObject(gameObject);
                clone = Instantiate(poof, transform.position, transform.rotation) as Transform;
                clone.Translate(0, 0, 0);
            }
            health--;
            rend.enabled = false;
            col.enabled = false;

            hit = 1;
            hitTime = Time.time;
            flying = 1;
            flyTime = Time.time - 1;

            if (isCapt == 1)
            {
                healthBarScript.SendMessage("SetHealth", health);
                if (health % 2 == 0)
                {
                    if (health == 4 || health == 2)
                    {
                        clone = Instantiate(bird, ulSpawn.position, transform.rotation) as Transform;
                        clone = Instantiate(bird, urSpawn.position, transform.rotation) as Transform;
                    }
                    if (health == 6 || health == 2) clone = Instantiate(bird, dSpawn.position, transform.rotation) as Transform;
                }
            }
        }
    }
}
