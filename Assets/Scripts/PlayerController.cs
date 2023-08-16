using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    public float playerSpeed; 
    [SerializeField] 
    private GameObject projectilePrefab; 
    [SerializeField] 
    private Transform firePos; 

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    

        Application.targetFrameRate = 60;
    }
    
    private void FixedUpdate() 
    {
        rb.velocity = moveInput * playerSpeed;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 vector = context.ReadValue<Vector2>();

        moveInput = vector;
    }

    public void FireWeapon(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Instantiate(projectilePrefab, firePos.position, Quaternion.identity);
    }
}
