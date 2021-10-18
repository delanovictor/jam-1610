using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Specie
{
    public GameObject prefab;

    public GameObject holder;
    
    public BarScript bar;

    public float growthStartOffset;
    public float growthInterval;
    public float growthRate;

    public int startNumber;
    public int currentNumber;
    public int maxNumber;
 
    public float lastSpawn;

    public string name;

    public int generation = 0;

    public int minGenReprodution = 0;

}
