using Unity.Mathematics;
using UnityEngine;

public class EnemyProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public float throwingForce = 500;
    public float cooldown;

    private float lastShootTime;

    public void Shoot()
    {
        if (lastShootTime + cooldown < Time.time)
        {
            GameObject go = Instantiate(projectile, transform.position, quaternion.identity);
        
            go.GetComponent<Rigidbody>().AddForce(transform.forward * throwingForce, ForceMode.Impulse);
            lastShootTime = Time.time;
        }
    }
}
