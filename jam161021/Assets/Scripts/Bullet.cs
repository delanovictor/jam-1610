using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    public GameObject rocketPrefab;
    
    void Start() {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (this.gameObject.tag == "Bullet2" && other.gameObject.tag != "Player") {
            Destroy(gameObject);
            Instantiate(rocketPrefab,this.gameObject.transform.position,this.gameObject.transform.rotation);
        }
        else if(other.gameObject.tag == "Fish" || other.gameObject.tag == "Plant"  || other.gameObject.layer == 8){
            Destroy(gameObject);
        }
    }
}
