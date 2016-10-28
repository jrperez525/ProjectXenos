using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{

    //private float x_start;
    //private float y_start;

    private float x_coord;
    private float y_coord;

    public int damage = 1;
    public bool isEnemyShot = false;
    public float speed = .001f;

    // Use this for initialization
    void Start()
    {
        
        //x_start = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        checkKeyPress();
        x_coord = transform.position.x;
    }

    void checkKeyPress()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.down * 10);
       
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player"|| obj.gameObject.tag == "Ground")
            Destroy(gameObject);

    }
}