using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour {

    public string scene;
    //private float start_x, start_y;
    public float deadZone1Height, deadZone2Height, zone1Start, zone2Start, zone1end, zone2end;
    

    // Use this for initialization
    void Start()
    {
       // start_x = transform.position.x;
       // start_y = transform.position.y;
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.x > zone1Start && transform.position.x < zone1end)
        {
            if (transform.position.y <= deadZone1Height)
                SceneManager.LoadScene(scene);
        }
        else if(transform.position.x > zone2Start && transform.position.x < zone2end)
        {
            if (transform.position.y <= deadZone2Height)
                SceneManager.LoadScene(scene);
        }
    }
}
