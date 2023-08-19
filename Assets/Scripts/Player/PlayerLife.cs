using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] 
    private int lifePointsMax; 
    [SerializeField] 
    private float respawnDelay; 
    [SerializeField] 
    private GameObject healthRowUI;
    [SerializeField] 
    private GameObject eggHealthObj;
    [SerializeField] 
    private GameObject characterObj;
    [SerializeField] 
    private Animator characterAnimator;

    [SerializeField] 
    private Transform currentSpawnPoint; 

    private int lifePoints;
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Awake() 
    {
        // Assign Player max health
        lifePoints = lifePointsMax;

        // Get components from player
        rb = GetComponent<Rigidbody2D>();
        playerInput = gameObject.GetComponent<PlayerInput>();

        // Add health objs to player UI
        for (int i = 0; i < lifePoints; i++)
        {
            Instantiate(eggHealthObj, healthRowUI.transform);
        }
    }

    // Collision Triggers
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("FallDeath"))
        {
            PlayerFallDeath();
        }
    }

    private void UpdatePlayerHealthUI()
    {
        var curVal = 0;
        foreach (Transform child in healthRowUI.transform)
        {
            if (curVal < lifePoints)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.GetComponent<Animator>().SetTrigger("eggDestroy");
            }
            curVal++;
        }
    }

    private void PlayerFallDeath()
    {
        lifePoints--;

        if (lifePoints > 0)
        {
            UpdatePlayerHealthUI();
        }
        else
        {
            UpdatePlayerHealthUI();
            Debug.Log("Outta Lives :()");
        }

        // Hide player and stop physics
        DisablePlayer();

        Invoke("RespawnPlayer", respawnDelay);
    }

    private void RespawnPlayer()
    {
        // Move player to current spawn point
        transform.position = currentSpawnPoint.position;

        // Renable player
        RenablePlayer();
    }

    private void DisablePlayer()
    {
        // Disable RB, set character obj unactive, reset animator values
        rb.simulated = false;
        characterObj.SetActive(false);

        // Disable player controls
        playerInput.DeactivateInput();
    }

    private void RenablePlayer()
    {
        rb.simulated = true;
        characterObj.SetActive(true);

        // Renable player controls
        playerInput.ActivateInput();
    }
}
