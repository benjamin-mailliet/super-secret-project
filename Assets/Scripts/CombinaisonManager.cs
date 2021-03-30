using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CombinaisonManager : MonoBehaviour {

    public List<int> currentCombinaison;

    void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {        
        reinitCombinaison();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentCombinaison.Count < 1) {
            Debug.Log("Destroy the fly");
            Destroy(this.gameObject);
        }
    }

    List<int> makeNewCombinaison() {
        return  new List<int> { Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5) };
    }

    void reinitCombinaison(GlobalCombinaisonManager.CONTROL_SCHEME currentControlScheme = GlobalCombinaisonManager.CONTROL_SCHEME.GAMEPAD) {
        currentCombinaison = makeNewCombinaison();
        updateUICurrentCombinaison(currentControlScheme);
    }

    public void updateUICurrentCombinaison(GlobalCombinaisonManager.CONTROL_SCHEME currentControlScheme) {
        string currentCombinaisonText = "";
        for(int i = 0; i < currentCombinaison.Count; i++) {
            currentCombinaisonText = currentCombinaisonText + GlobalCombinaisonManager.displayCombinaisonForControlScheme(currentCombinaison[i], currentControlScheme);
            if(i < currentCombinaison.Count - 1) {
                currentCombinaisonText = currentCombinaisonText + " - ";
            }
        }
        this.gameObject.GetComponentInChildren<TextMesh>().text = currentCombinaisonText;
    }

    
    public void inputCombinaison(int inputNumber, GlobalCombinaisonManager.CONTROL_SCHEME currentControlScheme)
    {
        if (currentCombinaison[0] == inputNumber)
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
