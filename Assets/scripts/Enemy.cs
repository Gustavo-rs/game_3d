using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float shootRange = 50f;
    [SerializeField]
    float minShootInterval = 10f; // Define o intervalo mínimo em segundos
    [SerializeField]
    float maxShootInterval = 20f; // Define o intervalo máximo em segundos

    private float nextShootTime; // Tempo do próximo tiro

    void Start()
    {
        Time.timeScale = 1f; // Garante que o tempo está na escala normal
        SetNextShootTime(); // Define o primeiro intervalo de tiro
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            AttemptShoot();
            SetNextShootTime(); // Redefine o próximo intervalo de tiro
        }
    }

    private void SetNextShootTime()
    {
        float waitTime = Random.Range(minShootInterval, maxShootInterval);
        Debug.Log("Esperando " + waitTime + " segundos antes do próximo tiro.");
        nextShootTime = Time.time + waitTime;
    }

    private void AttemptShoot()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, player.position) <= shootRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, shootRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("O inimigo acertou você!");
                }
                else
                {
                    Debug.Log("O tiro foi bloqueado por um obstáculo.");
                }
            }
        }
    }
}
