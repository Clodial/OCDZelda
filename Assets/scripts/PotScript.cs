using UnityEngine;
using System.Collections;

public class PotScript : MonoBehaviour
{
    private Animator animator;
    public Transform pickup;
    private Transform clone;

	// Use this for initialization
	void Start () 
    {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack" && animator.GetInteger("Int") == 0)
        {
            animator.SetInteger("Int", 1);
            if (Random.Range(0, 99) > 79)
            {
                clone = Instantiate(pickup, transform.position, transform.rotation) as Transform;
                clone.Translate(0, 0, 0);
            }
        }
    }
}
