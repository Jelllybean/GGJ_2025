using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public float rotateAmount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, rotateAmount * Time.deltaTime, 0, Space.World);
    }
}
