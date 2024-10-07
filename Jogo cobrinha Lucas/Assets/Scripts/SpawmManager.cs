using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawmManager : MonoBehaviour
{
    [SerializeField]float clock, cooldown = 2;
     
    [SerializeField] public GameObject maca;
    float alturaX;
    float alturaY;
    static public SpawmManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        SpawmMaca();
    }
    void SpawmMaca()
    {
        alturaX = Random.Range(-8.35f, 8.32f);
        alturaY = Random.Range(-4.47f, 4.48f);
        cooldown = Random.Range(1, 50);

        if(cooldown == 3 )
        {
           
            Instantiate(maca, new Vector2(alturaX, alturaY), Quaternion.identity);
        }

       if (GameManager.instance.Speed< 10)
            {
              GameManager.instance.Speed += 0.5f;
            }
            if (cooldown > 1)
            {
             cooldown -= 0.1f;
            clock = cooldown;
            }
         
            else
            {
                clock -= Time.deltaTime;
            }
       
          }
       
    }
    

