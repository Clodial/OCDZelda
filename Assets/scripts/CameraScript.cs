using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Vector3 player;
    private float moveX = 0;
    private float moveY = 0;
    public int uCheck, dCheck, lCheck, rCheck = 0;
    public int collide;
    public int changeRoom;
    public int changeDir = 0;
    public int changing = 0;
    private GameObject gameData;

	// Use this for initialization
	void Start () 
    {
        gameData = GameObject.Find("GameData");
	}
	
	// Update is called once per frame
	void Update ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        changeRoom = gameDataScript.changeRoom;

        if (changeRoom == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                if (player.x > transform.position.x + 0.4 && rCheck == 0) moveX = 1;
                else if (player.x < transform.position.x - 0.4 && lCheck == 0) moveX = -1;
                else moveX = 0;

                if (player.y > transform.position.y + 0.2 && uCheck == 0) moveY = 1;
                else if (player.y < transform.position.y - 0.2 && dCheck == 0) moveY = -1;
                else moveY = 0;

                transform.Translate(0.1f * moveX * Time.deltaTime, 0.1f * moveY * Time.deltaTime, 0, Space.World);
            }
        }
        else
        {
            if (changing == 0)
            {
                if (player.x > transform.position.x + 1.8)
                {
                    moveX = 1;
                    changeDir = 2;
                }
                else if (player.x < transform.position.x - 1.8)
                {
                    moveX = -1;
                    changeDir = 4;
                }
                else moveX = 0;

                if (player.y > transform.position.y + 1.3)
                {
                    moveY = 1;
                    changeDir = 1;
                }
                else if (player.y < transform.position.y - 1.3)
                {
                    moveY = -1;
                    changeDir = 3;
                }
                else moveY = 0;

                gameData.SendMessage("ChangeDir", changeDir);

                changing = 1;
            }
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(0.2f * moveX * Time.deltaTime, 0.2f * moveY * Time.deltaTime, 0, Space.World);
            }
        }
	}

    /*void OnCollisionExit(Collision other)
    {
        uCheck = 0;
        dCheck = 0;
        lCheck = 0;
        rCheck = 0;

        if (other.gameObject.transform.position.x > transform.position.x + 2 && changeDir == 4)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.gameObject.transform.position.x < transform.position.x - 2 && changeDir == 2)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.gameObject.transform.position.y > transform.position.x + 1.5 && changeDir == 3)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.gameObject.transform.position.y < transform.position.x - 1.5 && changeDir == 1)
        {
            changeRoom = 0;
            changing = 0;
        }
        collide = 0;
    }*/
    
    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            if (other.gameObject.transform.position.x > transform.position.x + 2) rCheck = 1;
            if (other.gameObject.transform.position.x < transform.position.x - 2) lCheck = 1;
            if (other.gameObject.transform.position.y > transform.position.x + 1.5) uCheck = 1;
            if (other.gameObject.transform.position.y < transform.position.x - 1.5) dCheck = 1;
        }
        collide = 1;
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainCamera" && changeRoom == 0)
        {
            if (other.transform.position.x > transform.position.x + 2 && other.transform.position.y < transform.position.y + 1.5 && other.transform.position.y > transform.position.y - 1.5) rCheck = 1;
            if (other.transform.position.x < transform.position.x - 2 && other.transform.position.y < transform.position.y + 1.5 && other.transform.position.y > transform.position.y - 1.5) lCheck = 1;
            if (other.transform.position.y > transform.position.y + 1.5 && other.transform.position.x < transform.position.x + 2 && other.transform.position.x > transform.position.x - 2) uCheck = 1;
            if (other.transform.position.y < transform.position.y - 1.5 && other.transform.position.x < transform.position.x + 2 && other.transform.position.x > transform.position.x - 2) dCheck = 1;
        }
        collide = 1;
    }

    void OnTriggerExit(Collider other)
    {
        uCheck = 0;
        dCheck = 0;
        lCheck = 0;
        rCheck = 0;

        if (other.transform.position.x > transform.position.x + 2 && changeDir == 4)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.transform.position.x < transform.position.x - 2 && changeDir == 2)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.transform.position.y > transform.position.y + 1.5 && changeDir == 3)
        {
            changeRoom = 0;
            changing = 0;
        }
        if (other.transform.position.y < transform.position.y - 1.5 && changeDir == 1)
        {
            changeRoom = 0;
            changing = 0;
        }

        gameData.SendMessage("ChangeRoom", changeRoom);
        collide = 0;
    }
}
