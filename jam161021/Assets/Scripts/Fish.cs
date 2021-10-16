using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public enum FishType {
        Herbivore,
        Carnivore,
        Onivore
    }

    public FishType fishType;

    public Vector2 speed;

    public float maxHunger;
    public float currentHunger;
    public float rateHunger;

    public float secondsToRandomPush;
    public float lastPush;

    void Start()
    {
        lastPush = Time.timeSinceLevelLoad;
        currentHunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - lastPush > secondsToRandomPush){
            Debug.Log("Random Push");
            lastPush = Time.timeSinceLevelLoad;
            secondsToRandomPush += Random.Range(5, 15);
        };

        currentHunger -= rateHunger * Time.deltaTime;
    }
}
