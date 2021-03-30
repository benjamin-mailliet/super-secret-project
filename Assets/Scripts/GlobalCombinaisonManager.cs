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


    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onEvent +=
        (eventPtr, device) =>
        {
            var gamepad = device as Gamepad;
            if (gamepad == null)
            {
                this.currentControlScheme = CONTROL_SCHEME.KEYBOARD;
                UpdateUIForEveryMonster();
            }
            else
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
                monster.GetComponent<CombinaisonManager>().inputCombinaison(1, this.currentControlScheme);
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
                monster.GetComponent<CombinaisonManager>().inputCombinaison(2, this.currentControlScheme);
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
                monster.GetComponent<CombinaisonManager>().inputCombinaison(3, this.currentControlScheme);
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
                monster.GetComponent<CombinaisonManager>().inputCombinaison(4, this.currentControlScheme);
            }
        }
    }

    public static string displayCombinaisonForControlScheme(int combinaison, CONTROL_SCHEME currentControlScheme)
    {
        if (currentControlScheme == CONTROL_SCHEME.GAMEPAD)
        {
            switch (combinaison)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "Y";
                case 4:
                    return "X";
                default:
                    return "";
            }
        }
        else
        {
            return combinaison + "";
        }
    }
}
