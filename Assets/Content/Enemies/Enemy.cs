using System;
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
    [FormerlySerializedAs("minimumSpeed")] [SerializeField] private float minimumSpeedMult;
    [FormerlySerializedAs("maximumSpeed")] [SerializeField] private float maximumSpeedMult;

    [HideInInspector] public List<GameObject> smallBubbles = new List<GameObject>();

    private int bubbleCount = 0;
    private bool isInBubble = false;
    private float lastStepTime = Mathf.NegativeInfinity;
    private float speedMultiplier = 1;

    private void Start()
    {
        speedMultiplier = maximumSpeedMult;
    }

    protected override void Update()
    {
        base.Update();

        // only move forwards when making a step
        float normalizedStep = (Mathf.Max(0, lastStepTime + stepDecaySeconds - Time.time)) / stepDecaySeconds;
        navAgent.speed = stepSpeedCurve.Evaluate(normalizedStep) * speedMultiplier;
    }

    public void AttachBubble(GameObject _smallBubble)
    {
        bubbleCount++;
        smallBubbles.Add(_smallBubble);
        if(bubbleCount > bubblesUntilBigBubble)
        {
            bigBubble.SetActive(true);
            isInBubble = true;
            for (int i = 0; i < smallBubbles.Count; i++)
            {
                smallBubbles[i].SetActive(false);
            }
            smallBubbles.Clear();
            _smallBubble.SetActive(false);
        }
        
        // reduce speed with each bubble
        speedMultiplier = Mathf.Lerp(maximumSpeedMult, minimumSpeedMult,
            ((float)bubbleCount / (float)bubblesUntilBigBubble));
    }

    public void OnDartHit()
    {
        if (isInBubble)
        {
            Destroy(gameObject);
        }
    }

    public void MovementStep()
    {
        lastStepTime = Time.time;
    }

    //public IEnumerator BigBubbleCountDown()
    //{
    //	yield return new WaitForSeconds(5f);
    //
    //}
}
