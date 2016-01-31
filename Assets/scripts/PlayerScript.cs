using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private float moveSpeed = 0;
    private float xSpeed = 0;
    private float ySpeed = 0;
    private int[] dashCt = new int[8];
    private float dash = 1;
    private int dashCd = 0;
    private int direction = 0;
    private int dirCt = 0;
    private int locked = 0;
    public int health = 10;
    private int hit = 0;
    private int invuln = 0;
    private Vector3 enemyPos;
    private Animator animator;
    private GameObject sword;
    
	// Use this for initialization
	void Start()
    {
        animator = this.GetComponent<Animator>();
        sword = transform.GetChild (0).gameObject;
        for (int i = 0; i < 8; i++)
            dashCt[i] = 0;
	}

    // Update is called once per frame
    void Update()
    {
        float maxSpeed = 4;
        float maxDash = 3.7f;
        float accel = 0.5f;
        float decel = 0.4f;

        if (dirCt > 0) dirCt--;
        for (int i = 0; i < 8; i++)
            if (dashCt[i] > 0) dashCt[i]--;
        if (dash > 1) dash -= 0.1f;
        if (dashCd > 0) dashCd--;
        if (invuln > 0) invuln--;
        
        
        if (invuln <= 15)
        {
            //Basic Movement
            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a") && dirCt == 0)
            {
                if (Input.GetKeyDown("w") && dashCd == 0)
                {
                    if (dashCt[0] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[0] = 12;
                }
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 0;
                ySpeed = 1;
                if (dirCt <= 0 && locked == 0)
                {
                    direction = 0;
                    animator.SetInteger("Direction", 0);
                }
            }
            if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && dirCt == 0)
            {
                if (Input.GetKeyDown("d") && dashCd == 0)
                {
                    if (dashCt[2] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[2] = 12;
                }
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 1;
                ySpeed = 0;
                if (dirCt <= 0 && locked == 0)
                {
                    direction = 1;
                    animator.SetInteger("Direction", 1);
                }
            }
            if (Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("a") && dirCt == 0)
            {
                if (Input.GetKeyDown("s") && dashCd == 0)
                {
                    if (dashCt[4] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[4] = 12;
                }
                if (moveSpeed < maxSpeed) moveSpeed += accel;
                xSpeed = 0;
                ySpeed = -1;
                if (dirCt <= 0 && locked == 0)
                { 
                    direction = 2;
                    animator.SetInteger("Direction", 2);
                }
            }
            if (Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("w") && dirCt == 0)
            {
                if (Input.GetKeyDown("a") && dashCd == 0)
                {
                    if (dashCt[6] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[6] = 12;
                }
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
            if (Input.GetKey("d") && Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s"))
            {
                if ((Input.GetKeyDown("d") || Input.GetKeyDown("w")) && dashCd == 0)
                {
                    if (dashCt[1] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[1] = 10;
                }
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
            if (Input.GetKey("d") && Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("w"))
            {
                if ((Input.GetKeyDown("d") || Input.GetKeyDown("s")) && dashCd == 0)
                {
                    if (dashCt[3] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[3] = 10;
                }
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
            if (Input.GetKey("a") && Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w"))
            {
                if ((Input.GetKeyDown("a") || Input.GetKeyDown("s")) && dashCd == 0)
                {
                    if (dashCt[5] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[5] = 10;
                }
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
            if (Input.GetKey("a") && Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s"))
            {
                if ((Input.GetKeyDown("a") || Input.GetKeyDown("w")) && dashCd == 0)
                {
                    if (dashCt[7] > 0)
                    {
                        dash = maxDash;
                        dashCd = 30;
                    }
                    dashCt[7] = 10;
                }
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

        //Attack
        if (Input.GetKeyDown("p"))
        {
            locked = 1;
        }
        if (Input.GetKeyUp("p"))
        {
            locked = 0;
            animator.SetInteger("Attack", 1);
            sword.tag = "Attack";
        }
        
        //Hurt
        if(hit == 1)
        {
            health--;
            invuln = 30;
            hit = 0;
            dash = 1;
            moveSpeed = 10;
            xSpeed = transform.position.x - enemyPos.x;
            ySpeed = transform.position.y - enemyPos.y;
        }

        transform.Translate(moveSpeed * xSpeed * dash * Time.deltaTime, moveSpeed * ySpeed * dash * Time.deltaTime, 0, Space.World);
        if (moveSpeed > 0) animator.SetFloat("Moving", 1);
        else animator.SetFloat("Moving", 0);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && invuln == 0)hit = 1;
        enemyPos = other.transform.position;
    }
}

 