using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Transform player; 
    [SerializeField]
    private Transform cube; 
    [SerializeField]
    private Slider progressSlider; 
    [SerializeField]
    private float maxTime = 40f; 
    [SerializeField]
    private float holdTime = 3f;

    private float currentTime;
    private bool isTimerRunning = true;
    private bool isHoldingButton = false;
    private float holdButtonStartTime;

    [SerializeField]
    private Player playerScript;


    void Start()
    {
        currentTime = maxTime;
        UpdateTimerText();
        StartCoroutine(TimerCountdown());

        progressSlider.gameObject.SetActive(false);
        progressSlider.value = 0;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float distanceToCube = Vector3.Distance(player.position, cube.position);

            if (distanceToCube <= 2f)
            {
                if (Input.GetKey(KeyCode.Alpha4) && !playerScript.tookDamage)
                {
                    if (!isHoldingButton)
                    {
                        isHoldingButton = true;
                        holdButtonStartTime = Time.time;
                        progressSlider.gameObject.SetActive(true);
                    }

                    float progress = (Time.time - holdButtonStartTime) / holdTime;
                    progressSlider.value = Mathf.Clamp01(progress);

                    if (progress >= 1f)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
                        StopTimer();
                        progressSlider.gameObject.SetActive(false);

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                else
                {
                    isHoldingButton = false;
                    progressSlider.value = 0;
                    progressSlider.gameObject.SetActive(false);
                }
            }
            else
            {
                progressSlider.value = 0;
                progressSlider.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (isTimerRunning && currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                isTimerRunning = false;
                Debug.Log("Tempo esgotado!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
    }

    private void UpdateTimerText()
    {
        timerText.text = currentTime.ToString("F0") + "s";
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Temporizador parado!");
    }
}
