using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageMeta : MonoBehaviour
{
    int tileCount; // height of level = number of tiles to be generated
    int[] tileStack; // array storing semirandomly generated tile id's, so that they can be despawned and recalled

    int batteryCount; // number of batteries to be generated
    float[][] batteries; // array storing battery coordinates and remaining power: {X,Y,Power}

    private void Start()
    {
        // initialize tileStack to size tileCount
        // prepopulate 0 and length-1, then semirandomly generate the rest

        PopulateBatteries();
    }

    private void PopulateBatteries()
    {
        // initialize batteryCount based on tileCount and difficulty level
        // for (int i = 0; i < batteryCount; i++) {batteryStructure.add(Battery.getBattery());}
        // generate coordinates for initial battery spawns

        // will need to update coordinates and remove 
    }


}
