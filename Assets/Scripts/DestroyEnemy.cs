using UnityEngine;
using RayFire;
using System.Collections.Generic;

public enum EnemyType { Goop, Bunny }
public class DestroyEnemy : MonoBehaviour
{
	public RayfireRigid rayfireRigid;
	public RayfireBomb bomb;

	public EnemyType enemyType;

	public static Dictionary<EnemyType, DestroyEnemy> destroyedEnemies = new Dictionary<EnemyType, DestroyEnemy>();

	private Vector3 startPosition;

	private void Awake()
	{
		destroyedEnemies.Add(enemyType, this);
	}
	void Start()
	{
		rayfireRigid.DemolishForced();
		transform.SetParent(null);
		rayfireRigid.fragments[0].transform.parent.position = transform.position;
		rayfireRigid.fragments[0].transform.parent.SetParent(transform);
		this.enabled = true;
		bomb.enabled = true;
		startPosition = transform.position;
		ExplodeEnemy(transform);
	}

	public void ExplodeEnemy(Transform _enemyPosition)
	{
		transform.position = _enemyPosition.position;
		transform.GetChild(0).gameObject.SetActive(true);
		//bomb.Restore();
		//Debug.Break();
		bomb.Explode(0);
		Invoke("Reset", 1.5f);
	}

	public void Reset()
	{
		transform.position = startPosition;
		bomb.Restore();
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
