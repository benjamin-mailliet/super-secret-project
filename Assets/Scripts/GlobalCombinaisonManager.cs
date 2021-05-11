using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalCombinaisonManager : MonoBehaviour, PlayerActionAsset.ICombinaisonActions
{
    private PlayerActionAsset playerActionAsset;

    private CONTROL_SCHEME currentControlScheme = CONTROL_SCHEME.GAMEPAD;

    public enum CONTROL_SCHEME
    {
        GAMEPAD,
        KEYBOARD
    };

    public enum ACTION_INPUT
    {
        A_1,
        B_2,
        Y_3,
        X_4,
        BAS,
        DROITE,
        HAUT,
        GAUCHE
    }


    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onEvent +=
        (eventPtr, device) =>
        {
            var gamepad = device as Gamepad;
            var mouse = device as Mouse;
            var keyboard = device as Keyboard;
            if (gamepad == null && mouse == null && this.currentControlScheme == CONTROL_SCHEME.GAMEPAD)
            {
                this.currentControlScheme = CONTROL_SCHEME.KEYBOARD;
                UpdateUIForEveryMonster();
            }
            else if(keyboard == null && mouse == null && this.currentControlScheme == CONTROL_SCHEME.KEYBOARD)
            {
                this.currentControlScheme = CONTROL_SCHEME.GAMEPAD;
                UpdateUIForEveryMonster();
            }
        };
    }

    private void UpdateUIForEveryMonster()
    {
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            monster.GetComponent<CombinaisonManager>().updateUICurrentCombinaison(this.currentControlScheme);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        playerActionAsset = new PlayerActionAsset();
        playerActionAsset.Combinaison.SetCallbacks(this);
    }

    public void OnEnable() {
        Debug.Log("Enabling combinaison controls!");
        playerActionAsset.Combinaison.Enable();
    }

    public void OnDisable() {
        Debug.Log("Disabling combinaison controls!");
        playerActionAsset.Combinaison.Disable();
    }

    public void On_1_BoutonSud(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale < 1.0f)
        {
            Debug.Log("Player input Action1 with " + context.control.device.displayName);
            foreach(GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
            {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.A_1, this.currentControlScheme);
            }
        }
    }

    public void On_2_BoutonEst(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale < 1.0f)
        {
            Debug.Log("Player input Action2 with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
            {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.B_2, this.currentControlScheme);
            }
        }
        
    }

    public void On_3_BoutonNord(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale < 1.0f)
        {
            Debug.Log("Player input Action3 with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
            {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.Y_3, this.currentControlScheme);
            }
        }
    }

    public void On_4_BoutonOuest(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale < 1.0f)
        {
            Debug.Log("Player input Action4 with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
            {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.X_4, this.currentControlScheme);
            }
        }
    }

    public static string displayCombinaisonForControlScheme(ACTION_INPUT combinaison, CONTROL_SCHEME currentControlScheme)
    {
        if (currentControlScheme == CONTROL_SCHEME.GAMEPAD)
        {
            switch (combinaison)
            {
                case ACTION_INPUT.A_1:
                    return "A";
                case ACTION_INPUT.B_2:
                    return "B";
                case ACTION_INPUT.Y_3:
                    return "Y";
                case ACTION_INPUT.X_4:
                    return "X";
                case ACTION_INPUT.BAS:
                    return "ba";
                case ACTION_INPUT.DROITE:
                    return "dr";
                case ACTION_INPUT.HAUT:
                    return "ha";
                case ACTION_INPUT.GAUCHE:
                    return "ga";
                default:
                    return "";
            }
        }
        else
        {
            switch (combinaison) {
                case ACTION_INPUT.A_1:
                    return "1";
                case ACTION_INPUT.B_2:
                    return "2";
                case ACTION_INPUT.Y_3:
                    return "3";
                case ACTION_INPUT.X_4:
                    return "4";
                case ACTION_INPUT.BAS:
                    return "ba";
                case ACTION_INPUT.DROITE:
                    return "fr";
                case ACTION_INPUT.HAUT:
                    return "ha";
                case ACTION_INPUT.GAUCHE:
                    return "ga";
                default:
                    return "";
            }
        }
    }

    public void OnBas(InputAction.CallbackContext context) {
        if (context.performed && Time.timeScale < 1.0f) {
            Debug.Log("Player input Bas with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster")) {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.BAS, this.currentControlScheme);
            }
        }
    }

    public void OnDroite(InputAction.CallbackContext context) {
        if (context.performed && Time.timeScale < 1.0f) {
            Debug.Log("Player input Droite with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster")) {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.DROITE, this.currentControlScheme);
            }
        }
    }

    public void OnHaut(InputAction.CallbackContext context) {
        if (context.performed && Time.timeScale < 1.0f) {
            Debug.Log("Player input Haut with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster")) {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.HAUT, this.currentControlScheme);
            }
        }
    }

    public void OnGauche(InputAction.CallbackContext context) {
        if (context.performed && Time.timeScale < 1.0f) {
            Debug.Log("Player input Gauche with " + context.control.device.displayName);
            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster")) {
                monster.GetComponent<CombinaisonManager>().inputCombinaison(ACTION_INPUT.GAUCHE, this.currentControlScheme);
            }
        }
    }
}
