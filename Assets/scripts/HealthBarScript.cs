using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{

    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();
	}

    void SetHealth(int health)
    { 
        animator.SetInteger("Health", health);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
