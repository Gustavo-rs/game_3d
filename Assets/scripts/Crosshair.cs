using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform crosshairRect;
    private Vector2 originalSize;

    void Start()
    {
        crosshairRect = GetComponent<RectTransform>();
        originalSize = crosshairRect.sizeDelta;
    }

    public void OnShoot()
    {
        // Aumenta a mira temporariamente ao atirar
        crosshairRect.sizeDelta = originalSize * 1.5f;
        Invoke("ResetSize", 0.1f); // Volta ao tamanho original após 0.1s
    }

    void ResetSize()
    {
        crosshairRect.sizeDelta = originalSize;
    }
}
