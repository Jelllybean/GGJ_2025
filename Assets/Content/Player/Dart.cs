using System;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] private LayerMask overlapSphereMask;
    [SerializeField] private float overlapSphereRadius;
    
    // Update is called once per frame
    void Update()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, overlapSphereRadius, overlapSphereMask);

        if (colls != null && colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].TryGetComponent(out Enemy e))
                {
                    e.OnDartHit();
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }
}
