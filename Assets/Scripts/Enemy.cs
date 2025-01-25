using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : Agent
{
	[Header("Navigation")]
	[SerializeField] private NavMeshAgent navAgent;
	[SerializeField] private float stepDecaySeconds;
	[SerializeField] private AnimationCurve stepSpeedCurve;

	[Header("Bubble mechanics")]
	[SerializeField] private GameObject bigBubble;
	[SerializeField] private int bubblesUntilBigBubble;
	[FormerlySerializedAs("minimumSpeed")][SerializeField] private float minimumSpeedMult;
	[FormerlySerializedAs("maximumSpeed")][SerializeField] private float maximumSpeedMult;

	[Header("Visuals")]
	[SerializeField] private Animator animator;

	[HideInInspector] public List<GameObject> smallBubbles = new List<GameObject>();

	private int bubbleCount = 0;
	private bool isInBubble = false;
	private float lastStepTime = Mathf.NegativeInfinity;
	private float speedMultiplier = 1;

	public DestroyEnemy destroyEnemy;
	public EnemyType enemyType;

	private void Start()
	{
		speedMultiplier = maximumSpeedMult;

		destroyEnemy = DestroyEnemy.destroyedEnemies[enemyType];
	}

	protected override void Update()
	{
		if (!isInBubble)
		{
			base.Update();

			animator.SetBool("IsOnOffMeshLink", navAgent.isOnOffMeshLink);

			if (navAgent.isOnOffMeshLink)
			{
				navAgent.acceleration = 0.01f;
				navAgent.speed = 6;
			}
			else
			{
				// only move forwards when making a step
				navAgent.acceleration = 100000;
				float normalizedStep = (Mathf.Max(0, lastStepTime + stepDecaySeconds - Time.time)) / stepDecaySeconds;
				navAgent.speed = stepSpeedCurve.Evaluate(normalizedStep) * speedMultiplier;
			}
		}
	}

	public void AttachBubble(GameObject _smallBubble)
	{
		bubbleCount++;
		smallBubbles.Add(_smallBubble);
		if (bubbleCount > bubblesUntilBigBubble)
		{
			bigBubble.SetActive(true);
			isInBubble = true;
			for (int i = 0; i < smallBubbles.Count; i++)
			{
				smallBubbles[i].SetActive(false);
				smallBubbles[i].transform.SetParent(null);
			}
			smallBubbles.Clear();
			_smallBubble.SetActive(false);
			StartCoroutine("FloatAboveGround");
		}

		// reduce speed with each bubble
		speedMultiplier = Mathf.Lerp(maximumSpeedMult, minimumSpeedMult,
			((float)bubbleCount / (float)bubblesUntilBigBubble));
	}

	public void OnDartHit()
	{
		if (isInBubble)
		{
			destroyEnemy?.ExplodeEnemy(transform);
			Destroy(gameObject);
		}
	}

	public void MovementStep()
	{
		lastStepTime = Time.time;
	}

	public IEnumerator FloatAboveGround()
	{
		navAgent.enabled = false;
		Vector3 _endPosition = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
		float _elapsedTime = 0;
		float _waitTime = 1f;
		Vector3 _currentPos = transform.position;

		while (_elapsedTime < _waitTime)
		{
			transform.position = Vector3.Lerp(_currentPos, _endPosition, (_elapsedTime / _waitTime));
			_elapsedTime += Time.deltaTime;
			yield return null;
		}

		_elapsedTime = 0;
		_waitTime = 15f;
		_currentPos = transform.position;
		//yield return new WaitForSeconds(0.5f);
		//Debug.Break();
		while (_elapsedTime < _waitTime)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2) * 0.01f, transform.position.z);
			_elapsedTime += Time.deltaTime;
			yield return null;
		}
		navAgent.enabled = true;
		isInBubble = false;
		bigBubble.SetActive(false);
		speedMultiplier = maximumSpeedMult;
		yield return null;
	}

	//public IEnumerator BigBubbleCountDown()
	//{
	//	yield return new WaitForSeconds(5f);
	//
	//}
}
