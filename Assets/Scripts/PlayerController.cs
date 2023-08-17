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
    private Transform groundPos; 
    [SerializeField] 
    private LayerMask groundLayer; 

    [SerializeField] 
    private Animator playerAnimator; 

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
            playerAnimator.SetBool("isJumping", false);

            // Reset player jumps
            jumpRemaining = jumpAmount;
        }
        else
        {
            // Ensure player is in jumping state
            playerAnimator.SetBool("isJumping", true);
        }
    }

    // Input functions
    
    public void MovePlayer(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<Vector2>().x;
        horizontalInput = val;

        // Set animator to walk animation
        playerAnimator.SetBool("isRunning", true);

        // Flip character according to input value
        if (!isFacingRight && val < 0)
            FlipPlayer();
        else if (isFacingRight && val > 0)
            FlipPlayer();
        // Value is 0, set animator to idle animation
        else if (val == 0)
            playerAnimator.SetBool("isRunning", false);
    }

    public void JumpPlayer(InputAction.CallbackContext context)
    {        
        // If the player has remaining jumps
        if (context.started && jumpRemaining > 0)
        {
            playerAnimator.SetTrigger("takeOff");
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
