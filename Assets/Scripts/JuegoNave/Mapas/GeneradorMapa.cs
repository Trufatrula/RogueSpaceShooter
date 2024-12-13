using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GeneradorMapa : MonoBehaviour
{
    private const int anchoGrid = 7;
    private const int alturaGrid = 15;
    private Node[,] grid = new Node[anchoGrid, alturaGrid];

    public GameObject nodos;
    public GameObject mapa;
    public int alturaActual = 0;

    private GameManagerNave gmNave;

    [SerializeField] private Sprite spriteEnemigo;
    [SerializeField] private Sprite spriteTesoro;
    [SerializeField] private Sprite spriteTienda;
    [SerializeField] private Sprite spriteElite;
    [SerializeField] private Sprite spriteGambling;

    [SerializeField] GameObject oleadasEnemigas;
    [SerializeField] GameObject oleadasEnemigas2;
    [SerializeField] GameObject oleadasEnemigasElite;
    [SerializeField] GameObject oleadasEnemigasBossFinal;
    [SerializeField] UnityEvent eventoEnemigo;
    [SerializeField] GameObject tesoro;
    [SerializeField] GameObject slots;
    [SerializeField] GameObject tienda;

    public Dictionary<Node.NodeType, Sprite> tipoSprite = new Dictionary<Node.NodeType, Sprite>();

    private Dictionary<Node.NodeType, float> probabilidadesTipo = new Dictionary<Node.NodeType, float>
    {
        { Node.NodeType.Enemigo, 0.5f },
        { Node.NodeType.Tesoro, 0.2f },
        { Node.NodeType.Tienda, 0.15f },
        { Node.NodeType.Boss, 0.1f },
        { Node.NodeType.Desafio, 0.05f }
    };

    void Start()
    {
        gmNave = FindAnyObjectByType<GameManagerNave>();
        GenerarGrid();
        GenerarCaminos();
        QuitarNodosDesconectados();
        PonerTipos();
        HighlightDeNodos();
    }

    private void Awake()
    {
        tipoSprite[Node.NodeType.Enemigo] = spriteEnemigo;
        tipoSprite[Node.NodeType.Tesoro] = spriteTesoro;
        tipoSprite[Node.NodeType.Tienda] = spriteTienda;
        tipoSprite[Node.NodeType.Boss] = spriteElite;
        tipoSprite[Node.NodeType.Desafio] = spriteGambling;
    }

    void GenerarGrid()
    {
        for (int x = 0; x < anchoGrid; x++)
        {
            for (int y = 0; y < alturaGrid; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                Vector3 worldPosition = new Vector3(transform.position.x + x*2, transform.position.y + y * 2, 0);
                GameObject nodeObject = Instantiate(nodos, worldPosition, Quaternion.identity, mapa.transform);

                Node node = nodeObject.GetComponent<Node>();
                node.Initialize(gridPosition);

                grid[x, y] = node;
            }
        }
    }

    void GenerarCaminos()
    {
        List<Node> startingNodes = new List<Node>();

        while (startingNodes.Count < 6)
        {
            int startX = Random.Range(0, anchoGrid);
            Node startNode = grid[startX, 0];

            if (!startingNodes.Contains(startNode))
            {
                startingNodes.Add(startNode);
            }
        }

        foreach (Node startNode in startingNodes)
        {
            GenerarCamino(startNode);
        }
    }

    void GenerarCamino(Node startNode)
    {
        Node currentNode = startNode;
        Node lastNode = null;

        for (int y = 0; y < alturaGrid - 1; y++)
        {
            int x = currentNode.PosicionEnGrid.x;
            List<Node> possibleNodes = new List<Node>();
            if (x > 0) possibleNodes.Add(grid[x - 1, y + 1]);
            possibleNodes.Add(grid[x, y + 1]);
            if (x < anchoGrid - 1) possibleNodes.Add(grid[x + 1, y + 1]);

            if (y == 0 && lastNode != null)
            {
                possibleNodes.Remove(lastNode);
            }

            Node nextNode = possibleNodes[Random.Range(0, possibleNodes.Count)];
            currentNode.ConnectTo(nextNode);

            lastNode = currentNode;
            currentNode = nextNode;
        }
    }

    void QuitarNodosDesconectados()
    {
        for (int x = 0; x < anchoGrid; x++)
        {
            for (int y = 0; y < alturaGrid; y++)
            {
                Node node = grid[x, y];
                if (node != null && node.NodosConectados.Count == 0)
                {
                    Destroy(node.gameObject);
                    grid[x, y] = null;
                }
            }
        }
    }

    void PonerTipos()
    {
        for (int x = 0; x < anchoGrid; x++)
        {
            for (int y = 0; y < alturaGrid; y++)
            {
                Node node = grid[x, y];
                if (node != null && node.NodosConectados.Count > 0)
                {
                    if (y == 0)
                    {
                        node.SetType(Node.NodeType.Enemigo);
                    }
                    else if (y == 14)
                    {
                        node.SetType(Node.NodeType.Boss);
                    }
                    else if (y < 6)
                    {
                        Node.NodeType nodeType = GetTipoDeNodo(excludeShopAndBoss: true);
                        node.SetType(nodeType);
                    }
                    else
                    {
                        Node.NodeType nodeType = GetTipoDeNodo();
                        node.SetType(nodeType);
                    }
                }
            }
        }
    }

    Node.NodeType GetTipoDeNodo(bool excludeShopAndBoss = false)
    {
        var availableTypeOdds = excludeShopAndBoss
            ? probabilidadesTipo.Where(entry => entry.Key != Node.NodeType.Tienda && entry.Key != Node.NodeType.Boss).ToList()
            : probabilidadesTipo.ToList();

        float totalProbability = availableTypeOdds.Sum(entry => entry.Value);
        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (var typeEntry in availableTypeOdds)
        {
            cumulativeProbability += typeEntry.Value;
            if (randomValue <= cumulativeProbability)
            {
                return typeEntry.Key;
            }
        }

        return availableTypeOdds.Last().Key;
    }

    void HighlightDeNodos()
    {
        for (int x = 0; x < anchoGrid; x++)
        {
            Node node = grid[x, 0];
            if (node != null)
            {
                node.Highlight();
            }
        }
    }

    public void SelectNodo(Node selectedNode)
    {
        selectedNode.MarcarComoAnterior();

        Node.NodeType typeNode = selectedNode.GetNodeType();
        alturaActual++;
        GameObject oleadas = null;
        switch (typeNode)
        {
            case Node.NodeType.Tienda:
                tienda.SetActive(true);
                eventoEnemigo.Invoke();
                break;
            case Node.NodeType.Boss:
                if(alturaActual == 15)
                {
                    oleadas = Instantiate(oleadasEnemigasBossFinal, new Vector3(-37.5f, 0, 0), Quaternion.identity);
                } else
                {
                    oleadas = Instantiate(oleadasEnemigasElite, new Vector3(-37.5f, 0, 0), Quaternion.identity);
                }
                oleadas.GetComponent<EnemigosNormales>().ComenzarOleadas(alturaActual * gmNave.GetModDificultad());
                eventoEnemigo.Invoke();
                break;
            case Node.NodeType.Enemigo:
                if (alturaActual%3 == 0)
                {
                    oleadas = Instantiate(oleadasEnemigas2, new Vector3(-37.5f, 0, 0), Quaternion.identity);
                } else
                {
                    oleadas = Instantiate(oleadasEnemigas, new Vector3(-37.5f, 0, 0), Quaternion.identity);
                }
                oleadas.GetComponent<EnemigosNormales>().ComenzarOleadas(alturaActual * gmNave.GetModDificultad());
                eventoEnemigo.Invoke();
                break;
            case Node.NodeType.Desafio:
                slots.SetActive(true);
                eventoEnemigo.Invoke();
                break;
            case Node.NodeType.Tesoro:
                tesoro.SetActive(true);
                eventoEnemigo.Invoke();
                break;
            default:
                break;
        }

        DeshighlightearNodos();

        foreach (Node connectedNode in selectedNode.NodosConectados)
        {
            if (connectedNode.transform.position.y > selectedNode.transform.position.y)
            {
                connectedNode.Highlight();
            }
        }
    }

    void DeshighlightearNodos()
    {
        for (int x = 0; x < anchoGrid; x++)
        {
            for (int y = 0; y < alturaGrid; y++)
            {
                Node node = grid[x, y];
                if (node != null)
                {
                    node.Unhighlight();
                }
            }
        }
    }
}