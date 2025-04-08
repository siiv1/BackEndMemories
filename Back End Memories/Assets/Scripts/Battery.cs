using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public static float MinimumPower = 500f;
    public static float FullPower = 1000f;
    public static float DepletionRate = FullPower / (60f * 30f); // depletion per tick

    public static float DamagedChance = 0.1f;

    public static Battery[] batteries = new Battery[8];
    private static int batteryNum = 0;

    float Power; // remaining battery
    public int batteryId;


    public Battery() : this(UnityEngine.Random.Range(MinimumPower, FullPower)) { }
    public Battery(float p)
    {
        Power = p;
        batteryId = batteryNum;

        if (batteryNum == batteries.Length)
        {
            Battery[] newBatteries = new Battery[batteryNum * 2];
            for (int i = 0; i < batteryNum; i++) newBatteries[i] = batteries[i];
            batteries = newBatteries;
        }

        batteries[batteryNum++] = this;
    }

    public static int GetBattery()
    {
        if (UnityEngine.Random.Range(0f, 1f) < DamagedChance)
        {
            if (batteryNum == batteries.Length)
            {
                Battery[] newBatteries = new Battery[batteryNum * 2];
                for (int i = 0; i < batteryNum; i++) newBatteries[i] = batteries[i];
                batteries = newBatteries;
            }
            batteries[batteryNum] = new DamagedBattery(batteryNum);
        }
        else batteries[batteryNum] = new Battery(batteryNum);
        return batteryNum-1;
    }

    public float Deplete()
    {
        this.Power -= DepletionRate;
        return this.Power;
    }

    public int getId()
    {
        return this.batteryId;
    }

    public Battery Pickup()
    {
        if (this.Power <= 0) return null;
        else return this;
    }
}
