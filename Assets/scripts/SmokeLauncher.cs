using System.Collections;
using UnityEngine;

public class SmokeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject smokePrefab; // Prefab da smoke
    [SerializeField]
    private float throwForce = 10f; // For�a do arremesso
    [SerializeField]
    private float smokeDuration = 5f; // Dura��o da fuma�a
    [SerializeField]
    private float launchOffset = 1f; // Dist�ncia � frente do jogador para o lan�amento

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowSmoke();
        }
    }

    private void ThrowSmoke()
    {
        // Define a posi��o inicial da smoke um pouco � frente e acima do jogador
        Vector3 spawnPosition = transform.position + transform.forward * launchOffset + Vector3.up * 1f;

        // Instancia a smoke na posi��o ajustada
        GameObject smoke = Instantiate(smokePrefab, spawnPosition, Quaternion.identity);

        // Adiciona uma for�a para lan�ar a smoke
        Rigidbody rb = smoke.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        // Inicia a corrotina para ativar a fuma�a ap�s um tempo
        StartCoroutine(ActivateSmoke(smoke));
    }

    private IEnumerator ActivateSmoke(GameObject smoke)
    {
        yield return new WaitForSeconds(1f); // Aguarda um segundo para a smoke atingir o solo

        // Ativa o sistema de part�culas
        ParticleSystem ps = smoke.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        // Desativa a smoke ap�s a dura��o definida
        yield return new WaitForSeconds(smokeDuration);
        Destroy(smoke); // Destr�i o objeto da smoke
    }
}
