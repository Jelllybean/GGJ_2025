using UnityEngine;

public class RotateBunny : MonoBehaviour
{
    public float rotationSpeed = 200;
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
