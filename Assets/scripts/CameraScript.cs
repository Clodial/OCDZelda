using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Vector3 player;
    private float moveX = 0;
    private float moveY = 0;
    public int uCheck, dCheck, lCheck, rCheck = 0;
    private Collider uPoint, dPoint, lPoint, rPoint = null;
    public int collide;
    public int changeRoom = 0;
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

        changeDir = gameDataScript.changeDir;
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
            /*if (changing == 0)
            {
                if (player.x > transform.position.x + 0.9)
                {
                    moveX = 1;
                    changeDir = 2;
                }
                else if (player.x < transform.position.x - 0.9)
                {
                    moveX = -1;
                    changeDir = 4;
                }
                else moveX = 0;

                if (player.y > transform.position.y + 0.6)
                {
                    moveY = 1;
                    changeDir = 1;
                }
                else if (player.y < transform.position.y - 0.6)
                {
                    moveY = -1;
                    changeDir = 3;
                }
                else moveY = 0;

                gameData.SendMessage("ChangeDir", changeDir);

                changing = 1;
            }*/
            
            switch (changeDir)
            {
                case 1:
                    moveY = 1;
                    break;
                case 2:
                    moveX = 1;
                    break;
                case 3:
                    moveY = -1;
                    break;
                case 4:
                    moveX = -1;
                    break;
                default: break;
            }

            for (int i = 0; i < 10; i++)
            {
                transform.Translate(0.2f * moveX * Time.deltaTime, 0.2f * moveY * Time.deltaTime, 0, Space.World);
            }
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainCamera" && changeRoom == 0)
        {
            if (other.transform.position.x > transform.position.x && other.transform.rotation.z != 0)
            {
                rCheck = 1;
                rPoint = other;

                if (player.x > other.transform.position.x) rCheck = 0;
            }
            if (other.transform.position.x < transform.position.x && other.transform.rotation.z != 0)
            {
                lCheck = 1;
                lPoint = other;

                if (player.x < other.transform.position.x) lCheck = 0;
            }
            if (other.transform.position.y > transform.position.y && other.transform.rotation.z == 0)
            {
                uCheck = 1;
                uPoint = other;

                if (player.y > other.transform.position.y) uCheck = 0;
            }
            if (other.transform.position.y < transform.position.y && other.transform.rotation.z == 0)
            {
                dCheck = 1;
                dPoint = other;

                if (player.y < other.transform.position.y) dCheck = 0;
            }
        }
        collide = 1;
    }

    void OnTriggerExit(Collider other)
    {
        uCheck = 0;
        dCheck = 0;
        lCheck = 0;
        rCheck = 0;

        if (other.transform.position.x > transform.position.x + 2 && changeDir == 4 && lPoint.Equals(other))
        {
            changeRoom = 0;
            changing = 0;
            lCheck = 0;
        }
        if (other.transform.position.x < transform.position.x - 2 && changeDir == 2 && rPoint.Equals(other))
        {
            changeRoom = 0;
            changing = 0;
            rCheck = 0;
        }
        if (other.transform.position.y > transform.position.y + 1.5 && changeDir == 3 && dPoint.Equals(other))
        {
            changeRoom = 0;
            changing = 0;
            dCheck = 0;
        }
        if (other.transform.position.y < transform.position.y - 1.5 && changeDir == 1 && uPoint.Equals(other))
        {
            changeRoom = 0;
            changing = 0;
            uCheck = 0;
        }

        gameData.SendMessage("ChangeRoom", changeRoom);
        collide = 0;
    }
}
