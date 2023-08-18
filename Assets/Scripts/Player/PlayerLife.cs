using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] 
    private int lifePointsMax; 
    [SerializeField] 
    public GameObject healthRowUI;
    [SerializeField] 
    public GameObject eggHealthObj;

    [SerializeField] 
    public Transform currentSpawnPoint; 

    private int lifePoints;

    private void Awake() 
    {
        // Assign Player max health
        lifePoints = lifePointsMax;

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

            // Move player to current spawn point
            transform.position = currentSpawnPoint.position;
        }
        else
        {
            UpdatePlayerHealthUI();
            transform.position = currentSpawnPoint.position;
            Debug.Log("Outta Lives :()");
        }
    }
}
