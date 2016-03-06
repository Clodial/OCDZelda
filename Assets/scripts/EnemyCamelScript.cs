using UnityEngine;
using System.Collections;

public class EnemyCamelScript : MonoBehaviour 
{

    private float moveSpeed = .3f;
    public float px, py, pz, hyp;
    public int state = 0;
    public int ct = 30;
    private Animator animator;
    private Vector3 player;
    private Vector3 newScale;
    public Transform pickup;
    public Transform poof;
    private Transform clone1;
    private Transform clone2;
    
	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () 
    {
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
                    transform.Translate(px * hyp * Time.deltaTime, py * hyp * Time.deltaTime, 0, Space.World);

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
	}

    void OnTriggerEnter(Collider other)
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
        }
    }
}
