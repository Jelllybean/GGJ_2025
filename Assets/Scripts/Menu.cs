using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public TextMeshProUGUI timerText;
	void Start()
	{
		Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
		Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);

		if(timerText)
		{
			SetTimer();
		}
	}

	public void StartGame(int _levelIndex)
	{
		SceneManager.LoadScene(_levelIndex);
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void SetResolution(TMP_Dropdown _dropdown)
	{
		Debug.Log(_dropdown.value);
		switch (_dropdown.value)
		{
			case 0:
				Screen.SetResolution(640, 480, true);
				break;
			case 1:
				Screen.SetResolution(1280, 720, true);
				break;
			case 2:
				Screen.SetResolution(1920, 1080, true);
				break;
			case 3:
				Screen.SetResolution(2560, 1440, true);
				break;
			case 4:
				Screen.SetResolution(3840, 2160, true);
				break;

		}
	}

	public void SetBorderless(Toggle _toggle)
	{
		if (_toggle.isOn)
		{
			Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
		}
		else
		{
			Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
		}

	}

	public void SetTimer()
	{
		timerText.text = FindFirstObjectByType<Timer>().timerText.text;
	}
}
