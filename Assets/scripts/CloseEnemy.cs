using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CloseEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Refer�ncia ao Transform do jogador
    [SerializeField]
    private float attackRange = 1.5f; // Dist�ncia m�nima para o ataque
    [SerializeField]
    private float damageInterval = 2f; // Intervalo entre danos quando encostar no jogador

    private NavMeshAgent agent; // Refer�ncia ao NavMeshAgent do inimigo
    private bool isAttacking = false; // Indica se o inimigo est� causando dano

    private Player playerScript; // Refer�ncia ao script do jogador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obt�m o NavMeshAgent
        agent.stoppingDistance = attackRange; // Define a dist�ncia m�nima de ataque

        // Obt�m o componente Player no Transform do jogador
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        // Define o destino do agente como a posi��o do jogador
        agent.SetDestination(player.position);

        // Verifica se o inimigo est� pr�ximo o suficiente para atacar
        if (agent.remainingDistance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        while (isAttacking)
        {
            // Verifica a dist�ncia do jogador a cada intervalo de dano
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                if (playerScript != null)
                {
                    playerScript.TakeDamage(1); // Causa 1 ponto de dano ao jogador
                    Debug.Log("O CloseEnemy encostou em voc� e causou dano!");
                }
            }
            else
            {
                // Para de atacar se o jogador sair do alcance
                isAttacking = false;
                Debug.Log("O CloseEnemy parou de atacar pois o jogador est� fora do alcance.");
            }

            yield return new WaitForSeconds(damageInterval); // Aguarda o intervalo de dano
        }
    }
}
