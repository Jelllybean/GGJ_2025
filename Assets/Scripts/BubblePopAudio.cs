using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BubblePopAudio : MonoBehaviour
{
    public static BubblePopAudio instance;

    public List<AudioSource> bubblePopAudio = new List<AudioSource>();
	private int previousIndex;

	private void Awake()
	{
		if (BubblePopAudio.instance)
		{
			Destroy(gameObject);
		}
		{
			instance = this;
		}
	}

	public void PlayBubbleAudio()
	{
		bubblePopAudio[SoundIndex()].Play();
	}

	private int SoundIndex()
	{
		int _index = Random.Range(0, bubblePopAudio.Count - 1);
		if (_index != previousIndex)
		{
			previousIndex = _index;
			return _index;
		}
		else
		{
			return previousIndex % bubblePopAudio.Count;
		}
	}
}
