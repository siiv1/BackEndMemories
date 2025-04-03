using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : MonoBehaviour
{
    float Power; // remaining battery
    int batteryId;
    // Start is called before the first frame update

    /*
     * public void Initialize(){
        this.Initialize(UnityEngine.Random.Range(BatteryActions.MinimumPower, BatteryActions.FullPower));
    }
    */

    /*
     * public void Initialize(float p) {
        Power = p;
        batteryId = batteryNum++;

        if (batteryId == batteries.Length)
        {
            Battery[] newb = new Battery[batteryId * 2];
            for (int i = 0; i < batteryId; i++) newb[i] = batteries[i];
            batteries = newb;
        }
        batteries[batteryId] = this;
    Battery NextBattery; // possibly use for easier coordinate checks; and also for checking if equipped battery has backup available (failure means game over)
    float Power; // remaining battery
    static float DepletionRate; // depletion per tick
    static float MinimumPower;
    static float FullPower;
    static float DamagedChance;
    // Start is called before the first frame update

    /*public Battery() : this(UnityEngine.Random.Range(MinimumPower, FullPower)) {}
    public Battery(float p) {
        Power = p;
        
    }
    */

    void Start()
    {
    }

   /* public static Battery getBattery() {
        if (UnityEngine.Random.Range(0f, 1f) < BatteryActions.DamagedChance) return new DamagedBattery();
        if (UnityEngine.Random.Range(0f, 1f) < DamagedChance) return new DamagedBattery();
        else return new Battery();
    }
   */

    void Update()
    {
            
    }

    void FixedUpdate()
    {
    }

    // Update is called once per frame
   /* void LateUpdate()
    {
        if (Power <= 0)
        {
            Player.SlotBattery(NextBattery);
        }
    }
   */

   /* public float Deplete() {
        this.Power -= BatteryActions.DepletionRate;
        return this.Power;
    }

    public int getId() {
        return this.batteryId;
    }

    public Battery Pickup()
    {
        if (this.Power <= 0) return null;
        else return this;
        this.Power -= DepletionRate;
        return this.Power;
    }

    class DamagedBattery : Battery {
        public DamagedBattery() { }
    }*/
}
