using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class levelChange : MonoBehaviour {

    private int first_level = 1;
    private int second_level = 2;
    private int third_level = 3;
    private int fourth_level = 4;
    private int fifth_level = 5;
    private int Main_Menu = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("1"))
        {
            SceneManager.LoadScene(first_level);
        }
        else if (Input.GetKey("2"))
        {
            SceneManager.LoadScene(second_level);
        }
        else if (Input.GetKey("3"))
        {
            SceneManager.LoadScene(third_level);
        }
        else if (Input.GetKey("4"))
        {
            SceneManager.LoadScene(fourth_level);
        }
        else if (Input.GetKey("5"))
        {
            SceneManager.LoadScene(fifth_level);
        }
        else if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(Main_Menu);
        }
    }
}
