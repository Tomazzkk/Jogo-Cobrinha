using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    [SerializeField] GameObject cobra;
    [SerializeField] float speed;
    
    private void Start()
    {
        
        //Instantiate(cobra);
    }
    private void Update()
    {
        MovCobra();
      
    }
    private void MovCobra()
    {
        Vector2 vector2 = cobra.transform.position;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            vector2.x += -1 * speed;
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            vector2.x += 1 * speed;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vector2.y += 1 * speed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vector2.y += -1 * speed;
        }
        cobra.transform.position = vector2; 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(collision.gameObject);
        }
    }
}
