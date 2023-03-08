using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = .5f;

    Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapseTime = 0;
        while(elapseTime < shakeDuration)
        {
            transform.position = initPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapseTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initPos;
        
    }

    
}
