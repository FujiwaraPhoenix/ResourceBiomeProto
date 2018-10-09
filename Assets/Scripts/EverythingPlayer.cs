using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof (Rigidbody2D))]
public class EverythingPlayer : MonoBehaviour {
    public static bool getDamage = false;
    public static bool getHealth = false;
    public static bool getSpeed = false;
    public static bool getEnergy = false;
    public int health = 20;
    public Text energyText;
    public Text speedText;
    public int energy = 100;
    public int visibility;
    public Text healthtxt;
    public SpriteRenderer bloodImage;
    public SpriteRenderer healthImage;
    public SpriteRenderer speedImage;
    public SpriteRenderer energyImage;
    public bool testing = false;

    public float playerSpeed = 4f;

    //Variables for food that you've eaten;
    //In order: N/A, poisoned, paralyzed, fast, slow, confused, drunk, hungry.
    public bool[] possibleStatuses = new bool[8];
    public float[] timeLeft = new float[8];



    //SET ANY OF THE BOOLS TO TRUE TO ACTIVATE THE EFFECTS
    //ENERGY AND SPEED WORK TOGETHER



    void Start () {
        //Make sure that, on startup, all the statuses are not active.
        for (int i = 0; i < timeLeft.Length; i++)
        {
            possibleStatuses[i] = false;
        }
    }
	

	void FixedUpdate () {

        healthtxt.text = "Health: " + health.ToString();
        energyText.text = "Energy: " + energy.ToString();
        speedText.text = "Speed: " + playerSpeed.ToString();

        movePlayer();
        statusDetails();
        if (testing)
        {
            testFunctions();
        }

        //This is for checking if statuses are still live after a set amount of time. Make feedback for this, please.
        for (int i = 0; i < timeLeft.Length; i++)
        {
            if (timeLeft[i] <= 0)
            {
                possibleStatuses[i] = false;
            }
            timeLeft[i] -= Time.deltaTime;
        }

        consumeFood();
    }

    public void statusDetails()
    {
        //damage/blood
        if (getDamage)
        {
            Color Opaque = new Color(255, 0, 0, 1);
            bloodImage.color = Color.Lerp(bloodImage.color, Opaque, 10 * Time.deltaTime);
            if (bloodImage.color.a >= 0.8)
            {
                getDamage = false;
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(255, 0, 0, 0);
            bloodImage.color = Color.Lerp(bloodImage.color, Transparent, 10 * Time.deltaTime);
        }

        //health++ healing
        if (getHealth)
        {
            Color Opaque = new Color(0, 255, 0, 1);
            healthImage.color = Color.Lerp(healthImage.color, Opaque, 10 * Time.deltaTime);
            if (healthImage.color.a >= 0.8)
            {
                getHealth = false;
            }
        }
        if (!getHealth)
        {
            Color Transparent = new Color(0, 255, 0, 0);
            healthImage.color = Color.Lerp(healthImage.color, Transparent, 10 * Time.deltaTime);
        }

        //energy? whatever this could be
        if (getEnergy)
        {
            Color Opaque = new Color(255, 255, 0, 1);
            energyImage.color = Color.Lerp(energyImage.color, Opaque, 10 * Time.deltaTime);
            if (energyImage.color.a >= 0.8)
            {
                getEnergy = false;
            }
        }
        if (!getEnergy)
        {
            Color Transparent = new Color(255, 255, 0, 0);
            energyImage.color = Color.Lerp(energyImage.color, Transparent, 10 * Time.deltaTime);
        }
        //speed ffect
        if (getSpeed)
        {
            Color Opaque = new Color(0, 200, 255, 1);
            speedImage.color = Color.Lerp(speedImage.color, Opaque, 10 * Time.deltaTime);
            if (speedImage.color.a >= 0.8)
            {
                getSpeed = false;
            }
        }
        if (!getSpeed)
        {
            Color Transparent = new Color(0, 200, 255, 0);
            speedImage.color = Color.Lerp(speedImage.color, Transparent, 20 * Time.deltaTime);
        }
    }

    //Moves the player around; player can use WASD or arrow keys to move around. When player moves with either of these, energy-=1.
    public void movePlayer()
    {
        if (energy > 0)
        {
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            GetComponent<Rigidbody2D>().velocity = targetVelocity * playerSpeed;
        }
        
        //viseffect test
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            health = health - 1;
            getDamage = true;
            
        }*/

        if (Input.GetKeyDown(KeyCode.W))
        {
            energy = energy - 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            energy = energy - 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            energy = energy - 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            energy = energy - 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            energy = energy - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            energy = energy - 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            energy = energy - 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            energy = energy - 1;
        }

        if (energy < playerSpeed)
        {
            playerSpeed = energy;
        }

        //Quote from Oscar: I have no idea what this does; is this necessary? Someone else check.
        if (Input.GetKeyDown(KeyCode.Y) && playerSpeed <= energy)
        {
            playerSpeed = playerSpeed + 1f;
            getSpeed = true;
        }
    }

    public void testFunctions()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            health = health + 1;
            getHealth = true;

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            energy = energy + 10;
            getEnergy = true;

        }
    }

    //If the player hits the spacebar while on top of an edible plant, eat the plant, then apply the effects to the player.

    public void consumeFood()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] checkForItem = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
            foreach (Collider2D itemCollider in checkForItem)
            {
                if (itemCollider.GetComponent<EdiblePlant>() != null)
                {
                    EdiblePlant e = itemCollider.GetComponent<EdiblePlant>();
                    health += e.healthHealed;
                    energy += e.hungerRecovered;

                    //If th eplant has an effect, flip it on.
                    switch (e.pE)
                    {
                        case EdiblePlant.possibleEffects.None:
                            possibleStatuses[0] = true;
                            timeLeft[0] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Poisoned:
                            possibleStatuses[1] = true;
                            timeLeft[1] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Paralyzed:
                            possibleStatuses[2] = true;
                            timeLeft[2] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Speedy:
                            possibleStatuses[3] = true;
                            timeLeft[3] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Slowed:
                            possibleStatuses[4] = true;
                            timeLeft[4] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Confused:
                            possibleStatuses[5] = true;
                            timeLeft[5] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Drunk:
                            possibleStatuses[6] = true;
                            timeLeft[6] = 5f;
                            break;
                        case EdiblePlant.possibleEffects.Hungry:
                            possibleStatuses[7] = true;
                            timeLeft[7] = 5f;
                            break;
                    }
                    Destroy(e.gameObject);
                }
            }
        }
    }
}
