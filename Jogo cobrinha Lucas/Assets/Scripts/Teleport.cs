using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerainfinita : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //inicializa a camera principal
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtenha as coordenadas da posição do player em relação à câmera (em coordenadas de viewport)
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        //verifica se o player saiu da tela
        if (viewportPosition.x > 1) // saiu pela direita
        {
            viewportPosition.x = 0; //teleporta ele para o lado esquerdo
        }
        else if (viewportPosition.x < 0) // saiu pela esquerda
        {
            viewportPosition.x = 1; //teleporta para o lado direito
        }
        if (viewportPosition.y > 1) // saiu por cima
        {
            viewportPosition.y = 0; //teleporta para baixo

        }
        else if (viewportPosition.y < 0) // se ele saiu por baixo
        {
            viewportPosition.y = 1; //teleporta para cima
        }
        //atualiza a posição do player com as novas coordenadas de viewpoint
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}