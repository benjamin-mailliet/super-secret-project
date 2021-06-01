using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TimeFlowManager : MonoBehaviour, PlayerActionAsset.IBulletTimeActions{

    public float slowdownFactor;
    private float fixedDeltaTime;
    private float bulletTimeDurationCountdown = -1;

    private float bulletTimeDuration = 2f;

    PlayerActionAsset playerActions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
        playerActions = new PlayerActionAsset();
        playerActions.BulletTime.SetCallbacks(this);
    }

    public void OnEnable() {
        Debug.Log("Enabling bullet time controls!");
        playerActions.BulletTime.Enable();
    }

    public void OnDisable() {
        Debug.Log("Disabling bullet time controls!");
        playerActions.BulletTime.Disable();
    }

    // Update is called once per frame
    void Update()
    {        
        if(bulletTimeDurationCountdown != -1) {
            GameObject.Find("timer").GetComponent<Image>().fillAmount = (bulletTimeDurationCountdown * (1/slowdownFactor)) / bulletTimeDuration;
            bulletTimeDurationCountdown -= Time.deltaTime;
            if (bulletTimeDurationCountdown < 0) {
                StopSlowMotion();
            }
        } else {
            GameObject.Find("timer").GetComponent<Image>().fillAmount = 1;
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    private void DoSlowMotion() {
        Debug.Log("Do Slow Motion");
        // Code that active bullet time
        if (Time.timeScale == 1.0f) {
            Time.timeScale = slowdownFactor;
            bulletTimeDurationCountdown = bulletTimeDuration * slowdownFactor;
        }
    }

    private void StopSlowMotion() {
        Debug.Log("Stop Slow Motion");
        if (Time.timeScale != 1.0f) {
            Time.timeScale = 1.0f;
            bulletTimeDurationCountdown = -1;
        }
    }

    public void OnBulletTime(InputAction.CallbackContext context) {
        if (context.ReadValue<float>() > 0.5) {
            DoSlowMotion();
        } else {
            StopSlowMotion();
        }
    }
}
