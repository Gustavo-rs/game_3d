using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    [SerializeField]
    private float dashSpeed = 10f;

    [SerializeField]
    private float dashDuration = 0.1f;

    private bool isDashing = false;

    public void Activate(Vector3 direction)
    {
        if (!isDashing)
        {
            StartCoroutine(PerformDash(direction));
        }
    }

    private IEnumerator PerformDash(Vector3 dashDirection)
    {
        isDashing = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + dashDirection * dashSpeed, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    public override void Activate() { }
}
