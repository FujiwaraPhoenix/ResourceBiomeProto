using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player pc;
    public int hp, hunger, hungerRate;

    //Variables for food that you've eaten;
    //In order: poisoned, paralyzed, fast, slow, confused, drunk, hungry.
    public bool[] possibleStatuses = new bool[7];
    public float[] timeLeft = new float[7];

    void Awake()
    {
        if (pc == null)
        {
            DontDestroyOnLoad(gameObject);
            pc = this;
        }
        else if (pc != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //Make sure that, on startup, all the statuses are not active.
        for (int i = 0; i < timeLeft.Length; i++)
        {
            possibleStatuses[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //This is for checking if statuses are still live after a set amount of time. Make feedback for this, please.
		for (int i = 0; i < timeLeft.Length; i++)
        {
            if (timeLeft[i] <= 0)
            {
                possibleStatuses[i] = false;
            }
            timeLeft[i] -= Time.deltaTime;
        }
	}
}
