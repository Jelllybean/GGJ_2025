using UnityEngine;
using UnityEngine.Serialization;

public class RotateBunny : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    
    [FormerlySerializedAs("rotationSpeed")] public float rotationMult = 200;
    void Update()
    {
        transform.Rotate(0, 0, rotationMult * Time.deltaTime * enemy.speedMultiplier);
    }
}
