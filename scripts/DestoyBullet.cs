using UnityEngine;
using System.Collections;

public class DestoyBullet : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Bullet")
        { 
            Destroy(obj.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
