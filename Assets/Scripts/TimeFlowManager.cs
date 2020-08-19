using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlowManager : MonoBehaviour
{

    public float slowdownFactor;
    private float fixedDeltaTime;
    private float bulletTimeDurationCountdown = -1;

    private float bulletTimeDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) {
            DoSlowMotion();
        }
        if (Input.GetButtonUp("Fire3")) {
            StopSlowMotion();
        }
        
        
        if(bulletTimeDurationCountdown != -1) {
            GameObject.Find("BulletTimeDuration").GetComponent<Text>().text = bulletTimeDurationCountdown.ToString();
            bulletTimeDurationCountdown -= Time.deltaTime;
            if (bulletTimeDurationCountdown < 0) {
                StopSlowMotion();
            }
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    private void DoSlowMotion() {
        Debug.Log("Bullet time activated");
        if (Time.timeScale == 1.0f) {
            Time.timeScale = slowdownFactor;
            bulletTimeDurationCountdown = bulletTimeDuration * slowdownFactor;
        }        
    }

    private void StopSlowMotion() {
        Debug.Log("Bullet time deactivated");
        if (Time.timeScale != 1.0f) {
            Time.timeScale = 1.0f;
            bulletTimeDurationCountdown = -1;
        }
    }
}
