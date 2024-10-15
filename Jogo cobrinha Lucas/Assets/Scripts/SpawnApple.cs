using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] public GameObject cabecaCobra; // Cabeça da cobra
    [SerializeField] public GameObject maca; // Objeto maçã
    public bool comeuMaca; // Variável para verificar se a cobra comeu a maçã
    private GameObject novaMaca; // Objeto para armazenar a nova maçã

    private Cobra crescerCobra; // Referência ao script que faz a cobra crescer

    void Awake()
    {
        // Instancia uma nova maçã na posição (1, 0, 0)
        novaMaca = Instantiate(maca, new Vector3(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo,0), Quaternion.identity);
    }

    void Update()
    {
        // Verifica se a posição da cabeça da cobra é igual à posição da maçã
        comeuMaca = (cabecaCobra.transform.position == novaMaca.transform.position);

        // Obtém o componente de crescimento da cobra
        crescerCobra = cabecaCobra.GetComponent<Cobra>();
        bool macaGerada = false; // Variável para verificar se a maçã foi gerada

        if (comeuMaca)
        {
            // Enquanto a nova maçã não for gerada em uma posição válida
            while (!macaGerada)
            {
                // Gera uma nova posição aleatória para a maçã
                Vector3 novaPosicaoMaca = new Vector3(Random.Range(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo), Random.Range(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo), 0);

                // Se a nova posição estiver em uma parte da cobra, continua buscando
                if (gerarNovaMaca(novaPosicaoMaca))
                {
                    continue;
                }
                else
                {
                    // Destrói a maçã antiga e cria uma nova na posição gerada
                    Destroy(novaMaca);
                    novaMaca = Instantiate(maca, novaPosicaoMaca, Quaternion.identity);
                    macaGerada = true;
                }
            }
        }
    }

    // Função que verifica se a nova maçã está na posição de uma parte do corpo da cobra
    bool gerarNovaMaca(Vector3 novaPosicaoMaca)
    {
        for (int i = 0; i < Cobra.instance.corpoDacobrinha.Count; i++)
        {
            if (novaPosicaoMaca == Cobra.instance.corpoDacobrinha[i].transform.position)
            {
                return true; // Se estiver na mesma posição que uma parte da cobra, não gera
            }
        }
        return false; // Se não houver conflito de posição, pode gerar a maçã
    }

}