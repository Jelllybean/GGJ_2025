using System.Collections.Generic;
using UnityEngine;

public class DartShooting : MonoBehaviour
{
	private Camera camera;
	[SerializeField] private float shootingForce = 100f;

	public GameObject dartPrefab;

	public Transform leftHandPosition;

	private int index = 0;

	public List<Rigidbody> darts;

	void Start()
    {
		camera = Camera.main;
		for (int i = 0; i < 200; i++)
		{
			GameObject _obj2 = Instantiate(dartPrefab);
			darts.Add(_obj2.GetComponent<Rigidbody>());
			_obj2.SetActive(false);
		}
	}

    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
			darts[index].gameObject.SetActive(true);
			darts[index].transform.position = leftHandPosition.position;
			darts[index].AddForce(shootingForce * camera.transform.forward);
			if (index >= darts.Count - 1)
			{
				index = 0;
			}
			else
			{
				index++;
			}
		}
	}
}
