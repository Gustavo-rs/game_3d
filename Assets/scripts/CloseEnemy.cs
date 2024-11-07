using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CloseEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField]
    private float damageInterval = 2f;

    private NavMeshAgent agent;
    private bool isAttacking = false;

    private Player playerScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;

        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }

        // Verifica se o agente está em um NavMesh
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("CloseEnemy não está posicionado no NavMesh!");
        }
    }

    void Update()
    {
        // Verifica se o agente está em um NavMesh antes de definir o destino
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);

            if (agent.remainingDistance <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            Debug.LogWarning("O agente não está no NavMesh.");
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        while (isAttacking)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                if (playerScript != null)
                {
                    playerScript.TakeDamage(1);
                    Debug.Log("O CloseEnemy encostou em você e causou dano!");
                }
            }
            else
            {
                isAttacking = false;
                Debug.Log("O CloseEnemy parou de atacar pois o jogador está fora do alcance.");
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
