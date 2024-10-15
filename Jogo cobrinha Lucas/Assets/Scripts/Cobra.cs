using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declara a classe Cobra, que controla o comportamento da cobra no jogo
public class Cobra : MonoBehaviour
{
    // Vari�veis p�blicas que podem ser ajustadas no Inspector do Unity
    public Transform prefabSegmento; // Prefab para os segmentos do corpo da cobra
    public Vector2Int direcao; // Dire��o que a cobra est� se movendo
    public float velocidade; // Velocidade da cobra
    public float multiplicadorVelocidade = 1f; // Multiplicador da velocidade
    public int tamanhoInicial = 4; // Tamanho inicial da cobra
    public bool atravessarParedes = false; // Se a cobra pode atravessar paredes

    // Lista de segmentos do corpo da cobra
    public List<Transform> segmentos = new List<Transform>();

    // Vari�veis privadas para controlar o movimento
    private Vector2Int entrada; // Entrada do jogador (dire��o)
    private float proximaAtualizacao; // Tempo at� a pr�xima atualiza��o de movimento

    // Lista para armazenar os segmentos da cobra
    public List<Transform> corpoDacobrinha = new List<Transform>();
    private Vector2Int direcaoQueSempreVai; // Armazena a dire��o do movimento

    // Inst�ncia est�tica da classe Cobra, para f�cil acesso de outros scripts
    static public Cobra instance;
    private void Awake()
    {
        instance = this; // Define a inst�ncia da cobra
    }

    // Inicia o estado da cobra no come�o do jogo
    private void Start()
    {
        ResetarEstado(); // Reseta a posi��o e tamanho da cobra
    }

    // Detecta as entradas do jogador (setas direcionais)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direcao = Vector2Int.up; // Define a dire��o para cima
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direcao = Vector2Int.down; // Define a dire��o para baixo
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcao = Vector2Int.right; // Define a dire��o para direita
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcao = Vector2Int.left; // Define a dire��o para esquerda
        }
    }

    // Lida com o movimento da cobra em intervalos fixos
    private void FixedUpdate()
    {
        if (Time.time < proximaAtualizacao)
        {
            return; // Espera at� o pr�ximo movimento
        }
        if (entrada != Vector2Int.zero)
        {
            direcao = entrada; // Atualiza a dire��o com a entrada
        }

        // Move cada segmento da cobra para a posi��o do anterior (em ordem inversa)
        for (int i = segmentos.Count - 1; i > 0; i--)
        {
            segmentos[i].position = segmentos[i - 1].position;
        }

        // Move a cabe�a da cobra para a nova posi��o, com base na dire��o
        int x = Mathf.RoundToInt(transform.position.x) + direcao.x;
        int y = Mathf.RoundToInt(transform.position.y) + direcao.y;
        transform.position = new Vector2(x, y);

        // Define o tempo da pr�xima atualiza��o, com base na velocidade
        proximaAtualizacao = Time.time + (1f / (velocidade * multiplicadorVelocidade));
    }

    // Fun��o que adiciona um novo segmento ao corpo da cobra
    public void Crescer()
    {
        Transform segmento = Instantiate(prefabSegmento); // Instancia um novo segmento
        segmento.position = segmentos[segmentos.Count - 1].position; // Coloca o novo segmento atr�s do �ltimo
        segmentos.Add(segmento); // Adiciona o novo segmento � lista
    }

    // Reseta o estado da cobra (usado no in�cio ou ao reiniciar)
    public void ResetarEstado()
    {
        direcao = Vector2Int.right; // Come�a movendo para a direita
        transform.position = Vector3.zero; // Define a posi��o inicial da cobra

        // Destr�i todos os segmentos da cobra, exceto a cabe�a
        for (int i = 1; i < segmentos.Count; i++)
        {
            Destroy(segmentos[i].gameObject);
        }

        // Limpa a lista de segmentos e reinicializa com a cabe�a
        segmentos.Clear();
        segmentos.Add(transform);

        // Cresce a cobra para o tamanho inicial
        for (int i = 0; i < tamanhoInicial - 1; i++)
        {
            Crescer();
        }
    }

    // Verifica se a cobra ocupa uma posi��o espec�fica
    public bool Ocupa(int x, int y)
    {
        // Percorre todos os segmentos da cobra
        foreach (Transform segmento in segmentos)
        {
            if (Mathf.RoundToInt(segmento.position.x) == x &&
                Mathf.RoundToInt(segmento.position.y) == y)
            {
                return true; // Retorna verdadeiro se a cobra est� nesta posi��o
            }
        }

        return false; // Retorna falso se nenhum segmento ocupa a posi��o
    }

    // Detecta colis�es com outros objetos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com uma ma��, a cobra cresce
        if (collision.gameObject.CompareTag("Maca"))
        {
            Crescer(); // Aumenta a cobra
            Destroy(collision.gameObject); // Remove a ma��
            GameManager.instance.scoreText += 1; // Atualiza a pontua��o
            GameManager.instance.textMeshProUGUI.text = GameManager.instance.scoreText.ToString(); // Exibe a pontua��o
        }

        // Se colidir com outro segmento da cobra, o jogo acaba
        if (collision.gameObject.CompareTag("Cobra"))
        {
            Debug.Log("morreu"); // Exibe uma mensagem no console
            GameManager.instance.GameOver(); // Chama a fun��o de fim de jogo
        }
    }

    // Define a velocidade da cobra a partir de uma entrada do usu�rio
    public void DefinirVelocidade(string value)
    {
        // Encontra a cobra no jogo e ajusta sua velocidade com base na entrada do usu�rio
        GameObject.Find("Cobra").GetComponent<Cobra>().velocidade = float.Parse(value);
    }
}
