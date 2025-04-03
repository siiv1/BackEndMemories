using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static float FullPower = Battery.FullPower;
    public int Health;
    public float Power;
    private static float DepletionRate = 2f;
    public Slider powerBar; 

    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
        this.Power = FullPower;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.Power > 0f)
        {
            Power -= DepletionRate;
            powerBar.value = Power;
        }
    }
}
