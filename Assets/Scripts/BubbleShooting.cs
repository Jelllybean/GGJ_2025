using System.Collections.Generic;
using UnityEngine;

public class BubbleShooting : MonoBehaviour
{
	private Camera camera;
	[SerializeField] private float shootingForce = 100f;
	public GameObject bubblePrefab;

	public Transform rightHandPosition;

	public List<Rigidbody> bubbles;


	private int bubblesIndex = 0;
	void Start()
	{
		camera = Camera.main;
		for (int i = 0; i < 200; i++)
		{
			GameObject _obj = Instantiate(bubblePrefab);
			bubbles.Add(_obj.GetComponent<Rigidbody>());
			_obj.SetActive(false);
		}
	}

	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 10000f))
		{
			Debug.DrawRay(camera.transform.position, camera.transform.forward * 10000f, Color.yellow);
		}

		if (Input.GetMouseButtonDown(0))
		{
			bubbles[bubblesIndex].gameObject.SetActive(true);
			bubbles[bubblesIndex].transform.SetParent(null);
			bubbles[bubblesIndex].transform.position = rightHandPosition.position;
			bubbles[bubblesIndex].AddForce(shootingForce * camera.transform.forward);
			if (bubblesIndex >= bubbles.Count - 1)
			{
				bubblesIndex = 0;
			}
			else
			{
				bubblesIndex++;
			}
			//_rb.AddForce(100 * transform.up);
		}


	}


}
