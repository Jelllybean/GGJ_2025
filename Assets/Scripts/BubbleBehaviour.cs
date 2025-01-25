using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{


	Rigidbody rb;

	public Vector3 gravity;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		gravity = Physics.gravity;
		ReverseGravity();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			rb.constraints = RigidbodyConstraints.FreezePosition;
			other.GetComponent<EnemyBubbleCounter>().AttachBubble(gameObject);
			transform.SetParent(other.transform);
			//transform.localPosition = Vector3.zero;
			//other.transform.SetParent(this.transform);
			//other.transform.localPosition = Vector3.zero;
			//rb.linearVelocity = Vector3.zero; 
		}
		else if(other.CompareTag("Dart"))
		{
			gameObject.SetActive(false);
		}
	}

	void FixedUpdate()
	{
		rb.AddForce(gravity * 0.5f, ForceMode.Acceleration);
	}

	void ReverseGravity() => gravity *= -1;
}
