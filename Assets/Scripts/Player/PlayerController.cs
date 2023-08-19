using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed; 
    [SerializeField] 
    private float jumpForce; 
    [SerializeField] 
    private int jumpAmount; 
    [SerializeField] 
    private int timeBeforeSprint; 
    [SerializeField] 
    private Transform groundPos; 
    [SerializeField] 
    private LayerMask groundLayer; 

    [SerializeField] 
    private Animator characterAnimator; 

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight;
    private int jumpRemaining;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   

        Application.targetFrameRate = 60;
    }
    
    private void Update()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (GroundCheck())
        {
            // Ensure player is not in jumping state
            characterAnimator.SetBool("isGrounded", false);
        }
        else
        {
            // Ensure player is in jumping state
            characterAnimator.SetBool("isGrounded", true);
        }
    }

    // Input functions
    
    public void MovePlayer(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<Vector2>().x;
        horizontalInput = val;

        // Set animator to walk animation
        characterAnimator.SetBool("isRunning", true);

        // Flip character according to input value
        if (!isFacingRight && val < 0)
            FlipPlayer();
        else if (isFacingRight && val > 0)
            FlipPlayer();
        // Value is 0, set animator to idle animation
        else if (val == 0)
            characterAnimator.SetBool("isRunning", false);
    }

    public void JumpPlayer(InputAction.CallbackContext context)
    {        
        // If the player is on ground, reset jump amount
        if (context.performed && GroundCheck())
        {
            // Reset player jumps
            jumpRemaining = jumpAmount;

            characterAnimator.SetTrigger("takeOff");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemaining--;
        }
        else if (context.performed && jumpRemaining > 0)
        {
            characterAnimator.SetTrigger("takeOff");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemaining--;
        }
    }

    // Functions
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundPos.position, 0.1f, groundLayer);
    }

    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
