using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    private GameObject healthbar;
    private Slider slider;

    void Start()
    {
        slider = transform.Find("Canvas/Healthbar").gameObject.GetComponent<Slider>();
        slider.maxValue = this.health;
    }

    void Update()
    {
        this.health -= Time.deltaTime;
        slider.value = this.health;
    }

    public void hit(int hitValue)
    {
        health -= hitValue;
    }

    public void Heal(int healValue)
    {
        health += healValue;
    }
}
