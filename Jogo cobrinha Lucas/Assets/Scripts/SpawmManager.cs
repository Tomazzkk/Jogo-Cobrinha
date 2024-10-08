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

   
}


