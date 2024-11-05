using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text timerText; // Referência ao texto da UI para o temporizador
    [SerializeField]
    private Transform player; // Referência ao jogador
    [SerializeField]
    private Transform cube; // Referência ao cubo
    [SerializeField]
    private Slider progressSlider; // Referência ao Slider de progresso
    [SerializeField]
    private float maxTime = 40f; // Tempo inicial do temporizador em segundos
    [SerializeField]
    private float holdTime = 3f; // Tempo necessário para manter o botão 4 pressionado

    private float currentTime;
    private bool isTimerRunning = true;
    private bool isHoldingButton = false;
    private float holdButtonStartTime;

    void Start()
    {
        currentTime = maxTime;
        UpdateTimerText(); // Atualiza o texto do temporizador no início do jogo
        StartCoroutine(TimerCountdown());

        // Configura a barra de progresso inicialmente como invisível
        progressSlider.gameObject.SetActive(false);
        progressSlider.value = 0;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float distanceToCube = Vector3.Distance(player.position, cube.position);

            if (distanceToCube <= 2f) // Ajuste a distância conforme necessário
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

                    // Verifica se o tempo necessário foi alcançado
                    if (progress >= 1f)
                    {
                        StopTimer(); // Para o temporizador
                        progressSlider.gameObject.SetActive(false); // Oculta a barra de progresso
                    }
                }
                else
                {
                    // Reseta o estado se o botão não estiver mais pressionado
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
