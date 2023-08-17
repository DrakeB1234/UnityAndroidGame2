using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed; 

    private Rigidbody2D rb;
    private float horizontalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    
    private void Update()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, 0f);
    }
    
    public void MovePlayer(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
    }
}
