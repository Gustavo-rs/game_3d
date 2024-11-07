using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int hitsTaken = 0;
    private int maxHits = 4;

    public void TakeHit()
    {
        hitsTaken++;
        Debug.Log("Inimigo foi atingido! Total de acertos: " + hitsTaken);

        if (hitsTaken >= maxHits)
        {
            Destroy(gameObject);
            Debug.Log("Inimigo destruído!");
        }
    }
}
