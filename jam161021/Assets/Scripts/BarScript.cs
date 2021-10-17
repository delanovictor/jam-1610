using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider slider;
   
    public void setValue(float value){
        slider.value = value;
    }

    public void setMax(float value){
        slider.maxValue = value;
    }
}
