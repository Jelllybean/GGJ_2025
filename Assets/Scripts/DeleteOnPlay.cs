using System;
using UnityEngine;

public class DeleteOnPlay : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject);
    }
}
