using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Collider2D cd;

    public float foodValue;

    // Start is called before the first frame update
    private void Awake() {
        cd = GetComponent<Collider2D>();
    }
   
    
}
