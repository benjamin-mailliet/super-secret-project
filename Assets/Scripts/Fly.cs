using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    GameObject player;
    private float speed = 4f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
    }
}
