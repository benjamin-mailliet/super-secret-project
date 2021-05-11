using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;

using static GlobalCombinaisonManager.ACTION_INPUT;
using static GlobalCombinaisonManager;
using System;
using Random = UnityEngine.Random;

public class CombinaisonManager : MonoBehaviour
{

    public List<ACTION_INPUT> currentCombinaison;

    public IMonster monsterScript;

    public GameObject a1Button;
    public GameObject b2Button;
    public GameObject y3Button;
    public GameObject x4Button;
    public GameObject hautButton;
    public GameObject basButton;
    public GameObject gaucheButton;
    public GameObject droiteButton;

    private bool combinaisonFinished = false;

    void Awake()
    {
        monsterScript = GetComponent<IMonster>();
    }

    // Start is called before the first frame update
    void Start()
    {
        reinitCombinaison();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCombinaison.Count < 1 && !combinaisonFinished)
        {
            combinaisonFinished = true;
            if (monsterScript != null)
            {
                monsterScript.killed();
            }
        }
    }

    List<ACTION_INPUT> makeNewCombinaison()
    {

        if (this.gameObject.name.Contains("Fly"))
        {
            return new List<ACTION_INPUT> { X_4, GAUCHE, B_2, DROITE, Y_3, HAUT, A_1, BAS };
        }
        else
        {
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

    void reinitCombinaison(CONTROL_SCHEME currentControlScheme = CONTROL_SCHEME.GAMEPAD)
    {
        currentCombinaison = makeNewCombinaison();
        updateUICurrentCombinaison(currentControlScheme);
    }

    public void updateUICurrentCombinaison(CONTROL_SCHEME currentControlScheme)
    {
        foreach (Transform child in this.gameObject.transform.Find("VisualCombinaison"))
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < currentCombinaison.Count; i++)
        {
            instantiateButtonPrefab(i, currentControlScheme);
        }
    }

    private void instantiateButtonPrefab(int actionNumber, CONTROL_SCHEME currentControlScheme)
    {
        Debug.Log("Instantiate button Prefab");
        //TODO ajouter les touches de clavier
        switch (currentCombinaison[actionNumber])
        {
            case ACTION_INPUT.A_1:
                instantiateActionNumber(actionNumber, a1Button);
                break;
            case ACTION_INPUT.B_2:
                instantiateActionNumber(actionNumber, b2Button);
                break;
            case ACTION_INPUT.Y_3:
                instantiateActionNumber(actionNumber, y3Button);
                break;
            case ACTION_INPUT.X_4:
                instantiateActionNumber(actionNumber, x4Button);
                break;
            case ACTION_INPUT.HAUT:
                instantiateActionNumber(actionNumber, hautButton);
                break;
            case ACTION_INPUT.BAS:
                instantiateActionNumber(actionNumber, basButton);
                break;
            case ACTION_INPUT.GAUCHE:
                instantiateActionNumber(actionNumber, gaucheButton);
                break;
            case ACTION_INPUT.DROITE:
                instantiateActionNumber(actionNumber, droiteButton);
                break;
        }
    }

    private void instantiateActionNumber(int actionNumber, GameObject gameObject)
    {
        Debug.Log("Width : " + this.gameObject.transform.Find("VisualCombinaison").GetComponent<RectTransform>().rect.width);
        Debug.Log("X : " + this.gameObject.transform.Find("VisualCombinaison").position.x);
        Debug.Log("Position calculée : " + (this.gameObject.transform.Find("VisualCombinaison").position.x - this.gameObject.transform.Find("VisualCombinaison").GetComponent<RectTransform>().rect.width / 4 + actionNumber));

        Instantiate(gameObject, new Vector2(this.gameObject.transform.Find("VisualCombinaison").position.x - this.gameObject.transform.Find("VisualCombinaison").GetComponent<RectTransform>().rect.width / 4 + actionNumber, this.gameObject.transform.Find("VisualCombinaison").position.y), Quaternion.identity, this.gameObject.transform.Find("VisualCombinaison"));
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
