using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //Upward movespeed
    private int platformId = 0;
    private float speed;
    private Rigidbody2D platformRB;
    private float yBase;
    public float yDirection;
    private float xDirection;
    private int remainingWait = 0;
    private int waitCooldown = 0;

    //Array of possible positions on x axis
    private static float[] xLocations = { -4f, -.5f, 3f };
    private static int lastX = 0;
    private static int platformCount = 0;
    private static int maxPlatforms = pSpawner.maxPlatforms;
    private static PlatformScript[] platforms = new PlatformScript[maxPlatforms];
    private static float lastY;
    public static float yOffset = 8f;
    public static float playerRoom = 3.5f;
    private static float vertRange = yOffset - playerRoom;
    private static int waitFrames = 60;

// Start is called before the first frame update
    void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
        platformId = platformCount;
        if (platformCount++ == platforms.Length)
        {
            PlatformScript[] npl = new PlatformScript[platformCount * 2];
            for (int i = 0; i < platformCount; i++) npl[i] = platforms[i];
            platforms = npl;
        }
        platforms[platformId] = this;

        float yStart;
        // setting y coordinate
        if (platformId == 0)
        {
            yBase = platformRB.position.y;
            yDirection = 1;
            xDirection = 0;
            lastY = yBase - vertRange;
            yStart = yBase - (vertRange * 3 / 4);
            speed = 3f / 50f;
        }
        else
        {
            speed = UnityEngine.Random.Range(2f, 4f) / 50f;
            
            if (platformId % 2 == UnityEngine.Random.Range(0, 2)) 
            {
                xDirection = platformId % 2 == 1 ? 1 : -1;
                yDirection = 0;
            }
            else {
                xDirection = 0;
                yDirection = platformId % 2 == 0 ? 1 : -1;
            }

            if (yDirection == 0)
            {
                lastY -= playerRoom;
                if (platforms[platformId - 1].yDirection == 0) lastY -= 1f;

                yStart = yBase = lastY;
            }
            else
            {
                yBase = lastY - playerRoom;
                lastY = yBase - vertRange;
                yStart = yBase - (vertRange/2) - (vertRange / 4 * yDirection);
            }
        }
        
        // choosing semirandom X position to start
        if (xDirection != 0)
        {
            transform.position = new Vector2(UnityEngine.Random.Range(xLocations[0],xLocations[xLocations.Length-1]),yBase);
        }
        else 
        {
            int nextX = UnityEngine.Random.Range(0, xLocations.Length);
            while (nextX == lastX) nextX = UnityEngine.Random.Range(0,xLocations.Length);
            lastX = nextX;
            
            transform.position = new Vector2(xLocations[lastX],yStart);
        }
    }

// Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (remainingWait > 0) remainingWait--;
        else
        {
            transform.position = new Vector2(transform.position.x + speed * xDirection, transform.position.y + speed * yDirection);
            if (waitCooldown > 0) waitCooldown--;
            else CheckMovementBounds();
        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        collider.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        collider.transform.SetParent(null);
    }

    private void CheckMovementBounds()
    {
        if (yDirection != 0)
        {
            //When yPos exceeds the bounds; record position and reverse motion
            if (transform.position.y > yBase)
            {
                yDirection = -1;
                waitCooldown = remainingWait = waitFrames;
            }
            else if (transform.position.y < yBase-vertRange){
                yDirection = 1;
                waitCooldown = remainingWait = waitFrames;
            }
        }
        else if (xDirection != 0)
        {
            if (transform.position.x < xLocations[0]+.9f)
            {
                xDirection = 1;
                waitCooldown = remainingWait = waitFrames;
            }
            else if (transform.position.x > xLocations[xLocations.Length-1]-.9f){
                xDirection = -1;
                waitCooldown = remainingWait = waitFrames;
            } 
        }

    }

    public static float GetBottom() 
    {
        return lastY;
    }
}
