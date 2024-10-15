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
    // Vari�veis de controle do jogo
    private Vector2 screenBounds; // Limites da tela
    float speed = 1; // Velocidade do jogo
    public int scoreText; // Pontua��o do jogador
    [SerializeField] GameObject menu, gameOver; // Refer�ncias para os menus de in�cio e de game over
    public int diametroDoCampo; // Di�metro do campo de jogo (definido pelo usu�rio)
    public int[,] grade; // Matriz representando o campo de jogo
    [SerializeField] public GameObject maca; // Refer�ncia para o prefab da ma��
    [SerializeField] int numeroDeMaca; // N�mero de ma��s (n�o usado diretamente para quantidade infinita)
    [SerializeField] public TextMeshProUGUI textMeshProUGUI; // Refer�ncia para o UI da pontua��o

    // Propriedades para acessar a velocidade e os limites da tela
    public float Speed { get => speed; set => speed = value; }
    public Vector2 ScreenBounds { get => screenBounds; }

    #region Singleton
    // Padr�o Singleton para f�cil acesso � inst�ncia do GameManager
    static public GameManager instance;
    private void Awake()
    {
        instance = this; // Define a inst�ncia do GameManager
        // Calcula os limites da tela com base no tamanho da c�mera
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height));
    }
    #endregion

    // Fun��o chamada quando o jogo come�a
    private void Start()
    {
        GerarGrade(); // Gera o campo de jogo
        gameOver.SetActive(false); // Desativa o menu de game over
    }

    // Fun��o que inicia o jogo, ajustando a c�mera com base no tamanho do campo
    public void IniciarJogo()
    {
        // Ajusta a posi��o e o tamanho da c�mera de acordo com o di�metro do campo
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2.0f - 2.0f, diametroDoCampo / 2.0f - 11.0f, -10);
        Camera.main.orthographicSize = diametroDoCampo / 2f;
        menu.SetActive(false); // Esconde o menu inicial
    }

    // Fun��o que gera a grade (campo de jogo) e instancia ma��s em posi��es aleat�rias
    void GerarGrade()
    {
        grade = new int[diametroDoCampo, diametroDoCampo]; // Inicializa a matriz com o tamanho do campo
        for (int i = 0; i < diametroDoCampo; i++)
        {
            for (int j = 0; j < diametroDoCampo; j++)
            {
                // Gera posi��es aleat�rias dentro do di�metro definido
                i = Random.Range(diametroDoCampo, diametroDoCampo);
                j = Random.Range(diametroDoCampo, diametroDoCampo);
                Instantiate(maca, new Vector2(i, j), Quaternion.identity); // Instancia uma ma�� na posi��o gerada
            }
        }
    }

    // Fun��o que define o di�metro do campo com base na entrada do usu�rio
    public void DefinirDIametro(string value)
    {
        diametroDoCampo = int.Parse(value); // Converte a string inserida para um n�mero inteiro
    }

    // Fun��o que exibe a tela de game over
    public void GameOver()
    {
        gameOver.SetActive(true); // Ativa o menu de game over
    }

    // Fun��o chamada quando o bot�o de menu � pressionado, reinicia o jogo
    public void MenuButton()
    {
        SceneManager.LoadScene("SampleScene"); // Recarrega a cena principal
    }
}
