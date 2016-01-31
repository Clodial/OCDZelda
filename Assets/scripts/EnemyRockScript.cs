using UnityEngine;
using System.Collections;

public class EnemyRockScript : MonoBehaviour {

    private float moveSpeed = 0.5f;
    private int direction = -1;
    private int ct = 0;
    private Animator animator;

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
            DestroyObject(gameObject);
    }
}
