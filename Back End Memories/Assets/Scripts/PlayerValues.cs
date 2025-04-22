using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    [Header("Health")]
    private static int maxHealth = 4;
    private int health = maxHealth;
    public float mvmtMod = 1f;

    [Header("Energy")]
    private float playerPower;
    private Battery currentBattery;
    private Battery offhandBattery;


    // Start is called before the first frame update
    void Start()
    {
        currentBattery = new Battery(Battery.FullPower);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        mvmtMod = health / (float)maxHealth;
        BatteryUpdate();
    }

    private void BatteryUpdate()
    {
        playerPower = currentBattery.Deplete();

        if (currentBattery == null)
        {
            if (offhandBattery == null)
            {
                // GameOver
            }
            else
            {
                currentBattery = offhandBattery;
                offhandBattery = null;
                // maybe some graphical stuff
            }
        }
    }

}
