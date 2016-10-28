using UnityEngine;
using System.Collections;

public class Attack_ConeGun : MonoBehaviour
{

    public GunEnemyAI gunAI;

    public bool isLeft = false;

    void Awake()
    {
        gunAI = gameObject.GetComponentInParent<GunEnemyAI>();

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                gunAI.Attack(false,true,true,false);
            }
            else
            {
                gunAI.Attack(true,true,true,false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        gunAI.Attack(false, false, false,false);
    }
}
