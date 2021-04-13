using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 10f;
    private float healthLossValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("HealthLoss", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.health);
    }

    private void HealthLoss()
    {
        this.health -= this.healthLossValue;
    }
}
