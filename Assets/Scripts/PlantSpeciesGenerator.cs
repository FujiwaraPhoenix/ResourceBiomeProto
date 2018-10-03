using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpeciesGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //At minimum, one species must recover hunger.
    public void generateSpecies()
    {
        EdiblePlant e = new EdiblePlant();
        e.healthHealed = Random.Range(-10, 10);
        e.hungerRecovered = Random.Range(-10, 10);
        int randomVal = Random.Range(0, 8);
        switch (randomVal)
        {
            case 0:
                e.pE = EdiblePlant.possibleEffects.None;
                break;
            case 1:
                e.pE = EdiblePlant.possibleEffects.Poisoned;
                break;
            case 2:
                e.pE = EdiblePlant.possibleEffects.Paralyzed;
                break;
            case 3:
                e.pE = EdiblePlant.possibleEffects.Speedy;
                break;
            case 4:
                e.pE = EdiblePlant.possibleEffects.Slowed;
                break;
            case 5:
                e.pE = EdiblePlant.possibleEffects.Confused;
                break;
            case 6:
                e.pE = EdiblePlant.possibleEffects.Drunk;
                break;
            case 7:
                e.pE = EdiblePlant.possibleEffects.Hungry;
                break;
        }
        checkSpecies();
    }

    public void checkSpecies()
    {
        bool vetted = false;
        for (int i = 0; i < Controller.Instance.speciesList.Length; i++)
        {
            if (!vetted)
            {
                if (Controller.Instance.speciesList[i].hungerRecovered > 0)
                {
                    vetted = true;
                }
            }
        }
        if (!vetted)
        {
            int chosenPlant = Random.Range(0, 3);
            Controller.Instance.speciesList[chosenPlant].hungerRecovered = Random.Range(1, 10);
        }
    }
}
