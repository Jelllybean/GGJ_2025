using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DartShooting : MonoBehaviour
{
	private Camera camera;
	[SerializeField] private float minShootForce, maxShootForce, maxChargeTime;
	[SerializeField] private float fovIncrease;

	public GameObject dartPrefab;
	public Transform leftHandPosition;
	public List<Rigidbody> darts;
	private int currentDart = 0;

	private bool isChargingShot = false;
	private float chargeStartTime;
	private float cameraDefaultFov;

	void Start()
    {
		camera = Camera.main;
		for (int i = 0; i < 30; i++)
		{
			GameObject _obj2 = Instantiate(dartPrefab);
			darts.Add(_obj2.GetComponent<Rigidbody>());
			_obj2.SetActive(false);
		}

		cameraDefaultFov = camera.fieldOfView;
    }

    void Update()
    {
	    // start charge
		if (Input.GetMouseButtonDown(1))
		{
			isChargingShot = true;
			chargeStartTime = Time.time;
		}
		
		// dolly zoom effect
		if (isChargingShot)
		{
			float normalizedCharge = 1 - (Mathf.Max(0,(chargeStartTime + maxChargeTime) - Time.time) / maxChargeTime);
			camera.fieldOfView = cameraDefaultFov + fovIncrease * normalizedCharge;
			
			// shoot
			if (Input.GetMouseButtonUp(1))
			{
				Shoot(Mathf.Lerp(minShootForce, maxShootForce, normalizedCharge));
				isChargingShot = false;
			}
		}
		else
		{
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, cameraDefaultFov, Time.deltaTime * 5);
		}
	}

    void Shoot(float force)
    {
	    darts[currentDart].gameObject.SetActive(true);
	    darts[currentDart].transform.position = leftHandPosition.position;
	    darts[currentDart].AddForce(force * camera.transform.forward, ForceMode.VelocityChange);
	    if (currentDart >= darts.Count - 1)
	    {
		    currentDart = 0;
	    }
	    else
	    {
		    currentDart++;
	    }
    }
}
