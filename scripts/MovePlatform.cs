using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{

    bool hasPlayer = false;
    private float start_x, start_y;
    public GameObject player, platform;
    public float distance, speed;
    //distance is the distance covered from the initial posiiton 

    // Use this for initialization
    void Start()
    {
        start_x = platform.transform.position.x; start_y = platform.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {


        if (hasPlayer && platform.transform.position.x <= start_x + distance)
        {
            transform.position = new Vector2(platform.transform.position.x + speed, start_y);
            player.transform.position = new Vector2(player.transform.position.x + speed, player.transform.position.y);
        }



    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
            hasPlayer = true;
    }
}
