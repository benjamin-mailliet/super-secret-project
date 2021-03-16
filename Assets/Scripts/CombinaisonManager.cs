using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CombinaisonManager : MonoBehaviour, PlayerActionAsset.ICombinaisonActions {

    public List<int> currentCombinaison;

    private Gamepad gamepad;

    private PlayerActionAsset playerActionAsset;

    void Awake() {
        playerActionAsset = new PlayerActionAsset();
        playerActionAsset.Combinaison.SetCallbacks(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        reinitCombinaison();
    }

    public void OnEnable() {
        Debug.Log("Enabling bullet time controls!");
        playerActionAsset.Combinaison.Enable();
    }

    public void OnDisable() {
        Debug.Log("Disabling bullet time controls!");
        playerActionAsset.Combinaison.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCombinaison.Count < 1) {
            Debug.Log("YOU ARE TOO STRONG FOR ME !");
            reinitCombinaison();
        }
    }

    List<int> makeNewCombinaison() {
        return  new List<int> { Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5) };
    }

    void reinitCombinaison() {
        currentCombinaison = makeNewCombinaison();
        updateUICurrentCombinaison();
    }

    void updateUICurrentCombinaison() {
        string currentCombinaisonText = "";
        for(int i = 0; i < currentCombinaison.Count; i++) {
            currentCombinaisonText = currentCombinaisonText + currentCombinaison[i];
            if(i < currentCombinaison.Count - 1) {
                currentCombinaisonText = currentCombinaisonText + " - ";
            }
        }
        GameObject.Find("CombinaisonToType").GetComponent<Text>().text = currentCombinaisonText;
    }

    
    public void On_1_BoutonSud(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Player input Action1");
            if (currentCombinaison[0] == 1) {
                currentCombinaison.RemoveAt(0);
                updateUICurrentCombinaison();
            } else {
                reinitCombinaison();
            }
        }
    }

    public void On_2_BoutonEst(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Player input Action2");
            if (currentCombinaison[0] == 2) {
                currentCombinaison.RemoveAt(0);
                updateUICurrentCombinaison();
            } else {
                reinitCombinaison();
            }
        }
    }

    public void On_3_BoutonNord(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Player input Action3");
            if (currentCombinaison[0] == 3) {
                currentCombinaison.RemoveAt(0);
                updateUICurrentCombinaison();
            } else {
                reinitCombinaison();
            }
        }
    }

    public void On_4_BoutonOuest(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Player input Action4");
            if (currentCombinaison[0] == 4) {
                currentCombinaison.RemoveAt(0);
                updateUICurrentCombinaison();
            } else {
                reinitCombinaison();
            }
        }
    }
}
