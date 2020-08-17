using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFlowManager : MonoBehaviour
{

    public float slowdownFactor;
    private float fixedDeltaTime;

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
            Debug.Log("Bullet time activated");
            DoSlowMotion();
        }
        if (Input.GetButtonUp("Fire3")) {
            Debug.Log("Bullet time deactivated");
            StopSlowMotion();
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    private void DoSlowMotion() {
        if(Time.timeScale == 1.0f) {
            Time.timeScale = slowdownFactor;
        }        
    }

    private void StopSlowMotion() {
        if (Time.timeScale != 1.0f) {
            Time.timeScale = 1.0f;
        }
    }
}
