using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text timerText; // Refer�ncia ao texto da UI para o temporizador
    [SerializeField]
    private Transform player; // Refer�ncia ao jogador
    [SerializeField]
    private Transform cube; // Refer�ncia ao cubo
    [SerializeField]
    private Slider progressSlider; // Refer�ncia ao Slider de progresso
    [SerializeField]
    private float maxTime = 40f; // Tempo inicial do temporizador em segundos
    [SerializeField]
    private float holdTime = 3f; // Tempo necess�rio para manter o bot�o 4 pressionado

    private float currentTime;
    private bool isTimerRunning = true;
    private bool isHoldingButton = false;
    private float holdButtonStartTime;

    void Start()
    {
        currentTime = maxTime;
        UpdateTimerText(); // Atualiza o texto do temporizador no in�cio do jogo
        StartCoroutine(TimerCountdown());

        // Configura a barra de progresso inicialmente como invis�vel
        progressSlider.gameObject.SetActive(false);
        progressSlider.value = 0;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float distanceToCube = Vector3.Distance(player.position, cube.position);

            if (distanceToCube <= 2f) // Ajuste a dist�ncia conforme necess�rio
            {
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    if (!isHoldingButton)
                    {
                        isHoldingButton = true;
                        holdButtonStartTime = Time.time;
                        progressSlider.gameObject.SetActive(true); // Mostra a barra de progresso
                    }

                    // Calcula o progresso como uma porcentagem do tempo de espera
                    float progress = (Time.time - holdButtonStartTime) / holdTime;
                    progressSlider.value = Mathf.Clamp01(progress); // Atualiza o valor do Slider

                    // Verifica se o tempo necess�rio foi alcan�ado
                    if (progress >= 1f)
                    {
                        StopTimer(); // Para o temporizador
                        progressSlider.gameObject.SetActive(false); // Oculta a barra de progresso
                    }
                }
                else
                {
                    // Reseta o estado se o bot�o n�o estiver mais pressionado
                    isHoldingButton = false;
                    progressSlider.value = 0; // Reseta a barra de progresso
                    progressSlider.gameObject.SetActive(false); // Oculta a barra de progresso
                }
            }
            else
            {
                // Oculta a barra se o jogador estiver longe do cubo
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
            }
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = "Tempo: " + currentTime.ToString("F0") + "s";
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Temporizador parado!");
    }
}
