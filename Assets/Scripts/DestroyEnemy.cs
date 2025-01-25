using UnityEngine;
using RayFire;
using System.Collections.Generic;

public enum EnemyType { Goop }
public class DestroyEnemy : MonoBehaviour
{
	public RayfireRigid rayfireRigid;
	public RayfireBomb bomb;

	public EnemyType enemyType;

	public static Dictionary<EnemyType, DestroyEnemy> destroyedEnemies = new Dictionary<EnemyType, DestroyEnemy>();

	private void Awake()
	{
		destroyedEnemies.Add(enemyType, this);
		Debug.Log(destroyedEnemies[enemyType]);
	}
	void Start()
	{
		rayfireRigid.DemolishForced();
		transform.SetParent(null);
		transform.position = rayfireRigid.fragments[0].transform.parent.position;
		rayfireRigid.fragments[0].transform.parent.SetParent(transform);
		this.enabled = true;
		bomb.enabled = true;
	}

	public void ExplodeEnemy(Transform _enemyPosition)
	{
		transform.position = _enemyPosition.position;
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
