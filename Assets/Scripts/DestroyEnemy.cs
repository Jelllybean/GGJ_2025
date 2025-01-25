using UnityEngine;
using RayFire;

public class DestroyEnemy : MonoBehaviour
{
	public RayfireRigid rayfireRigid;
	public RayfireBomb bomb;

	void Start()
	{
		rayfireRigid.DemolishForced();
		bomb.transform.SetParent(null);
		bomb.transform.position = rayfireRigid.fragments[0].transform.parent.position;
		rayfireRigid.fragments[0].transform.parent.SetParent(transform);
		this.enabled = true;
		bomb.enabled = true;
	}

	public void ExplodeEnemy()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		bomb.Explode(0);
		Invoke("Reset", 1.5f);
	}

	public void Reset()
	{
		bomb.Restore();
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
