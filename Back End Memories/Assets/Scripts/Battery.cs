using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battery : MonoBehaviour
{
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

    private void Awake()
    {
        if (Power < .1f)
        {
            float power = UnityEngine.Random.Range(0f, FullPower);

            if (power <= DamagedChance)
            {
                damaged = true;
                Power = 0f;
            }
            else
            {
                damaged = false;

                if (power < MinimumPower)
                    Power = (power / MinimumPower) * (FullPower - MinimumPower) + MinimumPower;

                else Power = power;
            }
        }
    }

    public Battery() 
    {
        if (batteryNum == batteries.Length)
        {
            Battery[] newBatteries = new Battery[batteryNum * 2];
            for (int i = 0; i < batteryNum; i++) newBatteries[i] = batteries[i];
            batteries = newBatteries;
        }

        batteryId = batteryNum;
        batteries[batteryNum++] = this;
    }

    public Battery(float p)
    {
        Power = p;
        batteryId = batteryNum;
        damaged = false;

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
        return (new Battery()).getId();
    }

    public static int GetBattery(float power) {
        return (new Battery(power)).getId();
    }

    public float Deplete()
    {
        this.Power -= DepletionRate;
        return this.Power;
    }

    public float RemainingPower() 
    {
        return this.Power;
    }

    public int getId()
    {
        return this.batteryId;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (this.damaged)
            {
                Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/ExplosionPrefab.prefab", typeof(GameObject)), GetComponent<Rigidbody2D>().position, Quaternion.identity);
                batteries[this.batteryId] = null;
                Destroy(gameObject);
            }

            else if (this.Power <= 0) Destroy(gameObject);
            else collider.GetComponent<PlayerValues>().InsertBattery(this);
        }
    }
}
