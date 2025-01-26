using UnityEngine;
using TMPro;
using RayFireEditor;
using System;

public class Timer : MonoBehaviour
{
	public TextMeshProUGUI timerText;

	public float timerTime;

	void Update()
	{
		timerTime = Time.time;
		int seconds = ((int)timerTime % 60);
		int minutes = ((int)timerTime / 60);
		timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
