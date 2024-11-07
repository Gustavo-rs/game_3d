using System.Collections;
using UnityEngine;

public class WallSkill : Skill
{
    private GameObject wallPrefab;
    [SerializeField]
    private float wallSpeed = 3.5f; 
    [SerializeField]
    private float maxDistance = 10f;

    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

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
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f;
        Quaternion spawnRotation = Quaternion.LookRotation(cameraTransform.forward);
        Vector3 fixedDirection = cameraTransform.forward;

        GameObject wallInstance = Instantiate(wallPrefab, spawnPosition, spawnRotation);

        float traveledDistance = 0f;

        while (traveledDistance < maxDistance)
        {
            float moveStep = wallSpeed * Time.deltaTime;
            wallInstance.transform.position += fixedDirection * moveStep;
            traveledDistance += moveStep;
            yield return null;
        }

        Destroy(wallInstance);
    }
}
