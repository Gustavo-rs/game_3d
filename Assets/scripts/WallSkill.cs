using System.Collections;
using UnityEngine;

public class WallSkill : Skill
{
    private GameObject wallPrefab; // Prefab da parede
    [SerializeField]
    private float wallSpeed = 3.5f; // Velocidade de movimento da parede
    [SerializeField]
    private float maxDistance = 10f; // Distância máxima antes de desaparecer

    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Método para definir o prefab da parede
    public void SetWallPrefab(GameObject prefab)
    {
        wallPrefab = prefab;
    }

    public override void Activate()
    {
        if (wallPrefab != null)
        {
            StartCoroutine(SpawnAndMoveWall());
        }
        else
        {
            Debug.LogWarning("WallPrefab não está definido!");
        }
    }

    private IEnumerator SpawnAndMoveWall()
    {
        // Define a posição inicial e a direção fixa da parede no momento em que a habilidade é ativada
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f;
        Quaternion spawnRotation = Quaternion.LookRotation(cameraTransform.forward);
        Vector3 fixedDirection = cameraTransform.forward; // Direção inicial fixada

        // Instancia a parede na posição inicial e na direção fixa
        GameObject wallInstance = Instantiate(wallPrefab, spawnPosition, spawnRotation);

        float traveledDistance = 0f;

        // Move a parede na direção fixa capturada no momento da instanciação
        while (traveledDistance < maxDistance)
        {
            float moveStep = wallSpeed * Time.deltaTime;
            wallInstance.transform.position += fixedDirection * moveStep;
            traveledDistance += moveStep;
            yield return null;
        }

        // Destrói a parede após atingir a distância máxima
        Destroy(wallInstance);
    }
}
