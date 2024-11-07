using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnInterval = 4f; // Intervalo de 4 segundos

    // Definindo os limites com base nas coordenadas fornecidas
    private Vector3 minPosition = new Vector3(1.08f, -0.62f, -65.528f);
    private Vector3 maxPosition = new Vector3(19.48f, -0.59f, -38.36f);

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemy.tag = "enemy"; // Define a tag "enemy" para o inimigo instanciado

            yield return new WaitForSeconds(spawnInterval); // Espera 4 segundos antes de instanciar o próximo inimigo
        }
    }
}
