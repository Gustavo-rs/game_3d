using System.Collections;
using UnityEngine;

public class SmokeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject smokePrefab; // Prefab da smoke
    [SerializeField]
    private float throwForce = 10f; // Força do arremesso
    [SerializeField]
    private float smokeDuration = 5f; // Duração da fumaça
    [SerializeField]
    private float launchOffset = 1f; // Distância à frente do jogador para o lançamento

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowSmoke();
        }
    }

    private void ThrowSmoke()
    {
        // Define a posição inicial da smoke um pouco à frente e acima do jogador
        Vector3 spawnPosition = transform.position + transform.forward * launchOffset + Vector3.up * 1f;

        // Instancia a smoke na posição ajustada
        GameObject smoke = Instantiate(smokePrefab, spawnPosition, Quaternion.identity);

        // Adiciona uma força para lançar a smoke
        Rigidbody rb = smoke.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        // Inicia a corrotina para ativar a fumaça após um tempo
        StartCoroutine(ActivateSmoke(smoke));
    }

    private IEnumerator ActivateSmoke(GameObject smoke)
    {
        yield return new WaitForSeconds(1f); // Aguarda um segundo para a smoke atingir o solo

        // Ativa o sistema de partículas
        ParticleSystem ps = smoke.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        // Desativa a smoke após a duração definida
        yield return new WaitForSeconds(smokeDuration);
        Destroy(smoke); // Destrói o objeto da smoke
    }
}
