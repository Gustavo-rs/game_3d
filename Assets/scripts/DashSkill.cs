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
        Vector3 targetPosition = startPosition + dashDirection.normalized * dashSpeed;

        // Realiza o Raycast para verificar obstruções
        RaycastHit hit;
        if (Physics.Raycast(startPosition, dashDirection, out hit, dashSpeed))
        {
            // Ajusta a posição alvo para o ponto de colisão se houver uma obstrução
            targetPosition = hit.point;
        }

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            // Move suavemente o jogador até a posição alvo, respeitando obstruções
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que o jogador termina exatamente na posição alvo
        transform.position = targetPosition;
        isDashing = false;
    }

    public override void Activate() { }
}
