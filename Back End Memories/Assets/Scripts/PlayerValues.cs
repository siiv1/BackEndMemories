using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    [Header("Health")]
    private static int maxHealth = 4;
    public int health = maxHealth;
    public float mvmtMod = 1f;

    [Header("Energy")]
    public float currentBattery = Battery.FullPower;
    public float offhandBattery;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (currentBattery > 0f)
        {
            Deplete();

            if (currentBattery <= 0f)
            {
                if (offhandBattery <= 0f)
                {
                    PlayerMovement pm = GetComponent<PlayerMovement>();
                    pm.moveSpeed = 0f;
                    pm.maxJumps = 0;
                    // GameOver
                }
                else
                {
                    currentBattery = offhandBattery;
                    offhandBattery = 0f;
                    // maybe some graphical stuff
                }
            }
        }
    }

    private void Deplete() 
    {
        currentBattery -= Battery.DepletionRate;
    }

    public void InsertBattery(Battery battery)
    {
        offhandBattery = currentBattery;
        currentBattery = battery.RemainingPower();
        Destroy(battery.gameObject);
    }
}
