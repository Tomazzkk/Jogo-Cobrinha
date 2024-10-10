using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    [SerializeField] GameObject cabecaCobra; // variavel serializada da cabeca da cobra
    [SerializeField] float speed; // variavel que controla a velocidade do jogo
    [SerializeField] List<GameObject> cobracorpolist = new List<GameObject>(); // lista para organizar a cobra e seu corpo
    [SerializeField] GameObject corpoCobra; // variavel GameObject do corpo da cobra 
    [SerializeField] float distanciaEntreSegmentos = 0.5f; // Dist�ncia entre os segmentos do corpo
    private List<Vector3> posicoesAnteriores = new List<Vector3>(); // Armazena as posi��es anteriores da cabe�a


    private void Awake()
    {
        
    }
    private void Start()
    {
        
        //Instantiate(cobra);
    }
    private void Update()
    {
        MovCobra(); // Checa a todo momento oque foi pedido no metodo
        
      
    }

    
    private void MovCobra()
    {

        Vector2 vector2 = cabecaCobra.transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector2.x += -1 * speed * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector2.x += 1 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vector2.y += 1 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vector2.y += -1 * speed * Time.deltaTime;
        }
        
        cabecaCobra.transform.position = vector2;

        posicoesAnteriores.Insert(0, cabecaCobra.transform.position);

        // Mant�m a lista com tamanho adequado
        if (posicoesAnteriores.Count > (cobracorpolist.Count + 1) * Mathf.RoundToInt(distanciaEntreSegmentos / Time.deltaTime))
        {
            posicoesAnteriores.RemoveAt(posicoesAnteriores.Count - 1);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)  // metodo para checar a colisao

    {

        if (collision.gameObject.CompareTag("Maca"))
        {
            GameObject novaParteCorpo = Instantiate(corpoCobra, cobracorpolist[cobracorpolist.Count - 1].transform.position, Quaternion.identity);
            cobracorpolist.Add(novaParteCorpo); // adiciona o corpo da cobra � lista
            Debug.Log("Colidiu"); // fala no console que colidiu
           Destroy(collision.gameObject); // destroi o game object colidido
        }
    }
    private void MoverCorpo()
    {
        for (int i = 0; i < cobracorpolist.Count; i++)
        {
            int indice = (i + 1) * Mathf.RoundToInt(distanciaEntreSegmentos / Time.deltaTime);
            if (i < posicoesAnteriores.Count)
            {
                // Atribui a posi��o anterior da cabe�a para cada segmento do corpo
                cobracorpolist[i].transform.position = posicoesAnteriores[i];
            }
        }
    }


    public void DefinirVelocidade(string value) //M�todo com parametro que define a velocidade atraves do menu 
    {
        GameObject.Find("Cobra").GetComponent<Cobra>().speed = float.Parse(value); // manda o valor inserido para a variavel speed
    }


}
