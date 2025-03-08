using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    Battery NextBattery; // possibly use for easier coordinate checks; and also for checking if equipped battery has backup available (failure means game over)
    double Power; // remaining battery
    double DepletionRate; // depletion per tick
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Power -= DepletionRate;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Power <= 0)
        {
            // Player.SlotBattery(NextBattery);
        }
    }
}
