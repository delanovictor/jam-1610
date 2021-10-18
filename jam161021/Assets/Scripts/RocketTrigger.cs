using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrigger : MonoBehaviour
{
    void Start(){
       StartCoroutine(ShootRocket(0.5f,gameObject));
    }
    IEnumerator ShootRocket(float delay, GameObject rocketGameObject) {

        yield return new WaitForSeconds (delay);

        Destroy(rocketGameObject);
        StopCoroutine("ShootRocket");
    }
}
