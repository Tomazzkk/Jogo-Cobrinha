using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declara a classe Cobra, que controla o comportamento da cobra no jogo
public class Cobra : MonoBehaviour
{
    // Variáveis públicas que podem ser ajustadas no Inspector do Unity
    public Transform prefabSegmento; // Prefab para os segmentos do corpo da cobra
    public Vector2Int direcao; // Direção que a cobra está se movendo
    public float velocidade; // Velocidade da cobra
    public float multiplicadorVelocidade = 1f; // Multiplicador da velocidade
    public int tamanhoInicial = 4; // Tamanho inicial da cobra
    public bool atravessarParedes = false; // Se a cobra pode atravessar paredes

    // Lista de segmentos do corpo da cobra
    public List<Transform> segmentos = new List<Transform>();

    // Variáveis privadas para controlar o movimento
    private Vector2Int entrada; // Entrada do jogador (direção)
    private float proximaAtualizacao; // Tempo até a próxima atualização de movimento

    // Lista para armazenar os segmentos da cobra
    public List<Transform> corpoDacobrinha = new List<Transform>();
    private Vector2Int direcaoQueSempreVai; // Armazena a direção do movimento

    // Instância estática da classe Cobra, para fácil acesso de outros scripts
    static public Cobra instance;
    private void Awake()
    {
        instance = this; // Define a instância da cobra
    }

    // Inicia o estado da cobra no começo do jogo
    private void Start()
    {
        ResetarEstado(); // Reseta a posição e tamanho da cobra
    }

    // Detecta as entradas do jogador (setas direcionais)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direcao = Vector2Int.up; // Define a direção para cima
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direcao = Vector2Int.down; // Define a direção para baixo
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcao = Vector2Int.right; // Define a direção para direita
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcao = Vector2Int.left; // Define a direção para esquerda
        }
    }

    // Lida com o movimento da cobra em intervalos fixos
    private void FixedUpdate()
    {
        if (Time.time < proximaAtualizacao)
        {
            return; // Espera até o próximo movimento
        }
        if (entrada != Vector2Int.zero)
        {
            direcao = entrada; // Atualiza a direção com a entrada
        }

        // Move cada segmento da cobra para a posição do anterior (em ordem inversa)
        for (int i = segmentos.Count - 1; i > 0; i--)
        {
            segmentos[i].position = segmentos[i - 1].position;
        }

        // Move a cabeça da cobra para a nova posição, com base na direção
        int x = Mathf.RoundToInt(transform.position.x) + direcao.x;
        int y = Mathf.RoundToInt(transform.position.y) + direcao.y;
        transform.position = new Vector2(x, y);

        // Define o tempo da próxima atualização, com base na velocidade
        proximaAtualizacao = Time.time + (1f / (velocidade * multiplicadorVelocidade));
    }

    // Função que adiciona um novo segmento ao corpo da cobra
    public void Crescer()
    {
        Transform segmento = Instantiate(prefabSegmento); // Instancia um novo segmento
        segmento.position = segmentos[segmentos.Count - 1].position; // Coloca o novo segmento atrás do último
        segmentos.Add(segmento); // Adiciona o novo segmento à lista
    }

    // Reseta o estado da cobra (usado no início ou ao reiniciar)
    public void ResetarEstado()
    {
        direcao = Vector2Int.right; // Começa movendo para a direita
        transform.position = Vector3.zero; // Define a posição inicial da cobra

        // Destrói todos os segmentos da cobra, exceto a cabeça
        for (int i = 1; i < segmentos.Count; i++)
        {
            Destroy(segmentos[i].gameObject);
        }

        // Limpa a lista de segmentos e reinicializa com a cabeça
        segmentos.Clear();
        segmentos.Add(transform);

        // Cresce a cobra para o tamanho inicial
        for (int i = 0; i < tamanhoInicial - 1; i++)
        {
            Crescer();
        }
    }

    // Verifica se a cobra ocupa uma posição específica
    public bool Ocupa(int x, int y)
    {
        // Percorre todos os segmentos da cobra
        foreach (Transform segmento in segmentos)
        {
            if (Mathf.RoundToInt(segmento.position.x) == x &&
                Mathf.RoundToInt(segmento.position.y) == y)
            {
                return true; // Retorna verdadeiro se a cobra está nesta posição
            }
        }

        return false; // Retorna falso se nenhum segmento ocupa a posição
    }

    // Detecta colisões com outros objetos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com uma maçã, a cobra cresce
        if (collision.gameObject.CompareTag("Maca"))
        {
            Crescer(); // Aumenta a cobra
            Destroy(collision.gameObject); // Remove a maçã
            GameManager.instance.scoreText += 1; // Atualiza a pontuação
            GameManager.instance.textMeshProUGUI.text = GameManager.instance.scoreText.ToString(); // Exibe a pontuação
        }

        // Se colidir com outro segmento da cobra, o jogo acaba
        if (collision.gameObject.CompareTag("Cobra"))
        {
            Debug.Log("morreu"); // Exibe uma mensagem no console
            GameManager.instance.GameOver(); // Chama a função de fim de jogo
        }
    }

    // Define a velocidade da cobra a partir de uma entrada do usuário
    public void DefinirVelocidade(string value)
    {
        // Encontra a cobra no jogo e ajusta sua velocidade com base na entrada do usuário
        GameObject.Find("Cobra").GetComponent<Cobra>().velocidade = float.Parse(value);
    }
}
