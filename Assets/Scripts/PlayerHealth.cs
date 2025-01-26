using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public RectTransform mask;
    public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mask.localScale = Vector2.Lerp(new Vector2(1.2546f, 1.456591f), new Vector2(2.487621f, 2.888128f), health / 100f);

    }
}
