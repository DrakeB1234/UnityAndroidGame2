using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    private void Awake() 
    {
        Invoke("DestroyProjectile", lifeTime); 
    }
    
    private void Update() 
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
