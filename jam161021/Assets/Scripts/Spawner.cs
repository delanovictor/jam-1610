using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Specie[] species;
   
    public float mapSizeX;
    public float mapSizeY;

    public float lastSpawn;

    void Start()
    {

        foreach(Specie s in species){
            s.holder = new GameObject(s.name);

            for(int i = 0; i < s.startNumber; i++){
                float randomX = Random.Range(0, mapSizeX);
                float randomY = Random.Range(0, mapSizeY);

                Instantiate(s.prefab, new Vector3(randomX, randomY, 1), Quaternion.identity, s.holder.transform);
            }

            s.lastSpawn =  Time.timeSinceLevelLoad;
        }
      
        Camera.main.transform.position = new Vector3(mapSizeX/2, mapSizeY/2, 0);
    }

    private void LateUpdate() {
        foreach(Specie s in species){
            s.currentNumber = s.holder.transform.childCount;
            s.bar.setMax(s.maxNumber);
            s.bar.setValue(s.currentNumber);

            if(((Time.timeSinceLevelLoad - s.lastSpawn) ) > s.growthInterval + s.growthStartOffset){
                s.growthStartOffset = 0;
                s.generation ++;
                int newOffspring = (int)(s.currentNumber * s.growthRate);


                if(newOffspring == 0 && s.currentNumber > 0){
                    newOffspring = 1;
                }


                if(s.currentNumber + newOffspring < s.maxNumber){
                    for(int i = 0; i < newOffspring; i++){
                        float randomX = Random.Range(0, mapSizeX);
                        float randomY = Random.Range(0, mapSizeY);

                        Instantiate(s.prefab, new Vector3(randomX, randomY, 1), Quaternion.identity, s.holder.transform);
                    }
                }
           
                s.lastSpawn = Time.timeSinceLevelLoad;
            }
        }
    }

     private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + new Vector3(mapSizeX/2, mapSizeY/2, 1),new Vector3(mapSizeX, mapSizeY, 1)
        );
    }
}
