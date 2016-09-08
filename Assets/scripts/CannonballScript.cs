using UnityEngine;
using System.Collections;

public class CannonballScript : MonoBehaviour
{
    private float time;
    private GameObject gameData;
    private GameDataScript gameDataScript;

    // Use this for initialization
    void Start ()
    {
        time = Time.time;
        gameData = GameObject.Find("GameData");
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameDataScript = gameData.GetComponent<GameDataScript>();
        if (gameDataScript.changeRoom == 1) DestroyObject(gameObject);

        transform.Translate(Vector3.down * Time.deltaTime);

        if (Time.time - time > 5) DestroyObject(gameObject);
	}
    
}
