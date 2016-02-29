using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 0;
    private float xSpeed = 0;
    private float ySpeed = 0;
    private int direction = 0;
    private int dirCt = 0;
    private int locked = 0;
    public int health = 7;
    private int hit = 0;
    private int invuln = 0;
    private int weapon = 1;
    private int bowCt = 0;
    private Vector3 enemyPos;
    private Animator animator;
    private GameObject sword;
    private GameObject bow;
    private GameObject spear;
    public Transform prefab;
    private Renderer rend1;      //Sword Renderer
    private Renderer rend2;      //Bow Renderer
    private Renderer rend3;      //Spear Renderer
    private Component healthBar;
    
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
	}

    // Update is called once per frame
    void Update()
    {
        float maxSpeed = 5;
        float accel = 0.5f;
        float decel = 0.4f;

        //Various Counters
        if (dirCt > 0) dirCt--;
        if (invuln > 0) invuln--;
        if (bowCt > 0) bowCt--;
        
        
        if (invuln <= 15)
        {
            //Basic Movement
            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a")) //Up
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 0;
                ySpeed = 1;
                if (dirCt <= 0 && locked == 0)
                {
                    direction = 0;
                    animator.SetInteger("Direction", 0);
                }
            }
            if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a")) //Right
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 1;
                ySpeed = 0;
                if (dirCt <= 0 && locked == 0)
                {
                    direction = 1;
                    animator.SetInteger("Direction", 1);
                }
            }
            if (Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("a")) //Down
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 0;
                ySpeed = -1;
                if (dirCt <= 0 && locked == 0)
                { 
                    direction = 2;
                    animator.SetInteger("Direction", 2);
                }
            }
            if (Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("w")) //Left
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = -1;
                ySpeed = 0;
                if (dirCt <= 0 && locked == 0)
                {
                    direction = 3;
                    animator.SetInteger("Direction", 3);
                }
            }

            //Diagonal Movement
            if (Input.GetKey("d") && Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s")) //Up/Right
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 1 / Mathf.Sqrt(2);
                ySpeed = 1 / Mathf.Sqrt(2);
                if (dirCt <= 0 && locked == 0)
                {
                    if (direction == 0 || direction == 3)
                    {
                        direction = 0;
                        animator.SetInteger("Direction", 0);
                    }
                    else 
                    {
                        direction = 1;
                        animator.SetInteger("Direction", 1);
                    }
                }
                dirCt = 5;
            }
            if (Input.GetKey("d") && Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("w")) //Down/Right
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 1 / Mathf.Sqrt(2);
                ySpeed = -1 / Mathf.Sqrt(2);
                if (dirCt <= 0 && locked == 0)
                {
                    if (direction == 1 || direction == 0)
                    {
                        direction = 1;
                        animator.SetInteger("Direction", 1);
                    }
                    else
                    {
                        direction = 2;
                        animator.SetInteger("Direction", 2);
                    }
                }
                dirCt = 5;
            }
            if (Input.GetKey("a") && Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w")) //Down/Left
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = -1 / Mathf.Sqrt(2);
                ySpeed = -1 / Mathf.Sqrt(2);
                if (dirCt <= 0 && locked == 0)
                {
                    if (direction == 2 || direction == 1)
                    {
                        direction = 2;
                        animator.SetInteger("Direction", 2);
                    }
                    else
                    {
                        direction = 3;
                        animator.SetInteger("Direction", 3);
                    }
                }
                dirCt = 5;
            }
            if (Input.GetKey("a") && Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s")) //Up/Left
            {
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = -1 / Mathf.Sqrt(2);
                ySpeed = 1 / Mathf.Sqrt(2);
                if (dirCt <= 0 && locked == 0)
                {
                    if (direction == 3 || direction == 2)
                    {
                        direction = 3;
                        animator.SetInteger("Direction", 3);
                    }
                    else
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
        if (!Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s"))
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
        }

        //Attack
        if (Input.GetKeyDown("p"))
        {
            locked = 1;
            if (weapon == 2) animator.SetInteger("Attack", 1);
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
                    Transform clone = Instantiate(prefab, transform.position, transform.rotation) as Transform;
                    clone.SendMessage("Set", direction);
                    bowCt = 30;
                }
            }
            if (weapon == 3)
            {
                animator.SetInteger("Attack", 1);
                spear.tag = "Attack";
            }
            dirCt = 25;
        }

        if (animator.GetInteger("Attack") == -1)
        {
            sword.tag = "Untagged";
            spear.tag = "Untagged";
            animator.SetInteger("Attack", 0);
        }
        
        //Hurt
        if(hit == 1)
        {
            health--;
            healthBar.SendMessage("SetHealth", health);
            invuln = 30;
            moveSpeed = 6;
            xSpeed = transform.position.x - enemyPos.x;
            ySpeed = transform.position.y - enemyPos.y;
            hit = 0;
        }

        //Movement
        transform.Translate(moveSpeed * xSpeed * Time.deltaTime, moveSpeed * ySpeed * Time.deltaTime, 0, Space.World);
        
        if (moveSpeed > 0) animator.SetFloat("Moving", 1);
        else animator.SetFloat("Moving", 0);
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
        if (other.gameObject.tag == "Health")
        {
            if(health < 7)health++;
            healthBar.SendMessage("SetHealth", health);
            DestroyObject(other.gameObject);
        }
    }
}

 