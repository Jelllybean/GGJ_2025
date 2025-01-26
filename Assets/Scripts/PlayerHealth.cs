using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public RectTransform mask;
    public float health;

    void Update()
    {
        mask.localScale = Vector2.Lerp(new Vector2(1.2546f, 1.456591f), new Vector2(2.487621f, 2.888128f), health / 100f);
    }

    public void RemoveHealth(float _healthToRemove)
    {
        health -= _healthToRemove;
        StartCoroutine("AddHealthBack");
    }

    public IEnumerator AddHealthBack()
    {
        yield return new WaitForSeconds(5);

        float _currentHealth = health;
		float _elapsedTime = 0;
		float _waitTime = 1.5f;

		while (health < 100)
		{
            health++;
			yield return null;
		}

        health = 100;
	}
}
