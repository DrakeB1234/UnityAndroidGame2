using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed; 
    [SerializeField] 
    private float jumpForce; 
    [SerializeField] 
    private Transform groundPos; 
    [SerializeField] 
    private LayerMask groundLayer; 

    [SerializeField] 
    private Animator playerAnimator; 

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    
    private void Update()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    // Input functions
    
    public void MovePlayer(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<Vector2>().x;
        horizontalInput = val;

        // Set animator to walk animation
        playerAnimator.SetBool("isRunning", true);

        // Flip character according to input value
        if (!isFacingRight && val == -1)
            FlipPlayer();
        else if (isFacingRight && val == 1)
            FlipPlayer();
        // Value is 0, set animator to idle animation
        else if (val == 0)
            playerAnimator.SetBool("isRunning", false);
    }

    public void JumpPlayer(InputAction.CallbackContext context)
    {
        if (GroundCheck() && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
