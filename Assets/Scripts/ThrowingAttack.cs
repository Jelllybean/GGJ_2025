using UnityEngine;

public class ThrowingAttack : MonoBehaviour
{
    public Rigidbody rb;
    public float throwingForce = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ThrowAttack()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * throwingForce, ForceMode.Impulse);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().RemoveHealth(40);
        }
	}
}
