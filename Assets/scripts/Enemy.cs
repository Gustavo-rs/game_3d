using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float shootRange = 50f;
    [SerializeField]
    float minShootInterval = 10f; // Define o intervalo m�nimo em segundos
    [SerializeField]
    float maxShootInterval = 20f; // Define o intervalo m�ximo em segundos

    private float nextShootTime; // Tempo do pr�ximo tiro

    private Player playerScript; // Refer�ncia ao script do jogador

    void Start()
    {
        Time.timeScale = 1f; // Garante que o tempo est� na escala normal
        SetNextShootTime(); // Define o primeiro intervalo de tiro

        // Obt�m o componente Player do objeto Transform do jogador
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            AttemptShoot();
            SetNextShootTime(); // Redefine o pr�ximo intervalo de tiro
        }
    }

    private void SetNextShootTime()
    {
        float waitTime = Random.Range(minShootInterval, maxShootInterval);
        Debug.Log("Esperando " + waitTime + " segundos antes do pr�ximo tiro.");
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
                    Debug.Log("O inimigo acertou voc�!");

                    // Verifica se o playerScript n�o � nulo antes de chamar TakeDamage
                    if (playerScript != null)
                    {
                        playerScript.TakeDamage(1); // Reduz 1 ponto de vida do jogador
                    }
                }
                else
                {
                    Debug.Log("O tiro foi bloqueado por um obst�culo.");
                }
            }
        }
    }
}
