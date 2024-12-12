using UnityEngine;

public class BackgroundColorLerp : MonoBehaviour
{
    public float lerpDuration = 2f;
    private Camera cam;
    private Color targetColor;
    private float lerpTime = 0f;

    void Start()
    {
        cam = Camera.main;
        targetColor = cam.backgroundColor;
    }

    void Update()
    {
        if(lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, targetColor, lerpTime / lerpDuration);
        }
    }

    public void SetBackgroundColor(string colorString)
    {
        if (ColorUtility.TryParseHtmlString(colorString, out Color parsedColor))
        {
            targetColor = parsedColor;
            lerpTime = 0f; 
        }
    }
}