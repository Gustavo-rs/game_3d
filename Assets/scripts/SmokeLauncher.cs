using System.Collections;
using UnityEngine;

public class SmokeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject smokePrefab;
    [SerializeField]
    private float throwForce = 10f;
    [SerializeField]
    private float smokeDuration = 5f;
    [SerializeField]
    private float launchOffset = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowSmoke();
        }
    }

    private void ThrowSmoke()
    {
        Vector3 spawnPosition = transform.position + transform.forward * launchOffset + Vector3.up * 1f;

        GameObject smoke = Instantiate(smokePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = smoke.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        StartCoroutine(ActivateSmoke(smoke));
    }

    private IEnumerator ActivateSmoke(GameObject smoke)
    {
        yield return new WaitForSeconds(1f);

        ParticleSystem ps = smoke.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        yield return new WaitForSeconds(smokeDuration);
        Destroy(smoke);
    }
}
