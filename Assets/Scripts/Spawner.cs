using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnTime = 5;
    private GameObject fly;

    void Start()
    {
        fly = Resources.Load("Fly") as GameObject;
        InvokeRepeating("Spawn", 2f, spawnTime);
    }

    private void Spawn()
    {
        int x = Random.Range(-5, 5);
        int y = Random.Range(-5, 5);
        Instantiate(fly, new Vector2(x, y), Quaternion.identity);
    }
}
