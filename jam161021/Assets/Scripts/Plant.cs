using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    Collider2D cd;

    // Start is called before the first frame update
    private void Awake() {
        cd = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            Die();
        }
        if(other.gameObject.tag == "Bullet2"){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }

}