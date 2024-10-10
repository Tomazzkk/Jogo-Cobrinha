using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int diametroDoCampo;
    public int[,] grade;
    public static Obstacle instance;
    int indexI, indexJ;

    private void Awake()
    {
        instance = this;
    }

    public void DefinirIndex(int i, int j)
    {
        indexI = i;
        indexJ = j;
    }
}
