using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
	private Rigidbody rb;

	private Vector3 gravity;

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
			other.GetComponent<Enemy>().AttachBubble(gameObject);
			transform.SetParent(other.transform);
			BubblePopAudio.instance?.PlayBubbleAudio();
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
