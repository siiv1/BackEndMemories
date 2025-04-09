using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //Upward movespeed
    private int platformId = 0;
    private float speed = 3f;
    private Rigidbody2D platformRB;
//x position platform starts at
    private float xStart;
    private float yBase;
    private float yDirection;
    private float xDirection;
    private Rigidbody2D player;
    private int remainingWait = 0;
    private int waitCooldown = 0;

    //Array of possible positions on x axis
    private static float[] xLocations = { -4f, -.5f, 3f };
    private static int lastX = 0;
    private static int platforms = 0;
    private static int maxPlatforms = 5;
    private static float initialY;
    private static float yOffset = -6f;
    private static int waitFrames = 60;

// Start is called before the first frame update
    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        platformId = platforms++;

        // setting y coordinate
        if (platformId == 0) yBase = initialY = platformRB.position.y;
        else yBase = initialY + platformId * yOffset;

        float yStart = UnityEngine.Random.Range(yBase,yBase+5);

        // setting movement direction
        if (platformId % 2 == UnityEngine.Random.Range(0,3))
        {
            yDirection = 0;
            xDirection = 1;
            yBase += yOffset/2;
        }
        else
        {
            yDirection = 1;
            xDirection = 0;
        }

        // choosing semirandom X position to start
        if (xDirection != 0)
        {
            transform.position = new Vector2(UnityEngine.Random.Range(xLocations[0],xLocations[xLocations.Length-1]),yStart);
        }
        else 
        {
            int nextX = UnityEngine.Random.Range(0, xLocations.Length);
            while (nextX == lastX) nextX = UnityEngine.Random.Range(0,xLocations.Length);
            lastX = nextX;
            
            transform.position = new Vector2(xLocations[lastX],yStart);
        }

        if (platformId < maxPlatforms) Instantiate(this);
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
            platformRB.velocity = new Vector2(speed * xDirection, speed * yDirection);
            if (waitCooldown > 0) waitCooldown--;
            else CheckMovementBounds();
        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        player = collider.rigidbody;
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        player = null;
    }
    
    private void CheckMovementBounds()
    {
        if (yDirection != 0)
        {
            //When yPos exceeds the bounds; record position and reverse motion
            if (transform.position.y > yBase + 5)
            {
                yDirection = -1;
                waitCooldown = remainingWait = waitFrames;
                platformRB.velocity = new Vector2(0,0);
                if (player != null) player.velocity = new Vector2(player.velocity.x, 0);
            }
            else if (transform.position.y < yBase-.1f){
                yDirection = 1;
                waitCooldown = remainingWait = waitFrames;
                platformRB.velocity = new Vector2(0,0);
            }
        }
        else if (xDirection != 0)
        {
            if (transform.position.x < xLocations[0]-.1f)
            {
                xDirection = 1;
                waitCooldown = remainingWait = waitFrames;
                platformRB.velocity = new Vector2(0f,0f);
            }
            else if (transform.position.x > xLocations[xLocations.Length-1]+.1f){
                xDirection = -1;
                waitCooldown = remainingWait = waitFrames;
                platformRB.velocity = new Vector2(0f,0f);
            }
            else if (player != null && player.velocity.x * xDirection < speed) player.velocity = new Vector2(player.velocity.x + speed*xDirection,player.velocity.y); 
        }

    }
}
