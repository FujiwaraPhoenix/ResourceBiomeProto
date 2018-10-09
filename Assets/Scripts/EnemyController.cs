using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from the Random Event Prototype
//Whatever generates NPCs should assign the player with setTarget(gameobject player), and the player should have the "Player" tag and the Player layer. If that causes problems let me know, I can change it.
//NPCs will destroy themselves when they run out of health. If enemies are going to be kept in lists, then I can change it so that it uses isAlive() instead, so whatever manages the lists can destroy it from there
//Enemies that deal contact damage will deal it once per collision
public class EnemyController : MonoBehaviour {

    Vector2 inputVector; //The direction it moves in
    Rigidbody2D rb; //The rigidbody

    public ProjectileController shot;

    public float maxHealth = 30; //How much health the NPC should start with and regen up to, if NPCs can regen
    float health; //How much health the NPC has
    bool poisoned = false; //Whether or not the NPC is poisoned. Holdover from the last project, might be useful
    bool alive = true; //Whether the npc is dead and should be removed

    public bool hostile = false; //Whether or not the enemy chases the player. I might set it up so that this changes if the player attacks the npc
    public int aggroRange = 5; //How close the player has to be for the enemy to chase them
    public int damage = 5; //How much damage the enemy does per collision
    public float speedMult = 2f; //Affects how fast the enemy moves

    public bool shooty = false; //Whether or not the NPC has a ranged attack
    public float shootCooldown = 2;
    public float shotTimer = 0;

    public bool wanderer = true; //Whether or not the NPC will wander around when not near the player
    bool wandering = false; //Whether or not the NPC is wandering around
    float wanderTimer = 0; //Timer for wandering purposes
    public float wanderTimerMax = 4; //Maximum wandering time
    public float wanderTimerMin = 0.5f; //Minimum wandering time
    public float wanderCooldownMax = 5; //Maximum time waiting between wandering
    public float wanderCooldownMin = 1; //Minimum time waiting between wandering
    float wanderCooldown = 5; //How long this period of waiting should last
    float wanderTime; //How long this period of wandering should last
    Vector2 wanderVector; //The direction the NPC wanders in

    public GameObject target; //The player

    SpriteRenderer sr;


    void Start() {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update() {

        if (hostile && Vector3.Distance(target.transform.position, transform.position) < aggroRange) // If the player is within range, chase it
        {
            wandering = false;
            wanderCooldown = Random.Range(wanderTimerMin, wanderTimerMax); //If the player goes out of aggro range, will wait before wandering around again
            inputVector = (target.transform.position - transform.position);
            if(shooty) //If the NPC has a ranged attack, it will use it on a cooldown
            {
                if(shotTimer >= shootCooldown)
                {
                    shotTimer = 0;
                    ProjectileController p = Instantiate(shot, transform.position + new Vector3(inputVector.x * .1f, inputVector.y * .1f, 0), Quaternion.identity);
                    p.target(target.transform.position, damage);
                }
                else
                {
                    shotTimer += Time.deltaTime;
                }
            }
        }
        else if (wanderer)
        {
            if (wandering) //If wandering, move in the wandering direction
            {
                inputVector = wanderVector;
                wanderTimer += Time.deltaTime;
                if(wanderTimer >= wanderTime) //If the timer runs out, set to stay still for a random amount of time
                {
                    wanderTimer = 0;
                    wanderVector = new Vector2(0, 0);
                    wandering = false;
                    wanderCooldown = Random.Range(wanderCooldownMin, wanderCooldownMax);
                }
            }
            else //Stay still for a while

            {
                inputVector = new Vector2(0, 0);
                wanderTimer += Time.deltaTime;
                if(wanderTimer >= wanderCooldown) //If the timer runs out, set a random wander direction and how long it should wander that way
                {
                    wanderTimer = 0;
                    wanderVector = Random.insideUnitCircle;
                    wandering = true;
                    wanderTime = Random.Range(wanderTimerMin, wanderTimerMax);
                }
            }
        }
        else //If it's not a wanderer and not chasing the player, stay still
        {
            inputVector = new Vector2(0, 0);
        }


        if (poisoned) //If poisoned, lose 3 health per second
        {
            health -= Time.deltaTime * 3;
        }
        sr.color = new Color(1, health / 30, health / 30);

        if (health <= 0) //If health runs out, remove the gameobject
        {
            alive = false;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() //For movement purposes
    {
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
        }
        rb.velocity = inputVector * speedMult;
    }

    public void setTarget(GameObject t) //Sets the player so the NPC knows how to chase it
    {
        target = t;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            target.GetComponent<EverythingPlayer>().getHit(damage);
        }
    }

    public void damaged(int damage) //Damage the NPC by this amount
    {
        health -= damage;
        hostile = true; //Non hostile NPCs will fight back if damaged
    }

    public bool aliveCheck() //Returns whether or not the NPC is alive. Used if the NPC has to be destroyed from another script, like if it has to be removed from a list first.
    {
        return alive;
    }

    public void reset() //Holdover from the last prototype, likely won't be needed
    {
        damage = 5;
        aggroRange = 5;
    }
}
