using System.Collections;
using UnityEngine;

public class VerticalDashSkill : Skill
{
    [SerializeField]
    private float dashUpwardSpeed = 5f;
    [SerializeField]
    private float dashUpwardDuration = 0.2f;

    private bool isDashingUpward = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Activate()
    {
        if (!isDashingUpward)
        {
            StartCoroutine(PerformVerticalDash());
        }
    }

    private IEnumerator PerformVerticalDash()
    {
        isDashingUpward = true;

        rb.useGravity = false;

        float elapsedTime = 0f;

        while (elapsedTime < dashUpwardDuration)
        {
            rb.velocity = new Vector3(rb.velocity.x, dashUpwardSpeed, rb.velocity.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.useGravity = true;
        isDashingUpward = false;
    }
}
