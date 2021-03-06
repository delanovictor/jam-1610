using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float currentEventTime;
    public Text currentEventText;


    public GameObject gameOverUI;
    public Text gameOverCause;
    public Text gameOverTime;

    public bool gameOver;

    private BarScript currentEventBar;
    private float originalPlantInterval;
    private float originalPlantGrowth;
    private float originalHerbivoreGrowth;
    private float originalHerbivoreInterval;


    public float eventPlantInterval;
    public float eventPlantGrowth;
    public float eventCarnivoreSpeed;
    public float eventHerbivoreGrowth;
    public float eventHerbivoreInterval;
    public float eventPlantDeGrowth;
    public float eventPlantDeath;


    public bool mainMenu;

    public GameObject Player;

    void Awake()
    {
        
        foreach(Specie s in species){
            s.holder = new GameObject(s.name);

            for(int i = 0; i < s.startNumber; i++){
                float randomX = Random.Range(0, mapSizeX);
                float randomY = Random.Range(0, mapSizeY);

                Instantiate(s.prefab, new Vector3(randomX, randomY, 1), Quaternion.identity, s.holder.transform);
            }
            if(!mainMenu){
                s.bar.setMax(s.maxNumber);
            }
            s.lastSpawn =  Time.timeSinceLevelLoad;
        }
      
        originalPlantInterval = species[0].growthInterval;
        originalPlantGrowth = species[0].growthRate;

        originalHerbivoreGrowth = species[1].growthRate;
        originalHerbivoreInterval = species[1].growthInterval;



        Camera.main.transform.position = new Vector3(mapSizeX/2, mapSizeY/2, 0);
        
        if(!mainMenu){
            nextEventBar.setMax(1);
            nextEventBar.setValue(0);

            currentEventBar = currentEventObject.GetComponentInChildren<BarScript>();
            currentEventObject.SetActive(false);
        }
        

        StartCoroutine ("RandomEventGenerator", timeBetweenEvents);

    }

    private void Update() {
        if(!mainMenu){
            nextEventBar.setValue((Time.timeSinceLevelLoad - nextEventTime) / timeBetweenEvents);
            currentEventBar.setValue((Time.timeSinceLevelLoad - currentEventTime) / eventDuration);
        }
    }
    
    private void LateUpdate() {
        foreach(Specie s in species){
            s.currentNumber = s.holder.transform.childCount;
            if(!mainMenu){
                s.bar.setValue(s.currentNumber);
                s.bar.setValue(s.currentNumber);
            }

            if(s.currentNumber == 0){
                GameOver("A Population Was Extinct");
            }else{
                if(s.currentNumber >= s.maxNumber){
                    GameOver("A Population Got Out of Control");
                }
            }

            if(!gameOver){
                if(((Time.timeSinceLevelLoad - s.lastSpawn) ) > s.growthInterval + s.growthStartOffset){
                    s.growthStartOffset = 0;
                    s.generation ++;
                    int newOffspring = (int)(s.currentNumber * s.growthRate);


                    if(newOffspring == 0 && s.currentNumber > 0){
                        newOffspring = 1;
                    }

                    if (newOffspring < s.minGenReprodution)
                    {
                        newOffspring = s.minGenReprodution;
                    }

                    for(int i = 0; i < newOffspring; i++){
                        float randomX = Random.Range(0, mapSizeX);
                        float randomY = Random.Range(0, mapSizeY);

                        Instantiate(s.prefab, new Vector3(randomX, randomY, 1), Quaternion.identity, s.holder.transform);
                    }
            
                

                    s.lastSpawn = Time.timeSinceLevelLoad;
                }
            }
        
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + new Vector3(mapSizeX/2, mapSizeY/2, 1),new Vector3(mapSizeX, mapSizeY, 1)
        );
    }

    private void GameOver(string message){
        if(!gameOver){
            Destroy(Player);
            gameOver = true;
            gameOverUI.SetActive(true);
            gameOverCause.text = message;
            float time = Time.timeSinceLevelLoad;

            gameOverTime.text = "You maintened the balance for " + time.ToString("C2") + " seconds";
        }
    }
    
    public void TryAgain(){
        SceneManager.LoadScene("SceneFish",LoadSceneMode.Single);
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }

    IEnumerator RandomEventGenerator(float delay) {
		while (true) {
            nextEventTime = Time.timeSinceLevelLoad;
			yield return new WaitForSeconds (delay);

            StartCoroutine(EventExecuter(Random.Range(1,5), eventDuration));
		}
	}

    IEnumerator EventExecuter(int eventID, float delay) {
        if(!mainMenu){
            currentEventTime = Time.timeSinceLevelLoad;
            currentEventObject.SetActive(true);
        }

        string eventName = "";

        switch(eventID){
            case 1:
                eventName = "Carnivore's Frenzy";
                Fish.eventCarnivoreSpeed = eventCarnivoreSpeed;
            break;

            case 2:
                eventName = "Plant's HyperGrowth";
                species[0].growthInterval = eventPlantInterval;
                species[0].growthRate = eventPlantGrowth;
            break;

            case 3:
                eventName = "Herbivore's Fertility";
                species[1].growthRate = eventHerbivoreGrowth;
                species[1].growthInterval = eventHerbivoreInterval;
            break;
            
            case 4:
                eventName = "Plant's Death";
                int numberOfPlants = Mathf.CeilToInt(species[0].holder.transform.childCount * eventPlantDeath);

                for(int i = 0; i < numberOfPlants; i++){
                    Destroy(species[0].holder.transform.GetChild(i).gameObject);
                }

                species[0].growthRate = eventPlantDeGrowth;

            break;
        }
        
        currentEventText.text = eventName;


        yield return new WaitForSeconds (delay);

        species[0].growthInterval = originalPlantInterval;

        species[0].growthRate = originalPlantGrowth;
        species[1].growthRate = originalHerbivoreGrowth;
        species[1].growthInterval = originalHerbivoreInterval;

        Fish.eventCarnivoreSpeed = 0;


        currentEventObject.SetActive(false);
        StopCoroutine("EventExecuter");
	}
}
