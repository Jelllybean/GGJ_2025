using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
    [HideInInspector]
    public List<GameObject> smallBubbles = new List<GameObject>();

    [SerializeField] private GameObject bigBubble;
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
            bigBubble.SetActive(true);
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
