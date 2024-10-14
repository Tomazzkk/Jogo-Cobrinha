using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    public Transform segmentoPrefab; // Prefab do segmento do corpo da cobra
    public Vector2Int direcao; // Direção atual da cobra
    public float velocidade = 20f ; // Velocidade da cobra
    public float multiplicadorVelocidade = 1f; // Multiplicador de velocidade
    public int tamanhoInicial = 4; // Tamanho inicial da cobra
    public bool atravessarParedes = false; // Controla se a cobra pode atravessar as paredes

    private List<Transform> corpoDacobrinha = new List<Transform>(); // Lista de segmentos do corpo
    private Vector2Int direcaoQueSempreVai; // Armazena a direção de entrada
    private float proximaAtualizacao; // Controla quando ocorrerá o próximo movimento

    private void Start()
    {
        ReiniciarEstado(); // Reinicia o estado da cobra ao iniciar
    }

    private void Update()
    {
        // Controla a direção da cobra com base nas teclas pressionadas
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
            return; // Aguarda até a próxima atualização
        }
        if (direcaoQueSempreVai != Vector2Int.zero)
        {
            direcao = direcaoQueSempreVai; // Atualiza a direção da cobra
        }

        // Move cada segmento para a posição do segmento anterior
        for (int i = corpoDacobrinha.Count - 1; i > 0; i--)
        {
            corpoDacobrinha[i].position = corpoDacobrinha[i - 1].position;
        }

        // Move a cabeça da cobra na direção atual
        // Arredonda os valores para garantir o alinhamento à grade
        int x = Mathf.RoundToInt(transform.position.x) + direcao.x;
        int y = Mathf.RoundToInt(transform.position.y) + direcao.y;
        transform.position = new Vector2(x, y);

        // Define o próximo tempo de atualização com base na velocidade
        proximaAtualizacao = Time.time + (1f / (velocidade * multiplicadorVelocidade));
    }

    public void Crescer()
    {
        // Instancia um novo segmento de corpo
        Transform segmento = Instantiate(segmentoPrefab);
        segmento.position = corpoDacobrinha[corpoDacobrinha.Count - 1].position;
        corpoDacobrinha.Add(segmento); // Adiciona o novo segmento à lista
    }

    public void ReiniciarEstado()
    {
        // Define a direção inicial da cobra para a direita
        direcao = Vector2Int.right;
        transform.position = Vector3.zero;

        // Começa no índice 1 para não destruir a cabeça
        for (int i = 1; i < corpoDacobrinha.Count; i++)
        {
            Destroy(corpoDacobrinha[i].gameObject); // Destroi os segmentos antigos
        }

        // Limpa a lista e adiciona a cabeça de volta
        corpoDacobrinha.Clear();
        corpoDacobrinha.Add(transform);

        // Adiciona os segmentos do corpo iniciais
        for (int i = 0; i < tamanhoInicial - 1; i++)
        {
            Crescer();
        }
    }

    public bool Ocupa(int x, int y)
    {
        // Verifica se algum segmento da cobra ocupa a posição (x, y)
        foreach (Transform segmento in corpoDacobrinha)
        {
            if (Mathf.RoundToInt(segmento.position.x) == x &&
                Mathf.RoundToInt(segmento.position.y) == y)
            {
                return true;
            }
        }

        return false; // Retorna falso se nenhum segmento estiver na posição
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Maca"))
        {
            Crescer(); // A cobra cresce quando colide com uma maçã
            Destroy(collision.gameObject);
        }
        
    }

    public void DefinirVelocidade(string value)
    {
        GameObject.Find("Cobra").GetComponent<Cobra>().velocidade = float.Parse(value); // manda o valor inserido para a variável speed
    }

}
