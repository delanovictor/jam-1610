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
    private  float LastFireGun;
    private  float LastFireRocket;

     public BarScript sliderGun;
     public BarScript sliderRocket;

    void Start(){
    
        sliderGun.setMax(1);
        sliderGun.setValue(0);
        
        sliderRocket.setMax(1);
        sliderRocket.setValue(0);
    }
    void Update()
    {   
        if(Input.GetButtonDown("Fire1")  && (Time.time - LastFireGun > FireRateGun))   {
            LastFireGun = Time.time;
            Shoot();
        }  
        if(Input.GetButtonDown("Fire2")  && (Time.time - LastFireRocket > FireRateRocket))   {
            LastFireRocket = Time.time;
            Shoot2();
        }  

        sliderRocket.setValue((Time.time - LastFireRocket) / FireRateRocket); 
        sliderGun.setValue((Time.time - LastFireGun) / FireRateGun);  
    }
    
    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
    } 

    void Shoot2(){
        Instantiate(bulletRocketPrefab, firePoint.position,firePoint.rotation);
    } 
}
