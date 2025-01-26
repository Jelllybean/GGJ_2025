using UnityEngine;
using TMPro;
using RayFireEditor;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Update()
    {
		var time = Time.time;
		int seconds = ((int)time % 60);
		int minutes = ((int)time / 60);
		timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
