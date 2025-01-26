using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class DartShooting : MonoBehaviour
{
	private CinemachineCamera camera;
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
		camera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineCamera>();
		for (int i = 0; i < 30; i++)
		{
			GameObject _obj2 = Instantiate(dartPrefab);
			darts.Add(_obj2.GetComponent<Rigidbody>());
			_obj2.SetActive(false);
		}

		cameraDefaultFov = camera.Lens.FieldOfView;
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
			camera.Lens.FieldOfView = cameraDefaultFov + fovIncrease * normalizedCharge;
			
			// shoot
			if (Input.GetMouseButtonUp(1))
			{
				Shoot(Mathf.Lerp(minShootForce, maxShootForce, normalizedCharge));
				isChargingShot = false;
			}
		}
		else
		{
			camera.Lens.FieldOfView = Mathf.Lerp(camera.Lens.FieldOfView, cameraDefaultFov, Time.deltaTime * 5);
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
