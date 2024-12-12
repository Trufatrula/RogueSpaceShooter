using UnityEngine;

public class SlotScroller : MonoBehaviour
{
    public RectTransform panelSlots;
    public float velocidadSlots = 100f;
    public bool girando = true;
    private float alturaImagen;
    private int imagenesCuenta;
    private float resetearPos;
    private float posInicial;
    private int selecionada;

    void Start()
    {
        alturaImagen = panelSlots.GetChild(0).GetComponent<RectTransform>().rect.height;
        imagenesCuenta = panelSlots.childCount;

        resetearPos = -alturaImagen * (imagenesCuenta-1);
        posInicial = 0f;
    }

    void Update()
    {
        if (girando)
        {
            panelSlots.anchoredPosition += Vector2.down * velocidadSlots * Time.deltaTime;

            if (panelSlots.anchoredPosition.y <= resetearPos)
            {
                panelSlots.anchoredPosition = new Vector2(panelSlots.anchoredPosition.x, posInicial);
            }
        }
    }


    public void Stop()
    {
        girando = false;
        SnapearEnImagen();
    }

    public void ResetSlot()
    {
        girando = true;
    }

    private void SnapearEnImagen()
    {
        float yPos = panelSlots.anchoredPosition.y;
        selecionada = Mathf.RoundToInt(-yPos / alturaImagen);

        selecionada = Mathf.Clamp(selecionada, 0, imagenesCuenta - 1);

        float targetYPos = -selecionada * alturaImagen;
        panelSlots.anchoredPosition = new Vector2(panelSlots.anchoredPosition.x, targetYPos);
    }

    public int GetIndiceSelecionado()
    {
        return selecionada;
    }
}