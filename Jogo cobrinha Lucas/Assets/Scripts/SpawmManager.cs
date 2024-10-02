using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawmManager : MonoBehaviour
{
    float clock, cooldown = 2;
    [SerializeField] GameObject maca;
    float alturaX;
    float alturaY;

    private void Update()
    {
        SpawmMaca();
    }
    void SpawmMaca()
    {
        alturaX = Random.Range(-8.35f, 8.32f);
        alturaY = Random.Range(-4.47f, 4.48f);

        if(clock > 0)
        {
            Instantiate(maca);
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
    

