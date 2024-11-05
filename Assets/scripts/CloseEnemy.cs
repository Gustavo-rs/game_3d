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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtém o NavMeshAgent
        agent.stoppingDistance = attackRange; // Define a distância mínima de ataque
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
            Debug.Log("O CloseEnemy encostou em você e causou dano!");
            yield return new WaitForSeconds(damageInterval); // Aguarda o intervalo de dano
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Para de causar dano se o jogador se afastar
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
