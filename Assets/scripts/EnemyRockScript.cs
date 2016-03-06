using UnityEngine;
using System.Collections;

public class EnemyRockScript : MonoBehaviour {

    private float moveSpeed = 0.1f;
    private int direction = -1;
    private int ct = 0;
    private Animator animator;
    public Transform pickup;
    public Transform poof;
    private Transform clone1;
    private Transform clone2;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (ct == 0)
        {
            direction *= -1;
            animator.SetInteger("Direction", direction);
            ct = Random.Range(270, 330);
        }
        else ct--;

        transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0, Space.World);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
            clone1 = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone1.Translate(0, 0, 0);
            if (Random.Range(0, 99) > 33)
            {
                clone2 = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                clone2.Translate(0, 0, 0);
            }
        }
    }
}
