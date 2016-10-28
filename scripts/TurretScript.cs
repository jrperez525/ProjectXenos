using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
    bool shoot = true;
	// Use this for initialization
    
    void Start () {
        StartCoroutine(SetTimeDelay());
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    IEnumerator SetTimeDelay()
    {
        
        while (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack();
            }
            yield return new WaitForSeconds(2);
        }
    }
 }

