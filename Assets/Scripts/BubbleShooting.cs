using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class BubbleShooting : MonoBehaviour
{
	public Camera camera;
	[SerializeField] private float shootingForce = 4000;
	public GameObject bubblePrefab;

	public Transform rightHandPosition;

	public List<Rigidbody> bubbles;


	private int bubblesIndex = 0;

	[Header("Reloading")]
	public int currentAmmoCount = 6;
	public bool canShoot = true;
	public TextMeshProUGUI ammoCounter;
	public string infinitySymbol;
	public Image numberImage;
	public List<Sprite> numberSprites;
	public Slider slider;
	public Animator animator;

	[Header("Audio")]
	public List<AudioSource> wooshEffects;
	private int previousIndex;

	void Start()
	{
		//camera = Camera.main;
		for (int i = 0; i < 200; i++)
		{
			GameObject _obj = Instantiate(bubblePrefab);
			bubbles.Add(_obj.GetComponent<Rigidbody>());
			_obj.SetActive(false);
		}
		numberImage.sprite = numberSprites[12];
		slider.value = 0;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && canShoot)
		{
			if (bubbles[bubblesIndex])
			{
				bubbles[bubblesIndex].gameObject.SetActive(true);
				bubbles[bubblesIndex].transform.SetParent(null);
				bubbles[bubblesIndex].transform.position = rightHandPosition.position;
				bubbles[bubblesIndex].AddForce(shootingForce * camera.transform.forward);
			}
			if (bubblesIndex >= bubbles.Count - 1)
			{
				bubblesIndex = 0;
			}
			else
			{
				bubblesIndex++;
			}
			currentAmmoCount--;
			numberImage.sprite = numberSprites[currentAmmoCount];
			int _index = SoundIndex();
			wooshEffects[_index].Play();
			//ammoCounter.text = currentAmmoCount.ToString() + infinitySymbol;
			if (currentAmmoCount <= 0)
			{
				canShoot = false;
				StartCoroutine("Reload");
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			canShoot = false;
			StartCoroutine("Reload");
		}
	}

	public void CantShoot()
	{
		canShoot = true;
		currentAmmoCount = 12;
		//ammoCounter.text = currentAmmoCount.ToString() + infinitySymbol;
	}

	private int SoundIndex()
	{
		int _index = Random.Range(0, wooshEffects.Count - 1);
		if (_index != previousIndex)
		{
			previousIndex = _index;
			return _index;
		}
		else
		{
			return previousIndex % wooshEffects.Count;
		}
	}

	public IEnumerator Reload()
	{
		animator.SetBool("isReloading", true);
		slider.gameObject.SetActive(true);
		slider.value = 0;
		float _elapsedTime = 0;
		float _waitTime = 1.5f;

		while (_elapsedTime < _waitTime)
		{
			slider.value = Mathf.Lerp(0, _waitTime, (_elapsedTime / _waitTime));
			_elapsedTime += Time.deltaTime;
			yield return null;
		}
		slider.value = 1.5f;
		slider.gameObject.SetActive(false);
		canShoot = true;
		currentAmmoCount = 6;
		numberImage.sprite = numberSprites[12];
		animator.SetBool("isReloading", false);
		//ammoCounter.text = currentAmmoCount.ToString() + infinitySymbol;
		yield return null;
	}

}
