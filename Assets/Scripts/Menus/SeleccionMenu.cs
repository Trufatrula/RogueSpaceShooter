using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SeleccionMenu : MonoBehaviour
{
    public RectTransform[] images;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public float spacing = 100f;
    public float lerpSpeed = 5f;
    public float posX;
    public float posYCentral;
    public bool isActive = false;
    public bool isDeployed = false;
    public bool isSingleSelection = false;

    private int selectedIndex = 0;
    private Vector2[] targetPositions;
    public bool isConfirmed = false;

    void Start()
    {
        targetPositions = new Vector2[images.Length];
        UpdateTargetPositions();
    }

    void Update()
    {
        if (isActive)
        {
            isDeployed = true;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveSelection(1);
            }
        }

        if (isDeployed)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].anchoredPosition = Vector2.Lerp(images[i].anchoredPosition, targetPositions[i], Time.deltaTime * lerpSpeed);
            }
        } 
        else
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].anchoredPosition = Vector2.Lerp(images[i].anchoredPosition, targetPositions[selectedIndex], Time.deltaTime * lerpSpeed);
            }
        }
    }

    void MoveSelection(int direction)
    {
        selectedIndex = (selectedIndex + direction + images.Length) % images.Length;

        UpdateTargetPositions();
    }

    void UpdateTargetPositions()
    {
        for (int i = 0; i < images.Length; i++)
        {
            int offsetFromSelected = i - selectedIndex;
            float targetY = posYCentral - (offsetFromSelected * spacing);
            targetPositions[i] = new Vector2(posX, targetY);

            images[i].GetComponent<Image>().color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }

    public void ConfirmSelection()
    {
        isConfirmed = true;
        images[selectedIndex].SetAsLastSibling();
        isDeployed = false;
        Debug.Log("Confirmed selection on " + gameObject.name + " - Item: " + selectedIndex);
    }

    public void Deselect()
    {
        isConfirmed = false;
        isDeployed = false;
        images[selectedIndex].GetComponent<Image>().color = normalColor;
    }

    public void ActivateMenu()
    {
        isActive = true;
        UpdateTargetPositions();
    }

    public void DeactivateMenu()
    {
        isActive = false;
    }

    public int getSelectedIndex()
    {
        return selectedIndex;
    }
}