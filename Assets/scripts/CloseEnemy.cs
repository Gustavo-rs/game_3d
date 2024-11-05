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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obt�m o NavMeshAgent
        agent.stoppingDistance = attackRange; // Define a dist�ncia m�nima de ataque
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
            Debug.Log("O CloseEnemy encostou em voc� e causou dano!");
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
