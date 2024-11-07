using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CloseEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Referência ao Transform do jogador
    [SerializeField]
    private float attackRange = 1.5f; // Distância mínima para o ataque
    [SerializeField]
    private float damageInterval = 2f; // Intervalo entre danos quando encostar no jogador

    private NavMeshAgent agent; // Referência ao NavMeshAgent do inimigo
    private bool isAttacking = false; // Indica se o inimigo está causando dano

    private Player playerScript; // Referência ao script do jogador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtém o NavMeshAgent
        agent.stoppingDistance = attackRange; // Define a distância mínima de ataque

        // Obtém o componente Player no Transform do jogador
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        // Define o destino do agente como a posição do jogador
        agent.SetDestination(player.position);

        // Verifica se o inimigo está próximo o suficiente para atacar
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
            // Verifica a distância do jogador a cada intervalo de dano
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                if (playerScript != null)
                {
                    playerScript.TakeDamage(1); // Causa 1 ponto de dano ao jogador
                    Debug.Log("O CloseEnemy encostou em você e causou dano!");
                }
            }
            else
            {
                // Para de atacar se o jogador sair do alcance
                isAttacking = false;
                Debug.Log("O CloseEnemy parou de atacar pois o jogador está fora do alcance.");
            }

            yield return new WaitForSeconds(damageInterval); // Aguarda o intervalo de dano
        }
    }
}
