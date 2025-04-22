using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public GameObject battery;
    public GameObject damagedBattery;

    public static float FullPower = 1000f;
    public static float MinimumPower = FullPower / 2;
    public static float DamagedChance = FullPower / 10;
    public static float DepletionRate = FullPower / 1800; // 30 seconds

    public static Battery[] batteries = new Battery[8];
    private static int batteryNum = 0;

    float Power; // remaining battery
    public int batteryId;
    
    public bool damaged;
    GameObject Explosion;

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
    private static Battery DamagedBattery() {
        Battery dmg = new Battery(0f);
        dmg.damaged = true;
        return dmg;
    }

    public static int GetBattery()
    {
        return GetBattery(UnityEngine.Random.Range(0f, FullPower));
    }

    public static int GetBattery(float power) {
        if (batteryNum == batteries.Length)
        {
            Battery[] newBatteries = new Battery[batteryNum * 2];
            for (int i = 0; i < batteryNum; i++) newBatteries[i] = batteries[i];
            batteries = newBatteries;
        }

        if (power <= DamagedChance)
            batteries[batteryNum] = DamagedBattery();

        else if (power < MinimumPower)
            batteries[batteryNum] = new Battery((power / MinimumPower) * (FullPower - MinimumPower) + MinimumPower);

        else batteries[batteryNum] = new Battery(power);

        return batteryNum++;
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

    public int Pickup()
    {
        if (this.damaged) 
        {
            Instantiate(Explosion, GetComponent<Rigidbody2D>().position, Quaternion.identity);
            batteries[this.batteryId] = null;
            Destroy(this);
            return -1;
        }

        if (this.Power <= 0) return -1;
        else return this.batteryId;
    }
}
