using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Agent
{
    [SerializeField] private NavMeshAgent navAgent;
    
    [Header("Bubble mechanics")]
    [SerializeField] private GameObject bigBubble;
    [SerializeField] private int bubblesUntilBigBubble;
    [SerializeField] private float minimumSpeed, maximumSpeed;
    
    [HideInInspector] public List<GameObject> smallBubbles = new List<GameObject>();

    private int bubbleCount = 0;

    private void Start()
    {
        navAgent.speed = maximumSpeed;
    }

    public void AttachBubble(GameObject _smallBubble)
    {
        bubbleCount++;
        smallBubbles.Add(_smallBubble);
        if(bubbleCount >= bubblesUntilBigBubble)
        {
            bigBubble.SetActive(true);
            for (int i = 0; i < smallBubbles.Count; i++)
            {
                smallBubbles[i].SetActive(false);
            }
            smallBubbles.Clear();
            _smallBubble.SetActive(false);
        }
        
        // reduce speed with each bubble
        navAgent.speed = Mathf.Lerp(maximumSpeed, minimumSpeed,
            ((float)bubbleCount / (float)bubblesUntilBigBubble));
    }

    //public IEnumerator BigBubbleCountDown()
    //{
    //	yield return new WaitForSeconds(5f);
    //
    //}
}
