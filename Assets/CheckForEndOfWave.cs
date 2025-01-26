using UnityEngine;

public class CheckForEndOfWave : MonoBehaviour
{
    public GameObject popupMenu;

	private void Update()
	{
		if(EnemySpawner.totalEnemies <= Enemy.enemiesKilled)
		{
			popupMenu.SetActive(true);
		}
	}
}
