using UnityEngine;
using System.Collections;

public class shootOnlyWhenNear : MonoBehaviour {

    public GameObject player, turret;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turret.transform.position.x - player.transform.position.x <= 20f && player.transform.position.x < turret.transform.position.x)
        {
            turret.GetComponent<WeaponScript>().enabled = true;
            turret.GetComponent<TurretScript>().enabled = true;
            turret.GetComponent<ShotScript>().enabled = true;
        }
        else
        {
            turret.GetComponent<WeaponScript>().enabled = false;
            turret.GetComponent<TurretScript>().enabled = false;
            turret.GetComponent<ShotScript>().enabled = false;
        }
    }
}
