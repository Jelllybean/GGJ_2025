using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private LayerMask overlapSphereMask;
    [SerializeField] private float overlapSphereRadius;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;

    private float lastDamageTime = Mathf.NegativeInfinity;
    
    // Update is called once per frame
    void Update()
    {
        if (lastDamageTime + cooldown > Time.time)
        {
            return;
        }
        
        Collider[] colls = Physics.OverlapSphere(transform.position, overlapSphereRadius, overlapSphereMask);

        if (colls != null && colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].TryGetComponent(out PlayerHealth e))
                {
                    e.RemoveHealth(damage);
                    lastDamageTime = Time.time;
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }
}
