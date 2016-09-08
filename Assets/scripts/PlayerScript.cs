using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private float moveSpeed = 0;
    private float xSpeed = 0;
    private float ySpeed = 0;
    public int direction = 2;
    private int dirCt = 0;
    private int locked = 0;
    public int health = 7;
    private int hit = 0;
    private int invuln = 0;
    private int weapon = 1;
    private int bowCt = 0;
    private int changeRoom;
    private int changeDir;
    private Vector3 enemyPos;
    private Animator animator;
    private GameObject sword;
    private GameObject bow;
    private GameObject spear;
    public Transform prefab;
    private Transform clone;
    private Transform clone1;
    private Transform clone2;
    public Transform poof;
    public Transform death;
    private Renderer rend1;     //Sword Renderer
    private Renderer rend2;     //Bow Renderer
    private Renderer rend3;     //Spear Renderer
    private Component healthBar;
    private GameObject gameData;

    private RaycastHit uHit;
    private RaycastHit dHit;
    private RaycastHit lHit;
    private RaycastHit rHit;
    private RaycastHit nHit;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetInteger("Weapon", 1);
        sword = transform.GetChild(0).gameObject;
        bow = transform.GetChild(1).gameObject;
        spear = transform.GetChild(2).gameObject;
        rend1 = sword.GetComponent<Renderer>();
        rend2 = bow.GetComponent<Renderer>();
        rend3 = spear.GetComponent<Renderer>();
        healthBar = GameObject.Find("Health Bar").GetComponent("HealthBarScript");
        gameData = GameObject.Find("GameData");
	}

    // Update is called once per frame
    void Update()
    {
        float maxSpeed = 1.2f;
        float accel = 0.5f;
        float decel = 0.4f;

        //Various Counters
        if (dirCt > 0) dirCt--;
        if (invuln > 0) invuln--;
        if (bowCt > 0) bowCt--;

        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        changeRoom = gameDataScript.changeRoom;
        changeDir = gameDataScript.changeDir;

        if (changeRoom == 0)
        {
            if (invuln <= 15)
            {
                //Basic Movement
                if ((Input.GetKey("w") || Input.GetKey("up")) && (!Input.GetKey("d") && !Input.GetKey("right"))
                        && (!Input.GetKey("s") && !Input.GetKey("down")) && (!Input.GetKey("a") && !Input.GetKey("up"))) //Up
                {
                    if (!Physics.BoxCast(transform.position, new Vector3(0.1f, 0), Vector3.up, out nHit, Quaternion.identity, 0.14f, 1, QueryTriggerInteraction.Ignore))
                    {
                        if (moveSpeed < maxSpeed) moveSpeed += accel;
                        if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    }
                    xSpeed = 0;
                    ySpeed = 1;
                    if (dirCt <= 0 && locked == 0)
                    {
                        direction = 0;
                        animator.SetInteger("Direction", 0);
                    }
                }
                if ((Input.GetKey("d") || Input.GetKey("right")) && (!Input.GetKey("w") && !Input.GetKey("up"))
                        && (!Input.GetKey("s") && !Input.GetKey("down")) && (!Input.GetKey("a") && !Input.GetKey("left"))) //Right
                {
                    if (!Physics.BoxCast(transform.position, new Vector3(0, 0.14f), Vector3.right, out nHit, Quaternion.identity, 0.1f, 1, QueryTriggerInteraction.Ignore))
                    {
                        if (moveSpeed < maxSpeed) moveSpeed += accel;
                        if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    }
                    xSpeed = 1;
                    ySpeed = 0;
                    if (dirCt <= 0 && locked == 0)
                    {
                        direction = 1;
                        animator.SetInteger("Direction", 1);
                    }
                }
                if ((Input.GetKey("s") || Input.GetKey("down")) && (!Input.GetKey("d") && !Input.GetKey("right"))
                        && (!Input.GetKey("w") && !Input.GetKey("up")) && (!Input.GetKey("a") && !Input.GetKey("left"))) //Down
                {
                    if (!Physics.BoxCast(transform.position, new Vector3(0.1f, 0), Vector3.down, out nHit, Quaternion.identity, 0.14f, 1, QueryTriggerInteraction.Ignore))
                    {
                        if (moveSpeed < maxSpeed) moveSpeed += accel;
                        if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    }
                    xSpeed = 0;
                    ySpeed = -1;                   
                    if (dirCt <= 0 && locked == 0)
                    {
                        direction = 2;
                        animator.SetInteger("Direction", 2);
                    }
                }
                if ((Input.GetKey("a") || Input.GetKey("left")) && (!Input.GetKey("d") && !Input.GetKey("right"))
                        && (!Input.GetKey("s") && !Input.GetKey("down")) && (!Input.GetKey("w") && !Input.GetKey("up"))) //Left
                {
                    if (!Physics.BoxCast(transform.position, new Vector3(0, 0.14f), Vector3.left, out nHit, Quaternion.identity, 0.1f, 1, QueryTriggerInteraction.Ignore))
                    {
                        if (moveSpeed < maxSpeed) moveSpeed += accel;
                        if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    }
                    xSpeed = -1;
                    ySpeed = 0;                  
                    if (dirCt <= 0 && locked == 0)
                    {
                        direction = 3;
                        animator.SetInteger("Direction", 3);
                    }
                }

                //Diagonal Movement
                if ((Input.GetKey("d") || Input.GetKey("right")) && (Input.GetKey("w") || Input.GetKey("up"))
                        && (!Input.GetKey("a") || !Input.GetKey("left")) && (!Input.GetKey("s") || !Input.GetKey("down"))) //Up/Right
                {
                    if (moveSpeed < maxSpeed) moveSpeed += accel;
                    if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    xSpeed = 1 / Mathf.Sqrt(2);
                    ySpeed = 1 / Mathf.Sqrt(2);
                    if (dirCt <= 0 && locked == 0)
                    {
                        if (direction == 3)
                        {
                            direction = 0;
                            animator.SetInteger("Direction", 0);
                        }
                        if (direction == 2)
                        {
                            //direction = 1;
                            //animator.SetInteger("Direction", 1);
                        }
                    }
                    dirCt = 5;
                }
                if ((Input.GetKey("d") || Input.GetKey("right")) && (Input.GetKey("s") || Input.GetKey("down"))
                        && (!Input.GetKey("a") || !Input.GetKey("left")) && (!Input.GetKey("w") || !Input.GetKey("up"))) //Down/Right
                {
                    if (moveSpeed < maxSpeed) moveSpeed += accel;
                    if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    xSpeed = 1 / Mathf.Sqrt(2);
                    ySpeed = -1 / Mathf.Sqrt(2);
                    if (dirCt <= 0 && locked == 0)
                    {
                        if (direction == 1 || direction == 0)
                        {
                            //direction = 1;
                            //animator.SetInteger("Direction", 1);
                        }
                        if (direction == 2 || direction == 3)
                        {
                            direction = 2;
                            animator.SetInteger("Direction", 2);
                        }
                    }
                    dirCt = 5;
                }
                if ((Input.GetKey("a") || Input.GetKey("left")) && (Input.GetKey("s") || Input.GetKey("down"))
                        && (!Input.GetKey("d") || !Input.GetKey("right")) && (!Input.GetKey("w") || !Input.GetKey("up"))) //Down/Left
                {
                    if (moveSpeed < maxSpeed) moveSpeed += accel;
                    if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    xSpeed = -1 / Mathf.Sqrt(2);
                    ySpeed = -1 / Mathf.Sqrt(2);
                    if (dirCt <= 0 && locked == 0)
                    {
                        if (direction == 2 || direction == 1)
                        {
                            direction = 2;
                            animator.SetInteger("Direction", 2);
                        }
                        if (direction == 3 || direction == 0)
                        {
                            direction = 3;
                            animator.SetInteger("Direction", 3);
                        }
                    }
                    dirCt = 5;
                }
                if ((Input.GetKey("a") || Input.GetKey("left")) && (Input.GetKey("w") || Input.GetKey("up"))
                        && (!Input.GetKey("d") || !Input.GetKey("right")) && (!Input.GetKey("s") || Input.GetKey("down"))) //Up/Left
                {
                    if (moveSpeed < maxSpeed) moveSpeed += accel;
                    if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
                    xSpeed = -1 / Mathf.Sqrt(2);
                    ySpeed = 1 / Mathf.Sqrt(2);
                    if (dirCt <= 0 && locked == 0)
                    {
                        if (direction == 3 || direction == 2)
                        {
                            direction = 3;
                            animator.SetInteger("Direction", 3);
                        }
                        if (direction == 0 || direction == 1)
                        {
                            direction = 0;
                            animator.SetInteger("Direction", 0);
                        }
                    }
                    dirCt = 5;
                }
            }
            else
            {
                if (moveSpeed > 0) moveSpeed -= decel;
                if (moveSpeed < 0) moveSpeed = 0;
            }

            //Idle
            if (!Input.GetKey("a") && !Input.GetKey("left") && !Input.GetKey("w") && !Input.GetKey("up")
                    && !Input.GetKey("d") && !Input.GetKey("right") && !Input.GetKey("s") && !Input.GetKey("down"))
            {
                if (moveSpeed > 0) moveSpeed -= decel;
                if (moveSpeed < 0) moveSpeed = 0;
            }

            //Switch Weapon
            if (Input.GetKeyDown("space"))
            {
                if (locked == 0) weapon = (weapon % 3) + 1;

                switch (weapon)
                {
                    case 1:
                        rend1.enabled = true;
                        rend2.enabled = false;
                        rend3.enabled = false;
                        animator.SetInteger("Weapon", 1);
                        break;
                    case 2:
                        rend1.enabled = false;
                        rend2.enabled = true;
                        rend3.enabled = false;
                        animator.SetInteger("Weapon", 2);
                        break;
                    case 3:
                        rend1.enabled = false;
                        rend2.enabled = false;
                        rend3.enabled = true;
                        animator.SetInteger("Weapon", 3);
                        break;
                }

                animator.SetInteger("Attack", 0);
            }

            //Attack
            if (Input.GetKey("p"))
            {
                locked = 1;
                if (weapon == 2 && bowCt < 5) animator.SetInteger("Attack", 1);
            }
            if (Input.GetKeyUp("p"))
            {
                locked = 0;
                if (weapon == 1)
                {
                    animator.SetInteger("Attack", 1);
                    sword.tag = "Attack";
                }
                if (weapon == 2)
                {
                    animator.SetInteger("Attack", 0);
                    if (bowCt == 0)
                    {
                        clone = Instantiate(prefab, transform.position, transform.rotation) as Transform;
                        clone.SendMessage("Set", direction);
                        bowCt = 30;
                    }
                }
                if (weapon == 3)
                {
                    animator.SetInteger("Attack", 1);
                    spear.tag = "Attack";
                }
                dirCt = 20;
            }

            if (animator.GetInteger("Attack") == -1 && (weapon == 1 || weapon == 3))
            {
                sword.tag = "Untagged";
                spear.tag = "Untagged";
                animator.SetInteger("Attack", 0);
            }

            //Hurt
            if (hit == 1)
            {
                health--;
                healthBar.SendMessage("SetHealth", health);

                if (health <= 0)
                {
                    clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
                    clone1.Translate(0, 0, 0);
                    clone2 = Instantiate(death, transform.position, transform.rotation) as Transform;
                    clone2.Translate(0, 0, 0);
                    gameData.SendMessage("LoadLevel", 1);
                    Destroy(gameObject);
                }
                invuln = 30;
                moveSpeed = 6;
                xSpeed = transform.position.x - enemyPos.x;
                ySpeed = transform.position.y - enemyPos.y;
                hit = 0;
            }

            for (int i = 0; i < 10; i++)
            {
                if (castUp() && ySpeed > 0) ySpeed = 0;
                if (castDown() && ySpeed < 0) ySpeed = 0;
                if (castLeft() && xSpeed < 0) xSpeed = 0;
                if (castRight() && xSpeed > 0) xSpeed = 0;

                //Crushed
                if (((castUp() && uHit.transform.tag == "Wall") && (castDown() && dHit.transform.tag == "Wall")) ||  
                        ((castLeft() && lHit.transform.tag == "Wall") && (castRight() && rHit.transform.tag == "Wall")))
                {
                    health = 0;
                    healthBar.SendMessage("SetHealth", health);
                    hit = 1;
                }

                //Movement
                transform.Translate(0.1f * moveSpeed * xSpeed * Time.deltaTime, 0.1f * moveSpeed * ySpeed * Time.deltaTime, 0, Space.World);
            }

            if (xSpeed == 0 && ySpeed == 0) moveSpeed = 0;

            if (moveSpeed > 0) animator.SetFloat("Moving", 1);
            else animator.SetFloat("Moving", 0);
        }
        else
        {
            if (changeDir == 1) transform.Translate(0, 0.7f * Time.deltaTime, 0, Space.World);
            if (changeDir == 2) transform.Translate(0.5f * Time.deltaTime, 0, 0, Space.World);
            if (changeDir == 3) transform.Translate(0, -0.5f * Time.deltaTime, 0, Space.World);
            if (changeDir == 4) transform.Translate(-0.7f * Time.deltaTime, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.Escape)) gameData.SendMessage("QuitGame");
	}

    bool castUp()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.06f, 0), Vector3.up, out uHit, Quaternion.identity, 0.12f, 1, QueryTriggerInteraction.Ignore);
    }

    bool castDown()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.06f, 0), Vector3.down, out dHit, Quaternion.identity, 0.12f, 1, QueryTriggerInteraction.Ignore);
    }

    bool castLeft()
    {
        return Physics.BoxCast(transform.position, new Vector3(0, 0.1f), Vector3.left, out lHit, Quaternion.identity, 0.07f, 1, QueryTriggerInteraction.Ignore);
    }

    bool castRight()
    {
        return Physics.BoxCast(transform.position, new Vector3(0, 0.1f), Vector3.right, out rHit, Quaternion.identity, 0.07f, 1, QueryTriggerInteraction.Ignore);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && invuln == 0)
        {
            hit = 1;
            enemyPos = other.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && invuln == 0)
        {
            hit = 1;
            enemyPos = other.transform.position;
        }
        if (other.gameObject.tag == "Health")
        {
            if(health < 7)health++;
            healthBar.SendMessage("SetHealth", health);
            DestroyObject(other.gameObject);
        }
        if (other.gameObject.tag == "Respawn")
        {
            if (other.transform.rotation.z != 0)
            {
                if (transform.position.x < other.transform.position.x) changeDir = 2;
                else changeDir = 4;
            }
            else
            {
                if (transform.position.y < other.transform.position.y) changeDir = 1;
                else changeDir = 3;
            }
            gameData.SendMessage("ChangeDir", changeDir);
            gameData.SendMessage("ChangeRoom", 1);
        }
        if (other.gameObject.tag == "Room")
        {
            gameData.SendMessage("ChangeNum", other.gameObject.layer-10);
        }
    }
}

 