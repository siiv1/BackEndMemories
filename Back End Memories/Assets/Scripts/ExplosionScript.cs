using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private Vector3 inc;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(0f, 0f, 1f);
        inc = new Vector3(.04f, .04f, 0);
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += inc;
        if (++tick >= 60) 
        {
            Destroy(this);
        }
        else if (tick % 20 == 0)
        {
            inc = new Vector3(inc.x / 2, inc.y / 2, 0);
        }
    }
}
