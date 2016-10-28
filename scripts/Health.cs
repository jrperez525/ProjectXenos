using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int health = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 40), ("HEALTH: " + health.ToString()));
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Bullet")
            updateHealth();           
    }

    public void updateHealth()
    {
        health--;
    }
}
