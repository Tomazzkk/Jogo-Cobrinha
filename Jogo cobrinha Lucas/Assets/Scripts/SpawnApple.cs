using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] public GameObject cabecaCobra; // Cabe�a da cobra
    [SerializeField] public GameObject maca; // Objeto ma��
    public bool comeuMaca; // Vari�vel para verificar se a cobra comeu a ma��
    private GameObject novaMaca; // Objeto para armazenar a nova ma��

    private Cobra crescerCobra; // Refer�ncia ao script que faz a cobra crescer

    void Awake()
    {
        // Instancia uma nova ma�� na posi��o (1, 0, 0)
        novaMaca = Instantiate(maca, new Vector3(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo,0), Quaternion.identity);
    }

    void Update()
    {
        // Verifica se a posi��o da cabe�a da cobra � igual � posi��o da ma��
        comeuMaca = (cabecaCobra.transform.position == novaMaca.transform.position);

        // Obt�m o componente de crescimento da cobra
        crescerCobra = cabecaCobra.GetComponent<Cobra>();
        bool macaGerada = false; // Vari�vel para verificar se a ma�� foi gerada

        if (comeuMaca)
        {
            // Enquanto a nova ma�� n�o for gerada em uma posi��o v�lida
            while (!macaGerada)
            {
                // Gera uma nova posi��o aleat�ria para a ma��
                Vector3 novaPosicaoMaca = new Vector3(Random.Range(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo), Random.Range(GameManager.instance.diametroDoCampo, GameManager.instance.diametroDoCampo), 0);

                // Se a nova posi��o estiver em uma parte da cobra, continua buscando
                if (gerarNovaMaca(novaPosicaoMaca))
                {
                    continue;
                }
                else
                {
                    // Destr�i a ma�� antiga e cria uma nova na posi��o gerada
                    Destroy(novaMaca);
                    novaMaca = Instantiate(maca, novaPosicaoMaca, Quaternion.identity);
                    macaGerada = true;
                }
            }
        }
    }

    // Fun��o que verifica se a nova ma�� est� na posi��o de uma parte do corpo da cobra
    bool gerarNovaMaca(Vector3 novaPosicaoMaca)
    {
        for (int i = 0; i < Cobra.instance.corpoDacobrinha.Count; i++)
        {
            if (novaPosicaoMaca == Cobra.instance.corpoDacobrinha[i].transform.position)
            {
                return true; // Se estiver na mesma posi��o que uma parte da cobra, n�o gera
            }
        }
        return false; // Se n�o houver conflito de posi��o, pode gerar a ma��
    }

}