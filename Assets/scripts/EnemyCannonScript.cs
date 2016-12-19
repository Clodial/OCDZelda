using UnityEngine;
using System.Collections;

public class EnemyCannonScript : MonoBehaviour
{
    private float moveSpeed = 0.5f;
    public int dir = 1;
    private float shoot;
    public int rot;
    public int room;
    private int currentRoom;
    private int changeRoom;

    private RaycastHit hit;
    public Transform ball;
    private Transform clone;
    private GameObject gameData;

    // Use this for initialization
    void Start ()
    {
        shoot = Time.deltaTime;
        gameData = GameObject.Find("GameData");
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        currentRoom = gameDataScript.roomNum;
        changeRoom = gameDataScript.changeRoom;

        if (currentRoom == room && changeRoom == 0)
        {
            if (Time.time - shoot > 2)
            {
                clone = Instantiate(ball, new Vector3(transform.position.x, transform.position.y - 0.16f + (0.32f * rot)), transform.rotation) as Transform;
                clone.Translate(0, 0, 0);
                clone.Rotate(0, 0, 180 * rot);

                shoot = Time.time + Random.Range(-0.2f, 0.2f);
            }

            transform.Translate(dir * moveSpeed * Time.deltaTime, 0, 0);

            if (castRight() && hit.transform.tag != "Player") dir = -1;
            if (castLeft() && hit.transform.tag != "Player") dir = 1;
        }
	}

    bool castRight()
    {
        return Physics.BoxCast(transform.position, new Vector3(0, 0.2f), Vector3.right, out hit, Quaternion.identity, 0.15f, 1, QueryTriggerInteraction.Ignore);
    }

    bool castLeft()
    {
        return Physics.BoxCast(transform.position, new Vector3(0, 0.2f), Vector3.left, out hit, Quaternion.identity, 0.15f, 1, QueryTriggerInteraction.Ignore);
    }
}
