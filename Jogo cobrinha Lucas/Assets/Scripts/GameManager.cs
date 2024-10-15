using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// Classe GameManager que controla o jogo
public class GameManager : MonoBehaviour
{
    // Variáveis de controle do jogo
    private Vector2 screenBounds; // Limites da tela
    float speed = 1; // Velocidade do jogo
    public int scoreText; // Pontuação do jogador
    [SerializeField] GameObject menu, gameOver; // Referências para os menus de início e de game over
    public int diametroDoCampo; // Diâmetro do campo de jogo (definido pelo usuário)
    public int[,] grade; // Matriz representando o campo de jogo
    [SerializeField] public GameObject maca; // Referência para o prefab da maçã
    [SerializeField] int numeroDeMaca; // Número de maçãs (não usado diretamente para quantidade infinita)
    [SerializeField] public TextMeshProUGUI textMeshProUGUI; // Referência para o UI da pontuação

    // Propriedades para acessar a velocidade e os limites da tela
    public float Speed { get => speed; set => speed = value; }
    public Vector2 ScreenBounds { get => screenBounds; }

    #region Singleton
    // Padrão Singleton para fácil acesso à instância do GameManager
    static public GameManager instance;
    private void Awake()
    {
        instance = this; // Define a instância do GameManager
        // Calcula os limites da tela com base no tamanho da câmera
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height));
    }
    #endregion

    // Função chamada quando o jogo começa
    private void Start()
    {
        GerarGrade(); // Gera o campo de jogo
        gameOver.SetActive(false); // Desativa o menu de game over
    }

    // Função que inicia o jogo, ajustando a câmera com base no tamanho do campo
    public void IniciarJogo()
    {
        // Ajusta a posição e o tamanho da câmera de acordo com o diâmetro do campo
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2.0f - 2.0f, diametroDoCampo / 2.0f - 11.0f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;
        menu.SetActive(false); // Esconde o menu inicial
    }

    // Função que gera a grade (campo de jogo) e instancia maçãs em posições aleatórias
    void GerarGrade()
    {
        grade = new int[diametroDoCampo, diametroDoCampo]; // Inicializa a matriz com o tamanho do campo
        for (int i = 0; i < diametroDoCampo; i++)
        {
            for (int j = 0; j < diametroDoCampo; j++)
            {
                // Gera posições aleatórias dentro do diâmetro definido
                i = Random.Range(diametroDoCampo, diametroDoCampo);
                j = Random.Range(diametroDoCampo, diametroDoCampo);
                Instantiate(maca, new Vector2(i, j), Quaternion.identity); // Instancia uma maçã na posição gerada
            }
        }
    }

    // Função que define o diâmetro do campo com base na entrada do usuário
    public void DefinirDIametro(string value)
    {
        diametroDoCampo = int.Parse(value); // Converte a string inserida para um número inteiro
    }

    // Função que exibe a tela de game over
    public void GameOver()
    {
        gameOver.SetActive(true); // Ativa o menu de game over
    }

    // Função chamada quando o botão de menu é pressionado, reinicia o jogo
    public void MenuButton()
    {
        SceneManager.LoadScene("SampleScene"); // Recarrega a cena principal
    }
}
