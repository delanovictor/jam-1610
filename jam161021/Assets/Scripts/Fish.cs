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

    public Vector2 maxSpeed;
    public Vector2 currentSpeed;
    public Vector3 targetPosition;
    public float smoothVelocity;
    public float smoothTime;
    public GameObject targetObject;
    
    public float wanderRadius;
    public bool isWandering;


    public float maxHunger;
    public float currentHunger;
    public float rateHunger;

    public float hungerThreshold;
    public bool isWalkingToFood;

    public float viewRadius;
	public List<Transform> visibleFoods = new List<Transform>();

    public float secondsToRandomPush;
    public float lastPush;


    public Collider2D cd;

    void Start()
    {
        currentHunger = maxHunger;

        secondsToRandomPush = Random.Range(5, 15);

        maxSpeed.x += Random.Range(0, 2);
        maxSpeed.y += Random.Range(0, 2);

        lastPush = Time.timeSinceLevelLoad;

        StartCoroutine ("FindTargetsWithDelay", .1);
        
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad - lastPush > secondsToRandomPush){
            lastPush = Time.timeSinceLevelLoad;
            secondsToRandomPush = Random.Range(5, 15);
        };

  
        currentHunger -= rateHunger * Time.deltaTime;
    }

    private void FixedUpdate() {
        Vector3 direction = new Vector3();


        if(targetObject == null){
            Wander();
        }else{
            targetPosition = targetObject.transform.position;
        }

        direction = targetPosition - transform.position;
    
       
        direction = direction.normalized;

        currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, maxSpeed.x, ref smoothVelocity, smoothTime);
        currentSpeed.y = Mathf.SmoothDamp(currentSpeed.y, maxSpeed.y, ref smoothVelocity, smoothTime);

        Vector3 velocity = new Vector3(direction.x * currentSpeed.x, direction.y * currentSpeed.y, 0);

        transform.Translate(velocity * Time.deltaTime);

    }

    IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

    void Wander(){
        Vector3 direction = targetPosition - transform.position;

        if(isWandering){
            if((Vector3.SqrMagnitude(direction) - 1) < 1){
                isWandering = false;
            }
        }else{
            if(!isWalkingToFood){
                isWandering = true;
                float randomAngle = Random.Range(0,360);
                targetPosition = new Vector3(Mathf.Cos(randomAngle) * wanderRadius, Mathf.Sin(randomAngle) * wanderRadius, 1);
            }
        }
    }

	void FindVisibleTargets() {
		visibleFoods.Clear ();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), viewRadius);
        
		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance (transform.position, target.position);
            float dstToNewTarget = Vector3.Distance (targetsInViewRadius[i].transform.position, transform.position);
              
            if(dstToTarget > dstToNewTarget || isWandering){
                if(fishType == FishType.Herbivore){
                    if(targetsInViewRadius[i].tag == "Plant"){
                        if(currentHunger < hungerThreshold){
                            isWandering = false;
                            isWalkingToFood = true;
                            targetObject = targetsInViewRadius[i].gameObject;
                        }
                    }
                }else{
                    if(fishType == FishType.Carnivore){
                        Fish targetFish = targetsInViewRadius[i].gameObject.GetComponent<Fish>();
                        if(targetFish.fishType == FishType.Herbivore){
                            isWandering = false;
                            isWalkingToFood = true;
                            targetObject = targetsInViewRadius[i].gameObject;
                        }
                    }
                }
            }
         
		}
	}
    
   private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision");
        switch(fishType){
            case FishType.Herbivore:
                Debug.Log("1");

                if(other.gameObject.tag == "Plant"){
                    Debug.Log("2");

                    if(other.gameObject == targetObject){
                        Debug.Log("3");

                        StartCoroutine(Eat(other.gameObject, 1.5f));
                    }
                }
                
                if(other.gameObject.tag == "Bullet"){
                    Die();
                }
            break;
            case FishType.Carnivore:

            break;
        }
    }
    
    IEnumerator Eat(GameObject target, float time)
    {
        Debug.Log("4");

        yield return new WaitForSeconds(time);
        Debug.Log("5");

        targetObject = null;
        isWalkingToFood = false;
        Destroy(target);
        currentHunger += 40f;
        StopCoroutine("Eat");
    }

    void Die(){
        Destroy(gameObject);
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
