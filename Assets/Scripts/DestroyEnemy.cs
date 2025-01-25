using UnityEngine;
using RayFire;

public class DestroyEnemy : MonoBehaviour
{
	public RayfireRigid rayfireRigid;
	public RayfireBomb bomb;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		rayfireRigid.DemolishForced();
		bomb.transform.SetParent(null);
		bomb.transform.position = rayfireRigid.fragments[0].transform.parent.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			ExplodeEnemy();
		}
	}

	public void ExplodeEnemy()
	{
		rayfireRigid.Activate();
		bomb.Explode(0);
	}
}
