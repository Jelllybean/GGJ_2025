using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class BubbleShooting : MonoBehaviour
{
	private Camera camera;
	[SerializeField] private float shootingForce = 100f;
	public GameObject bubblePrefab;

	public Transform rightHandPosition;

	public List<Rigidbody> bubbles;


	private int bubblesIndex = 0;

	[Header("Reloading")]
	public int currentAmmoCount = 6;
	public bool canShoot = true;
	public TextMeshProUGUI ammoCounter;
	public string infinitySymbol;

	[Header("Audio")]
	public List<AudioSource> wooshEffects;
	private int previousIndex;

	void Start()
	{
		camera = Camera.main;
		for (int i = 0; i < 200; i++)
		{
			GameObject _obj = Instantiate(bubblePrefab);
			bubbles.Add(_obj.GetComponent<Rigidbody>());
			_obj.SetActive(false);
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && canShoot)
		{
			bubbles[bubblesIndex].gameObject.SetActive(true);
			bubbles[bubblesIndex].transform.SetParent(null);
			bubbles[bubblesIndex].transform.position = rightHandPosition.position;
			bubbles[bubblesIndex].AddForce(shootingForce * camera.transform.forward);
			if (bubblesIndex >= bubbles.Count - 1)
			{
				bubblesIndex = 0;
			}
			else
			{
				bubblesIndex++;
			}
			currentAmmoCount--;
			int _index = SoundIndex();
			wooshEffects[_index].Play();
			ammoCounter.text = currentAmmoCount.ToString() + infinitySymbol;
			if (currentAmmoCount <= 0)
			{
				canShoot = false;
				Invoke("CantShoot", 1.5f);
			}
		}
	}

	public void CantShoot()
	{
		canShoot = true;
		currentAmmoCount = 6;
		ammoCounter.text = currentAmmoCount.ToString() + infinitySymbol;
	}

	private int SoundIndex()
	{
		int _index = Random.Range(0, wooshEffects.Count - 1);
		if(_index != previousIndex)
		{
			previousIndex = _index;
			return _index;
		}
		else
		{
			return previousIndex%wooshEffects.Count;
		}
	}

}
