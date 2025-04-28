using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //some public variables for the player
    public Rigidbody2D rb;
    public Animator animator;
    private PlayerValues pv;
    
    [Header("Movement")]
    bool isFacingRight = true;
    public float moveSpeed = 3f;
    //public float playerMvmtMult = 0.5f;
    float horizontalMovement;
 
    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;
    private PlatformScript ground;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallspeed = 12f;
    public float fallSpeedMultiplier = 2f;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding = false;
    //Wall Jumping
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.3f;
    float wallJumpTimer;
    public float wallJumpDampener = 0.995f;
    public Vector2 wallJumpPower = new Vector2(6f, 10f);

    [Header("UI Control")]
    public static bool isUIOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
        pv = gameObject.GetComponent<PlayerValues>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if any UI open 
        if (!isUIOpen)
        {
            GroundCheck();
            Gravity();
            WallSlide();
            WallJump();
            if (!isWallJumping)
            {
                // Moving the character according to their speed
                rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
                animator.SetFloat("magnitude", rb.velocity.magnitude);
                Flip();
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * wallJumpDampener, rb.velocity.y); // reducing wall jump horizontal velocity
            }
        }
        else rb.velocity = Vector2.zero;
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //Fall increasingly faster
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallspeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        //Reading movement value
        horizontalMovement = context.ReadValue<Vector2>().x;

        //Calling the appropriate animation for right and left movement
        if (horizontalMovement > 0)
        {
            animator.SetTrigger("isFacingRight");
        }
        else if (horizontalMovement < 0)
        {
            animator.SetTrigger("isFacingLeft");
        }
    }
    
    //Jumping mechanic according to player's interaction
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                // Hold down jump button = full height
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max((0.5f * (1 + pv.mvmtMod)),0.74f) * jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
            else if (context.canceled)
            {
                // Tap jump button = half height
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;

            }
        }

        //Wall Jump
        if (pv.mvmtMod > 0.5f && context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2((0.5f * (1 + pv.mvmtMod)) * wallJumpDirection * wallJumpPower.x, pv.mvmtMod * wallJumpPower.y); //Jump off wall
            wallJumpTimer = 0;
            if (pv.mvmtMod > 0.75f) jumpsRemaining = maxJumps;
            animator.SetTrigger("jump");
            //Force Flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
            Invoke(nameof(CancelWallJump), wallJumpTime + 0.2f); //Wall Jump lasts 0.5f -- Jump again = 0.6f
        }
    }

    private void WallSlide()
    {
        bool isPressingIntoWall = (horizontalMovement > 0 && IsTouchingRightWall()) || (horizontalMovement < 0 && IsTouchingLeftWall());
        bool rSlide = false;
        bool lSlide = false;

        if (pv.mvmtMod > 0.25f && !isGrounded && isPressingIntoWall)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            //Calling the appropriate animation for right and left wallslides
            if (isPressingIntoWall && IsTouchingRightWall())
            {
                rSlide = true;
                animator.SetBool("rightSlide", rSlide);
            }
            else if (isPressingIntoWall && IsTouchingLeftWall())
            {
                lSlide = true;
                animator.SetBool("leftSlide", lSlide);
            }
        }
        else if (!isPressingIntoWall)
        {
            isWallSliding = false;
            animator.SetBool("rightSlide", isWallSliding);
            animator.SetBool("leftSlide", isWallSliding);
        }
    }

    private bool IsTouchingRightWall()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer) &&
               transform.localScale.x > 0; // Facing right
    }

    private bool IsTouchingLeftWall()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer) &&
               transform.localScale.x < 0; // Facing left
    }

    //Checking first if we're wallsliding, then doing the jump and setting the time delay
    private void WallJump()
    {
        if (pv.mvmtMod > 0.5f && isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            CancelInvoke(nameof(CancelWallJump));
        }
        else if (!isWallSliding && wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    //checking if the player is grounded so that they can jump
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void Flip()
    {
        // Only flip if not wall-sliding, not wall-jumping, and movement direction changes
        if (!isWallSliding && !isWallJumping &&
            (isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
       
    }

    //For adjusting the parameters below and beside the player that detect the ground and wall
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
