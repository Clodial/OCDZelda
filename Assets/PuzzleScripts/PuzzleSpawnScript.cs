using UnityEngine;
using System.Collections;

public class PuzzleSpawnScript : MonoBehaviour 
{
	public Transform prefab;
	public float moveSpeed, spawnTime;
	private float nextSpawn, randSize = 0;

	// Use this for initialization
	void Start() 
	{
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (nextSpawn < Time.time) 
		{
			randSize = Random.Range(0,3f);

			Transform clone = Instantiate(prefab, transform.position, transform.rotation) as Transform;
			clone.SendMessage("SetSpeed", moveSpeed);
			clone.transform.localScale += new Vector3(0, randSize, 0);
			nextSpawn += spawnTime + randSize/3;
		}
	}
}
