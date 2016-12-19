using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScript : MonoBehaviour
{
    public int i;
    private int ct = 90;
    private CanvasRenderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ct > 0) ct--;
        else ct = 120;

        if (ct > 30) rend.SetAlpha(255);  //Make it visible
        else rend.SetAlpha(0);        //Make it invisible

        if (Input.GetKeyDown("return"))
        {
            if (i == 0) SceneManager.LoadScene(1);      //Load next scene
            else Application.Quit();
        }
    }
}
