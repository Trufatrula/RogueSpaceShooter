
using TMPro;
using UnityEngine;

public class CambioDeColor : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float colorChangeSpeed = 2f;
    private Color[] colors = new Color[]
    {
        Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta
    };
    private int currentColorIndex = 0;
    private float lerpTime = 0f;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        lerpTime += Time.deltaTime * colorChangeSpeed;

        textMeshPro.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], lerpTime);

        if (lerpTime >= 1f)
        {
            lerpTime = 0f;
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }
    }
}