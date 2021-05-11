using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour, IMonster
{
    private GameObject player;
    private PlayerCore playerCore;
    public GameObject explosion;
    private Vector2 target;

    private float speed = 5f;
    private float minErraticMoveStrength = 15f;
    private float maxErraticMoveStrength = 25f;
    private float erraticMoveFrequency = 0.8f;
    private float erraticMoveTimer = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        playerCore = player.GetComponent<PlayerCore>();
    }

    void Update()
    {
        makeErraticMovment();
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void makeErraticMovment()
    {
        if (erraticMoveTimer > 0)
        {
            erraticMoveTimer -= Time.deltaTime;
        }
        else
        {
            erraticMoveTimer = Random.Range(0f, erraticMoveFrequency);
            float strength = Random.Range(minErraticMoveStrength, maxErraticMoveStrength);
            float erraticValueX = Random.Range(-1 * strength, 1 * strength);
            float erraticValueY = Random.Range(-1 * strength, 1 * strength);
            target = new Vector2(player.transform.position.x + erraticValueX, player.transform.position.y + erraticValueY);
        }
    }

    public void killed()
    {
        Debug.Log("Destroy the fly");
        explosion = Instantiate(explosion, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
        Destroy(this.explosion, 1);
        playerCore.Heal(3);
        Destroy(this.gameObject);
    }
}
