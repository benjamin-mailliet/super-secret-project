using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    Renderer renderer;
    PlayerHealth playerHealth;
    PlayerMovment playerMovment;
    private float hitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMovment = GetComponent<PlayerMovment>();
    }

    void Update()
    {
        this.hitTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster" && this.hitTimer <= 0)
        {
            playerHealth.hit(10);
            playerMovment.hit(transform.position.x < other.transform.position.x ? -1 : 1);
            this.hitTimer = 2f;
            StartCoroutine("blink");
        }
    }

    private IEnumerator blink()
    {
        while (this.hitTimer > 0)
        {
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        renderer.enabled = true;
    }
}
