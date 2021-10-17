using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Specie[] species;
   
    public float mapSizeX;
    public float mapSizeY;

    public float lastSpawn;

    public float timeBetweenEvents;
    public float eventDuration;

    public BarScript nextEventBar;
    public float nextEventTime;

    public GameObject currentEventObject;
    private BarScript currentEventBar;
    public float currentEventTime;

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

        nextEventBar.setMax(1);
        nextEventBar.setValue(0);

        currentEventBar = currentEventObject.GetComponentInChildren<BarScript>();
        currentEventObject.SetActive(false);

        StartCoroutine ("RandomEventGenerator", timeBetweenEvents);

    }

    private void Update() {
        nextEventBar.setValue((Time.timeSinceLevelLoad - nextEventTime) / timeBetweenEvents);
        currentEventBar.setValue((Time.timeSinceLevelLoad - currentEventTime) / eventDuration);
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

    IEnumerator RandomEventGenerator(float delay) {
		while (true) {
            nextEventTime = Time.timeSinceLevelLoad;
			yield return new WaitForSeconds (delay);

            Debug.Log("Starting Event");
            StartCoroutine(EventExecuter(1, eventDuration));
		}
	}

    IEnumerator EventExecuter(int eventID, float delay) {
        currentEventTime = Time.timeSinceLevelLoad;
        currentEventObject.SetActive(true);
        switch(eventID){
            
        }
        Debug.Log("Executando evento " +eventID);
        yield return new WaitForSeconds (delay);

        currentEventObject.SetActive(false);
        Debug.Log("Fim do evento " +eventID);
        StopCoroutine("EventExecuter");
	}
}
