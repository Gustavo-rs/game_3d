using System.Collections;
using UnityEngine;

public class WallSkill : Skill
{
    private GameObject wallPrefab; // Prefab da parede
    [SerializeField]
    private float wallSpeed = 3.5f; // Velocidade de movimento da parede
    [SerializeField]
    private float maxDistance = 10f; // Dist�ncia m�xima antes de desaparecer

    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // M�todo para definir o prefab da parede
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
            Debug.LogWarning("WallPrefab n�o est� definido!");
        }
    }

    private IEnumerator SpawnAndMoveWall()
    {
        // Define a posi��o inicial e a dire��o fixa da parede no momento em que a habilidade � ativada
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f;
        Quaternion spawnRotation = Quaternion.LookRotation(cameraTransform.forward);
        Vector3 fixedDirection = cameraTransform.forward; // Dire��o inicial fixada

        // Instancia a parede na posi��o inicial e na dire��o fixa
        GameObject wallInstance = Instantiate(wallPrefab, spawnPosition, spawnRotation);

        float traveledDistance = 0f;

        // Move a parede na dire��o fixa capturada no momento da instancia��o
        while (traveledDistance < maxDistance)
        {
            float moveStep = wallSpeed * Time.deltaTime;
            wallInstance.transform.position += fixedDirection * moveStep;
            traveledDistance += moveStep;
            yield return null;
        }

        // Destr�i a parede ap�s atingir a dist�ncia m�xima
        Destroy(wallInstance);
    }
}
