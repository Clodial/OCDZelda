using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyCamelScript : MonoBehaviour 
{
    private float moveSpeed = .5f;
    public float px, py, pz, hyp;
    private int state = 0;
    private int ct = 30;

    private int hit = 0;
    private int health = 7;
    private int invuln = 0;

    public int isBoss;
    public int room;
    private int currentRoom;
    private int changeRoom;

    private Animator animator;
    private Vector3 player;
    private Vector3 newScale;

    public Transform pickup;
    public Transform poof;
    private Transform clone1;
    private Transform clone2;

    private Renderer camelRenderer;
    private Renderer healthBar;
    private Component healthBarScript;
    private GameObject gameData;
    
	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
        camelRenderer = this.GetComponent<Renderer>();
        if (isBoss == 1)
        {
            healthBar = GameObject.Find("Boss Health Bar").GetComponent<Renderer>();
            healthBarScript = GameObject.Find("Boss Health Bar").GetComponent("HealthBarScript");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameData = GameObject.Find("GameData");
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        currentRoom = gameDataScript.roomNum;
        changeRoom = gameDataScript.changeRoom;

        if (invuln > 0) invuln--;

        if (currentRoom == room && changeRoom == 0)
        {
            if(isBoss == 1) healthBar.enabled = true;

            if (invuln % 10 == 9) camelRenderer.enabled = false;
            else camelRenderer.enabled = true;

            state = animator.GetInteger("State");
            switch (state)
            {
                case 0:
                    if (ct == 0)
                    {
                        state = 1;
                        animator.SetInteger("State", state);
                        ct = 300;
                    }
                    else ct--;
                    break;
                case 2:
                    if (ct == 0)
                    {
                        state = 3;
                        animator.SetInteger("State", state);
                        ct = 60;
                    }
                    else
                    {
                        player = GameObject.FindGameObjectWithTag("Player").transform.position;
                        px = player.x - transform.position.x;
                        py = player.y - transform.position.y;
                        pz = Mathf.Sqrt((px * px) + (py * py));
                        if (pz != 0) hyp = moveSpeed / pz;
                        else hyp = 0;
                        if(pz > 0.2f)transform.Translate(px * hyp * Time.deltaTime, py * hyp * Time.deltaTime, 0, Space.World);

                        if (ct < 280)
                        {
                            newScale = transform.localScale;
                            if (px > 0) newScale.x = Mathf.Abs(newScale.x) * -1;
                            else newScale.x = Mathf.Abs(newScale.x);
                            transform.localScale = newScale;
                        }

                        ct--;
                    }
                    break;
                case 3:
                    break;
                default:
                    break;
            }

            //Hurt
            if (hit == 1)
            {
                health--;
                if(isBoss == 1) healthBarScript.SendMessage("SetHealth", health);

                if (health <= 0)
                {
                    DestroyObject(gameObject);
                    clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
                    clone1.Translate(0, 0, 0);
                    if (Random.Range(0, 99) > 79)
                    {
                        clone2 = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                        clone2.Translate(0, 0, 0);
                    }
                    if(isBoss == 1) gameData.SendMessage("LoadLevel", 2);
                }
                invuln = 30;
                hit = 0;
            }
        }
        else
        {
            if (isBoss == 1) healthBar.enabled = false;
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack" && invuln == 0)
        {
            hit = 1;
        }
    }
}
