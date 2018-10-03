using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from a tower defense game I made a few years ago
public class ProjectileController : MonoBehaviour {

    float duration;
    public LayerMask playerLayer;

    double damage;
    int speed = 6;

    // Use this for initialization
    void Start () {
        duration = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if(duration<=0.0f)  //Deletes the projectile in case it misses and goes off screen
        {
            Destroy(gameObject);
        }

        //Determines collision and tells the hit enemies to take damage
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5, playerLayer);
        foreach (Collider2D c in colliders)
        {
            //if hit player, damage them and destroy the game object
        }
    }


    public void target (Vector3 destination, double d) //Sets the direction and stats of the projectile
    {
        //See comment on the type variable in TowerController
        damage = d;
        float angle = Mathf.Atan2(destination.y - transform.position.y, destination.x - transform.position.x);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*speed;
    }


}
