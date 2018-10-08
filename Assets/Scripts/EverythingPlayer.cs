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

    public float playerSpeed = 4f;





        //SET ANY OF THE BOOLS TO TRUE TO ACTIVATE THE EFFECTS
        //ENERGY AND SPEED WORK TOGETHER



	void Start () {
		
	}
	

	void FixedUpdate () {

        healthtxt.text = "Health: " + health.ToString();
        energyText.text = "Energy: " + energy.ToString();
        speedText.text = "Speed: " + playerSpeed.ToString();
        if (energy > 0)
        {
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            GetComponent<Rigidbody2D>().velocity = targetVelocity * playerSpeed;
        }
		

        //viseffect test
        if (Input.GetKeyDown(KeyCode.T))
        {
            health = health - 1;
            getDamage = true;
            
        }

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

        if (Input.GetKeyDown(KeyCode.Y) && playerSpeed <= energy)
        {
            playerSpeed = playerSpeed + 1f;
            getSpeed = true;

        }

        if (energy < playerSpeed)
        {
            playerSpeed = energy;
        }

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


}
