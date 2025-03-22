using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    Battery NextBattery; // possibly use for easier coordinate checks; and also for checking if equipped battery has backup available (failure means game over)
    float Power; // remaining battery
    static float DepletionRate; // depletion per tick
    static float MinimumPower;
    static float FullPower;
    // Start is called before the first frame update
    void Start()
    {
        Power = UnityEngine.Random.Range(MinimumPower,FullPower);
    }

    void Update()
    {
            
    }

    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Power <= 0)
        {
            // Player.SlotBattery(NextBattery);
        }
    }

    public float Deplete() {
        this.Power -= DepletionRate;
        return this.Power;
    }
}
