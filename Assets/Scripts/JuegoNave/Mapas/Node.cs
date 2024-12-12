using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int PosicionEnGrid;
    public List<Node> NodosConectados = new List<Node>();
    public GameObject lineaP;
    public NodeType Type;

    private bool highlighted = false;
    private bool esAnterior = false;
    private Color colorOriginal;
    private Color colorHighlight = Color.yellow;
    private Color colorAnterior = Color.cyan;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
    }

    public void Initialize(Vector2Int gridPosition)
    {
        PosicionEnGrid = gridPosition;
    }

    public void ConnectTo(Node otherNode)
    {
        if (!NodosConectados.Contains(otherNode))
        {
            NodosConectados.Add(otherNode);
            otherNode.NodosConectados.Add(this);

            GameObject lineObject = Instantiate(lineaP, transform, transform);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, otherNode.transform.position);
        }
    }

    public void Highlight()
    {
        highlighted = true;
        spriteRenderer.color = colorHighlight;
        StartCoroutine(EfectoPulos());
    }

    public void Unhighlight()
    {
        highlighted = false;
        StopCoroutine(EfectoPulos());
        if (!esAnterior)
        {
            spriteRenderer.color = colorOriginal;
        }
    }

    public void MarcarComoAnterior()
    {
        esAnterior = true;
        spriteRenderer.color = colorAnterior;
    }

    private IEnumerator EfectoPulos()
    {
        float pulseSpeed = 0.5f;
        while (highlighted)
        {
            float scale = 1 + Mathf.PingPong(Time.time * pulseSpeed, 0.1f);
            transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    private void OnMouseDown()
    {
        if (highlighted)
        {
            GeneradorMapa mapGenerator = FindObjectOfType<GeneradorMapa>();
            mapGenerator.SelectNodo(this);
        }
    }

    public enum NodeType
    {
        Enemigo,
        Tesoro,
        Tienda,
        Boss,
        Desafio
    }

    public void SetType(NodeType type)
    {
        Type = type;

        GeneradorMapa mapGenerator = FindObjectOfType<GeneradorMapa>();
        if (mapGenerator != null && mapGenerator.tipoSprite.ContainsKey(type))
        {
            spriteRenderer.sprite = mapGenerator.tipoSprite[type];
        }
    }

    public NodeType GetNodeType()
    {
        return Type;
    }
}