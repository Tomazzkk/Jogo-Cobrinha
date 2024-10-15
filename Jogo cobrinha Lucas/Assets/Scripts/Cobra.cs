using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    public Transform prefabSegmento;
    public Vector2Int direcao;
    public float velocidade;
    public float multiplicadorVelocidade = 1f;
    public int tamanhoInicial = 4;
    public bool atravessarParedes = false;

    private List<Transform> segmentos = new List<Transform>();
    //private readonly List<Transform> segmentos = new List<Transform>();
    private Vector2Int entrada;
    private float proximaAtualizacao;

    private void Start()
    {
        ResetarEstado();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direcao = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direcao = Vector2Int.down;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcao = Vector2Int.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcao = Vector2Int.left;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time < proximaAtualizacao)
        {
            return;
        }
        if (entrada != Vector2Int.zero)
        {
            direcao = entrada;
        }
        // Defina a posição de cada segmento para ser a mesma do segmento que ele segue.
        // Isso deve ser feito em ordem inversa para que a posição seja definida como a anterior,
        // caso contrário, todos ficarão empilhados.
        for (int i = segmentos.Count - 1; i > 0; i--)
        {
            segmentos[i].position = segmentos[i - 1].position;
        }
        // Mova a cobra na direção que ela está indo
        // Arredonde os valores para garantir que esteja alinhado à grade
        int x = Mathf.RoundToInt(transform.position.x) + direcao.x;
        int y = Mathf.RoundToInt(transform.position.y) + direcao.y;
        transform.position = new Vector2(x, y);
        // Defina o tempo da próxima atualização com base na velocidade
        proximaAtualizacao = Time.time + (1f / (velocidade * multiplicadorVelocidade));
    }

    public void Crescer()
    {
        Transform segmento = Instantiate(prefabSegmento);
        segmento.position = segmentos[segmentos.Count - 1].position;
        segmentos.Add(segmento);
    }

    public void ResetarEstado()
    {
        direcao = Vector2Int.right;
        transform.position = Vector3.zero;

        // Comece no índice 1 para evitar destruir a cabeça
        for (int i = 1; i < segmentos.Count; i++)
        {
            Destroy(segmentos[i].gameObject);
        }

        // Limpa a lista mas adiciona novamente a cabeça
        segmentos.Clear();
        segmentos.Add(transform);

        // -1 porque a cabeça já está na lista
        for (int i = 0; i < tamanhoInicial - 1; i++)
        {
            Crescer();
        }
    }

    public bool Ocupa(int x, int y)
    {
        foreach (Transform segmento in segmentos)
        {
            if (Mathf.RoundToInt(segmento.position.x) == x &&
                Mathf.RoundToInt(segmento.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Maca"))
        {
            Crescer();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Cobra"))
        {
            Debug.Log("morreu");
        }


    }
    public void DefinirVelocidade(string value)
    {
        GameObject.Find("Cobra").GetComponent<Cobra>().velocidade = float.Parse(value); // manda o valor inserido para a variável velocidade
    }
}
