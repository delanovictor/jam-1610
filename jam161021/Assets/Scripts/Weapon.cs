using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;
    public GameObject bulletRocketPrefab;
    
    public float FireRateGun = 1.0f;
    public float FireRateRocket = 5.0f;
 
 //The actual time the player will be able to fire.
    private  float NextFireGun;
    private  float NextFireRocket;

     public Slider sliderGun;
     public Slider sliderRocket;

    void Update()
    {   
        if(Input.GetButtonDown("Fire1")  && Time.time > NextFireGun)   {
            NextFireGun = Time.time + FireRateGun;
            Debug.Log(NextFireGun);
            sliderGun.value = NextFireGun;
            Shoot();
        }  
        if(Input.GetButtonDown("Fire2")  && Time.time > NextFireRocket)   {
            NextFireRocket = Time.time + FireRateRocket;
            sliderRocket.value = NextFireRocket;   
            Shoot2();
        }  
    }
    
    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
    } 

    void Shoot2(){
        Instantiate(bulletRocketPrefab, firePoint.position,firePoint.rotation);
    } 
}
