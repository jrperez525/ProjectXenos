using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
            player.GetComponent<Health>().updateHealth();
            
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(this);
        }
        if (col.gameObject.tag == "Ground")
        {
            Destroy(col.gameObject);
        }
        
        if (col.gameObject.tag == "Wall")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Platform")
        {
            Destroy(col.gameObject);
        }
    }

    void OnCollsionEnter2D(Collider2D e)
    {
        if (e.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
