using UnityEngine;
using System.Collections;

public class MovingForthandBack : MonoBehaviour
{
    
    //public float maxRight, maxLeft;
    public GameObject platform, player;
    public float speed = 0.16f;
    public float speedWithPlayer = 0.14f;
    float start_y, start_x, start_z;
    public float endMovement;

    bool isMovingLeft = true;
    bool hasPlayer = false;
    // Use this for initialization

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            hasPlayer = true;
        }

    }

    void Start()
    {
        start_x = platform.transform.position.x;
        start_y = platform.transform.position.y;
        start_z = platform.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {


        if (isMovingLeft)
        {
            // platform.transform.position = new Vector3(platform.transform.position.x - speed, start_y, start_z);
            if (hasPlayer)
            {
                player.transform.position = new Vector3(player.transform.position.x - speedWithPlayer, player.transform.position.y, player.transform.position.z);
                platform.transform.position = new Vector3(platform.transform.position.x - speedWithPlayer, start_y, start_z);
            }
            else
            {
               
                platform.transform.position = new Vector3(platform.transform.position.x - speed, start_y, start_z);
            }
            if (platform.transform.position.x <= (start_x + endMovement))
            {
                isMovingLeft = false;
            }
            if (Mathf.Abs(platform.transform.position.x - player.transform.position.x) >= 3f || Mathf.Abs(platform.transform.position.y - player.transform.position.y) >= 1.5f)
            {
                hasPlayer = false;
            }
        }
        else
        {
            // platform.transform.position = new Vector3(platform.transform.position.x + speed, start_y, start_z);
            if (hasPlayer)
            {
                player.transform.position = new Vector3(player.transform.position.x + speedWithPlayer, player.transform.position.y, player.transform.position.z);
                platform.transform.position = new Vector3(platform.transform.position.x + speedWithPlayer, start_y, start_z);
            }
            else
            {
                
                platform.transform.position = new Vector3(platform.transform.position.x + speed, start_y, start_z);
            }
            if (platform.transform.position.x >= start_x)
            {
                isMovingLeft = true;
            }
            if (Mathf.Abs(platform.transform.position.x - player.transform.position.x) >= 3f || Mathf.Abs(platform.transform.position.y - player.transform.position.y) >= 1.5f)
            {
                hasPlayer = false;
            }
        }
    }
}

