using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = LevelManagerScript.instance.characterMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = LevelManagerScript.instance.characterCurrentHp;
       
    }
}
