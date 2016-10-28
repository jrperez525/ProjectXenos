using UnityEngine;
using System.Collections;

public class EnemyScript1 : MonoBehaviour
{

    //Enemy script 1 is used by the enemy to patrol in an area
    //From the start point he goes back and forth left and right

    float start_x; //public because everytime, player goes on change and returns for patrol his start position changes,
                          //we don't want it to patrol any other area.
                    
    private Animation anim;
    
    float start_y, start_z;
    public float patrolDistance, speed; //Distance to travel from the initial to the extreme
    //if patrolDistance is 5, enemy walks 5 to right from start then 5 to left from start, thus, totol 10
    bool goRight = true; //initially go right
    public GameObject enemy, player;
    public int curHealth, maxHealth;
    // Use this for initialization
    void Start()
    {
        anim = enemy.GetComponent<Animation>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
        start_x = enemy.transform.position.x;
        start_y = enemy.transform.position.y;
        start_z = enemy.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth == 0)
        {
            Destroy(gameObject);
        }

        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) <= 7f)
        { //if player is near, either from left or right, stop patrolling, and attack him.
          //So, stop this script and enable the chase script, enemyscript2.
            GetComponent<Enemyscript3>().enabled = true;
            GetComponent<EnemyScript1>().enabled = false;
        }
        if (goRight)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x + speed, start_y, start_z);
            if (enemy.transform.position.x >= (start_x + patrolDistance))
                goRight = false;
        }
        else
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x - speed, start_y, start_z);
            if (enemy.transform.position.x <= start_x - patrolDistance)
                goRight = true;

        }
    }
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Bullet")
        {
            updateHealth();
            Destroy(obj.gameObject);
        }
    }

    public void updateHealth()
    {
        curHealth--;
    }
}