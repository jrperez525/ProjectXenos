using UnityEngine;
using System.Collections;

public class Enemyscript3 : MonoBehaviour
{

    //In this script the enemy chases the player until he's far enough or dead if near
    //If he's very near say 1 or 2f, he attacks and retreats back some distance, 
    //after retreating, he chases back and then attacks again, 
    //this keeps repeating, until anyone of them is dead!

    //The enemy can chase, attack or retreat.
    //First thing! He chases after the player, after the chase he attacks.
    //After the attack, he retreats back some distance and then chases again and this repeats.

    float start_x, start_y, start_z;
    public GameObject enemy, player, turret;
    public float speed;
    public string status;
    public int curhealth;
    public int maxhealth;

    private Animator anim;

    //Bools that make this script
    bool isChasing;
    bool isRetreating;
    bool isAttacking;
    bool ifChange = false;
    float retreatDistance = 0f;
    int attackPlayer = 1;
   

    // Use this for initialization
    void Start()
    {
        isChasing = true; isRetreating = false; isAttacking = false;
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
        start_x = enemy.transform.position.x;
        start_y = enemy.transform.position.y;
        start_z = enemy.transform.position.z;
        anim.SetBool("ifChase", true);
        anim.SetBool("ifRetreat", false);
        anim.SetBool("ifAttack", false);
    }

    void flip()
    {

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (curhealth == 0)
        {
            Destroy(gameObject);
        }

        //If player is near then attack, else retreat or chase according to what ifChasing is.
        if (Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 3f && Mathf.Abs(player.transform.position.x - enemy.transform.position.x) <= 1.2f)
        {
            isAttacking = true;
            anim.SetBool("ifAttack", true);
            isChasing = false;
        }
        else
        {
            isAttacking = false;
            isRetreating = true;
        }

        //Flip as per the position of the player.
        if (Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 1f || 
            Mathf.Abs(player.transform.position.y - enemy.transform.position.y) >= 2f) {
            if (player.transform.position.x < enemy.transform.position.x && !ifChange)
            {
                ifChange = true;
                flip();
            }
            else if (player.transform.position.x > enemy.transform.position.x && ifChange == true)
            {
                ifChange = false;
                flip();
            }
        }

        //Either Attack-----Or (chase or retreat).
        if (isAttacking)
        {
          
            anim.SetBool("ifChase", false);
            anim.SetBool("ifRetreat", false);
            anim.SetBool("ifAttack", true);
            Attack();
        }
        else
        {
            if (isChasing)
            {
                attackPlayer = 1;
                anim.SetBool("ifChase", true);
                anim.SetBool("ifRetreat", false);
                anim.SetBool("ifAttack", false);
                Chase();
            }
            else if (isRetreating)
            {
                attackPlayer = 1;
                anim.SetBool("ifRetreat", true);
                anim.SetBool("ifChase", false);
                anim.SetBool("ifAttack", false);
                Retreat();
            }
        }
    }

    public void updateHealth()
    {
        curhealth--;
    }

    void Chase()
    {

        status = "chasing";
        //If player is right --- Go to right
        if (player.transform.position.x > enemy.transform.position.x)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x + speed, start_y, start_z);
        }
        //If player is left --- Go to left
        else if (player.transform.position.x <= enemy.transform.position.x)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x - speed, start_y, start_z);
        }
    }

    void Attack()
    {
        status = "attacking";
        //Stands and attacks *** can be modified ***
        if (player.transform.position.x > enemy.transform.position.x)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x + speed, start_y, start_z);
        }
        //If player is left --- Go to left
        else if (player.transform.position.x <= enemy.transform.position.x)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x - speed, start_y, start_z);
        }
       
    }


    void Retreat()
    {
        status = "retreating";
        //if player is right --- go left
        retreatDistance += speed; //Distance travelled by the enemy

        if (retreatDistance >= 6f) //if retreated enough . . stop retreating chase again
        {
            isRetreating = false;
            isChasing = true;
            retreatDistance = 0f;

        }
        else
        {
            if (player.transform.position.x > enemy.transform.position.x)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x - speed, start_y, start_z);
            }
            //if player is left --- go right
            else if (player.transform.position.x <= enemy.transform.position.x)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x + speed, start_y, start_z);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        
        if (obj.gameObject.tag == "Player")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack2") && attackPlayer!=0)// == 1)
                
            {
                player.GetComponent<Health>().health--;
                attackPlayer = 0;
            }
        }

        if (obj.gameObject.tag == "Bullet")
        {
            updateHealth();
            Destroy(obj.gameObject);
        }
    }
}
