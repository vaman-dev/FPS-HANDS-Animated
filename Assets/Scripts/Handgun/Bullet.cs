using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // Time before the bullet is destroyed

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the bullet after 'lifeTime' seconds
    }
}
