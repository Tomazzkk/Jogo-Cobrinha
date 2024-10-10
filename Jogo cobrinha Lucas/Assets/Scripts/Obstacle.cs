using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    public static Obstacle instance;
    

    private void Awake()
    {
        instance = this;
    }

    

   
}
