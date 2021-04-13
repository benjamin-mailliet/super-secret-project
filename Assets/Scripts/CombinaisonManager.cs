using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;

using static GlobalCombinaisonManager.ACTION_INPUT;
using static GlobalCombinaisonManager;
using System;
using Random = UnityEngine.Random;

public class CombinaisonManager : MonoBehaviour {

    public List<ACTION_INPUT> currentCombinaison;

    private GameObject explosion;

    private bool combinaisonFinished = false;

    void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        explosion = Resources.Load("Arcade Spark") as GameObject;
        reinitCombinaison();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentCombinaison.Count < 1 && !combinaisonFinished) {
            combinaisonFinished = true;
            Debug.Log("Destroy the fly");
            explosion = Instantiate(explosion, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
            Destroy(this.explosion, 1);
            Destroy(this.gameObject);            
        }
    }

    List<ACTION_INPUT> makeNewCombinaison() {

        if (this.gameObject.name.Contains("Fly")) {
            return new List<ACTION_INPUT> { X_4, GAUCHE, B_2, DROITE, Y_3,  HAUT, A_1, BAS};
        } else {
            return new List<ACTION_INPUT> { 
                (ACTION_INPUT) Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7)),
                (ACTION_INPUT)Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7)),
                (ACTION_INPUT)Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7)),
                (ACTION_INPUT)Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7)),
                (ACTION_INPUT)Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7)),
                (ACTION_INPUT)Enum.GetValues(typeof(ACTION_INPUT)).GetValue(Random.Range(0, 7))
            };
        }
    }

    void reinitCombinaison(CONTROL_SCHEME currentControlScheme = CONTROL_SCHEME.GAMEPAD) {
        currentCombinaison = makeNewCombinaison();
        updateUICurrentCombinaison(currentControlScheme);
    }

    public void updateUICurrentCombinaison(CONTROL_SCHEME currentControlScheme) {
        string currentCombinaisonText = "";
        for(int i = 0; i < currentCombinaison.Count; i++) {
            currentCombinaisonText = currentCombinaisonText + GlobalCombinaisonManager.displayCombinaisonForControlScheme(currentCombinaison[i], currentControlScheme);
            if(i < currentCombinaison.Count - 1) {
                currentCombinaisonText = currentCombinaisonText + " - ";
            }
        }
        this.gameObject.GetComponentInChildren<TextMesh>().text = currentCombinaisonText;
    }

    
    public void inputCombinaison(ACTION_INPUT actionInput, CONTROL_SCHEME currentControlScheme)
    {
        if (currentCombinaison[0] == actionInput)
        {
            currentCombinaison.RemoveAt(0);
            updateUICurrentCombinaison(currentControlScheme);
        }
        else
        {
            reinitCombinaison(currentControlScheme);
        }
    }

    

}
