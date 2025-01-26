using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    public Camera animCam;
    public Camera realCam;
    public float length;

    public string clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        realCam.enabled = false;
        animCam.enabled = true;
        
        if(animCam.GetComponent<Animator>() != null)
        {
            animCam.GetComponent<Animator>().Play(clip);
        }

        StartCoroutine(SwitchCamera());
    }


    public IEnumerator SwitchCamera()
    {
        yield return new WaitForSeconds(length);
        realCam.enabled = true;
        animCam.enabled = false;
        
    }

}
