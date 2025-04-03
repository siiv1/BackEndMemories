using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryActions // : MonoBehaviour
{}
/*    public static float MinimumPower = 500f;
    public static float FullPower = 1000f;
    public static float DepletionRate = FullPower / (60f * 30f); // depletion per tick
    public static float DamagedChance = 0.1f;
    private static Battery[] batteries = new Battery[1];
    private static int batteryNum;


    public BatteryActions() { }
    // Start is called before the first frame update

    void Start()
    {
    }

    public static void createBattery()
    { 
        if (batteries.Length == batteryNum)
        {
            Battery[] newb = new Battery[batteryNum * 2];
            for (int i = 0; i < batteryNum; i++) newb[i] = batteries[i];
            batteries = newb;
        }

        batteries[batteryNum] = (UnityEngine.Random.Range(0f, 1f) < DamagedChance) ?
            new (GameObject)Initialize(DamagedBattery) :
            new (GameObject)Initialize(Battery);

        return batteries[batteryNum++].getId();
    }

    public static Battery getBattery(int id) {
        return batteries[id];
    }
}*/
