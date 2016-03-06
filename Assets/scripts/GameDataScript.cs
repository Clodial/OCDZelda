using UnityEngine;
using System.Collections;

public class GameDataScript : MonoBehaviour
{
    public int changeRoom = 0;
    public int changeDir = 0;

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    void ChangeRoom(int i)
    {
        changeRoom = i;
    }

    void ChangeDir(int i)
    {
        changeDir = i;
    }
}