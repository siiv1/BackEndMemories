using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 5f;
<<<<<<< Updated upstream
    private static float playerMvmtMult = 0.1f;
=======
    public float playerMvmtMult = 0.5f;
>>>>>>> Stashed changes
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Moving the character according to speed
    void Update()
    {
        rb.velocity = new Vector2(playerMvmtMult * horizontalMovement * moveSpeed, rb.velocity.y);   
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
<<<<<<< Updated upstream
        if (context.performed)
        {
            // Hold down jump button = full height
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        else if (context.canceled) {
            // Tap jump button = half height
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
=======
        if (isGrounded())
        {
            if (context.performed)
            {
                // Hold down jump button = full height
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (context.canceled)
            {
                // Tap jump button = half height
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
>>>>>>> Stashed changes
    }
}
