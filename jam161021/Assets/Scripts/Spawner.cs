using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject herbivore;
    public GameObject carnivore;
    public GameObject plant;


    public int herbivoreStart;
    public int carnivoreStart;
    public int plantStart;

    public float mapSizeX;
    public float mapSizeY;

    void Start()
    {
        for(int i = 0; i < herbivoreStart; i++){
            float randomX = Random.Range(0, mapSizeX);
            float randomY = Random.Range(0, mapSizeY);

            Instantiate(herbivore, new Vector3(randomX, randomY, 1), Quaternion.identity);
        }

         for(int i = 0; i < carnivoreStart; i++){
            float randomX = Random.Range(0, mapSizeX);
            float randomY = Random.Range(0, mapSizeY);

            Instantiate(carnivore, new Vector3(randomX, randomY, 1), Quaternion.identity);
        }

         for(int i = 0; i < plantStart; i++){
            float randomX = Random.Range(0, mapSizeX);
            float randomY = Random.Range(0, mapSizeY);

            Instantiate(plant, new Vector3(randomX, randomY, 1), Quaternion.identity);
        }
    }

    
}
