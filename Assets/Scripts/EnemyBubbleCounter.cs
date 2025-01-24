using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubbleCounter : MonoBehaviour
{
	public List<GameObject> smallBubbles = new List<GameObject>();
	private int bubbleCount = 0;
	void Start()
	{

	}

	void Update()
	{

	}

	public void AttachBubble(GameObject _smallBubble)
	{
		bubbleCount++;
		smallBubbles.Add(_smallBubble);
		if(bubbleCount > 3)
		{
			transform.GetChild(0).gameObject.SetActive(true);
			for (int i = 0; i < smallBubbles.Count; i++)
			{
				smallBubbles[i].SetActive(false);
			}
			smallBubbles.Clear();
			_smallBubble.SetActive(false);
		}
	}

	//public IEnumerator BigBubbleCountDown()
	//{
	//	yield return new WaitForSeconds(5f);
	//
	//}
}
