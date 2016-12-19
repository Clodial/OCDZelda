using UnityEngine;
using System.Collections;

public class EnemyCentipedeScript : MonoBehaviour
{
    private float move, moveSpeed = 0.7f;
    private float dx, dy = 0;
    private float dir, direction = 0;
    private float rot = 0.015f;

    public int room;
    private int currentRoom;
    private int changeRoom;

    public Transform poof;
    private Transform clone;

    private Vector3 player;
    private GameObject gameData;

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.Find("GameData");
        dir = transform.eulerAngles.z * Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
        currentRoom = gameDataScript.roomNum;
        changeRoom = gameDataScript.changeRoom;

        if (currentRoom == room && changeRoom == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.position;
            dx = player.x - transform.position.x;
            dy = player.y - transform.position.y;
            direction = Mathf.Atan2(dy, dx);

            if (((direction - dir < 0) && (direction - dir > -Mathf.PI)) || (direction - dir >= Mathf.PI)) dir -= rot;
            else dir += rot;

            if (Mathf.Abs(direction - dir) <= Mathf.PI) move = ((Mathf.PI - Mathf.Abs(direction - dir) + 0.1f) / Mathf.PI + 0.1f) * moveSpeed;
            else move = (((2 * Mathf.PI) - Mathf.Abs(direction - dir) + 0.1f) / Mathf.PI + 0.1f) * moveSpeed;

            if (dir > Mathf.PI) dir -= 2 * Mathf.PI;
            if (dir <= -Mathf.PI) dir += 2 * Mathf.PI;

            transform.eulerAngles = new Vector3(0, 0, dir * Mathf.Rad2Deg);
            transform.Translate(move * Vector3.right * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Attack")
        {
            DestroyObject(gameObject);
            clone = Instantiate(poof, transform.position, transform.rotation) as Transform;
            clone.Translate(0, 0, 0);
        }
    }
}
