using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameDataScript : MonoBehaviour
{
    public int changeRoom = 0;
    public int changeDir = 0;
    public int roomNum = 1;
    public int level = 1;
    public int ct = 0;
    public int load = 0;
    public int quit = 0;

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);

        if (ct > 0) ct--;
        if (load == 1 && ct <= 0)
        {
            load = 0;
            SceneManager.LoadScene(level);
        }
        if (quit == 1 && ct <= 0)
        {
            Application.Quit();
        }
    }

    void ChangeRoom(int i)
    {
        changeRoom = i;
    }

    void ChangeDir(int i)
    {
        changeDir = i;
    }

    void ChangeNum(int i)
    {
        roomNum = i;
    }

    void LoadLevel(int i)
    {
        level = i;
        load = 1;
        ct = 45;
    }

    void ReloadLevel()
    {
        load = 1;
        ct = 45;
    }

    void QuitGame()
    {
        quit = 1;
        ct = 30;
    }
}