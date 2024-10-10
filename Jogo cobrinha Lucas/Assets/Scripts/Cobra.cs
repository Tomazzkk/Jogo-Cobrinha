using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    public Transform segmentoPrefab; // Prefab do segmento do corpo da cobra
    public Vector2Int direcao; // Dire��o atual da cobra
    public float velocidade; // Velocidade da cobra
   // public float multiplicadorVelocidade = 1f; // Multiplicador de velocidade
    public int tamanhoInicial = 1; // Tamanho inicial da cobra
    public bool atravessarParedes = false; // Controla se a cobra pode atravessar as paredes

    private List<Transform> segmentos = new List<Transform>(); // Lista de segmentos do corpo
    private Vector2Int entrada; // Armazena a dire��o de entrada
    private float proximaAtualizacao; // Controla quando ocorrer� o pr�ximo movimento

    private void Start()
    {
        ReiniciarEstado(); // Reinicia o estado da cobra ao iniciar
    }

    private void Update()
    {
        // Controla a dire��o da cobra com base nas teclas pressionadas
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
            return; // Aguarda at� a pr�xima atualiza��o
        }
        if (entrada != Vector2Int.zero)
        {
            direcao = entrada; // Atualiza a dire��o da cobra
        }

        // Move cada segmento para a posi��o do segmento anterior
        for (int i = segmentos.Count - 1; i > 0; i--)
        {
            segmentos[i].position = segmentos[i - 1].position;
        }

        // Move a cabe�a da cobra na dire��o atual
        // Arredonda os valores para garantir o alinhamento � grade
        int x = Mathf.RoundToInt(transform.position.x) + direcao.x;
        int y = Mathf.RoundToInt(transform.position.y) + direcao.y;
        transform.position = new Vector2(x, y);

        proximaAtualizacao = Time.time + (1f / (velocidade));
    }

    public void Crescer()
    {
        // Instancia um novo segmento de corpo
        Transform segmento = Instantiate(segmentoPrefab);
        segmento.position = segmentos[segmentos.Count - 1].position;
        segmentos.Add(segmento); // Adiciona o novo segmento � lista
    }

    public void ReiniciarEstado()
    {
        // Define a dire��o inicial da cobra para a direita
        direcao = Vector2Int.right;
        transform.position = Vector3.zero;

        // Come�a no �ndice 1 para n�o destruir a cabe�a
        for (int i = 1; i < segmentos.Count; i++)
        {
            Destroy(segmentos[i].gameObject); // Destroi os segmentos antigos
        }

        // Limpa a lista e adiciona a cabe�a de volta
        segmentos.Clear();
        segmentos.Add(transform);

        // Adiciona os segmentos do corpo iniciais
        for (int i = 0; i < tamanhoInicial - 1; i++)
        {
            Crescer();
        }
    }

    public bool Ocupa(int x, int y)
    {
        // Verifica se algum segmento da cobra ocupa a posi��o (x, y)
        foreach (Transform segmento in segmentos)
        {
            if (Mathf.RoundToInt(segmento.position.x) == x & Mathf.RoundToInt(segmento.position.y) == y)
            {
                return true;
            }
        }

        return false; // Retorna falso se nenhum segmento estiver na posi��o
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Maca"))
        {
            Crescer();// A cobra cresce quando colide com uma ma��
            Destroy(collision.gameObject);
        }
     
    }


    public void DefinirVelocidade(string value) //M�todo com par�metro que define a velocidade atrav�s do menu 
    {
        GameObject.Find("Cobra").GetComponent<Cobra>().velocidade = float.Parse(value); // manda o valor inserido para a vari�vel speed
       
    }
}
