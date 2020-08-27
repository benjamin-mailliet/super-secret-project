using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CombinaisonManager : MonoBehaviour
{

    private static string[] xboxTouches = { "A", "B", "X", "Y" };

    public List<int> currentCombinaison;

    private Gamepad gamepad;


    // Start is called before the first frame update
    void Start()
    {
        reinitCombinaison();
    }

    // Update is called once per frame
    void Update()
    {
        /*gamepad = Gamepad.current;
        if (gamepad == null) {
            foreach(Gamepad gamepadCurrent in Gamepad.all){
                Debug.Log("Gamepad " + gamepadCurrent.displayName);
                Debug.Log("Index " + gamepadCurrent.device);
            }
            Debug.LogError("Gamepad non reconnu");
            return; // No gamepad connected.
        }
        checkPlayerInput();
        if(currentCombinaison.Count < 1) {
            Debug.Log("YOU ARE TOO STRONG FOR ME !");
            reinitCombinaison();
        }*/
    }

    List<int> makeNewCombinaison() {
        return  new List<int> { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
    }

    void reinitCombinaison() {
        currentCombinaison = makeNewCombinaison();
        updateUICurrentCombinaison();
    }

    void updateUICurrentCombinaison() {
        string currentCombinaisonText = "";
        for(int i = 0; i < currentCombinaison.Count; i++) {
            currentCombinaisonText = currentCombinaisonText + xboxTouches[currentCombinaison[i]];
            if(i < currentCombinaison.Count - 1) {
                currentCombinaisonText = currentCombinaisonText + " - ";
            }
        }
        GameObject.Find("CombinaisonToType").GetComponent<Text>().text = currentCombinaisonText;
    }

    void checkPlayerInput() {
        if (gamepad.aButton.wasPressedThisFrame) {
            Debug.Log("Player input Action1");
            if (currentCombinaison[0] == 0) {
                currentCombinaison.RemoveAt(0);
            } else {
                reinitCombinaison();
            }
        }
        if (gamepad.bButton.wasPressedThisFrame) {
            Debug.Log("Player input Action2");
            if (currentCombinaison[0] == 1) {
                currentCombinaison.RemoveAt(0);
            } else {
                reinitCombinaison();
            }
        }
        if (gamepad.xButton.wasPressedThisFrame) {
            Debug.Log("Player input Action3");
            if (currentCombinaison[0] == 2) {
                currentCombinaison.RemoveAt(0);
            } else {
                reinitCombinaison();
            }
        }
        if (gamepad.yButton.wasPressedThisFrame) {
            Debug.Log("Player input Action4");
            if (currentCombinaison[0] == 3) {
                currentCombinaison.RemoveAt(0);
            } else {
                reinitCombinaison();
            }
        }
        updateUICurrentCombinaison();
    }
}
