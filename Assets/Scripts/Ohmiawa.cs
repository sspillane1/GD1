using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohmiawa : MonoBehaviour {
    public float duration = 3f;
    private Transform sprite;
    void Start()
    {
        sprite = GetComponent<Transform>();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        // 3 seconds you can change this 
        //to whatever you want
        yield return new WaitForSeconds(duration);
        float rotAngle = Mathf.Floor(Random.Range(0,2))*90;
        sprite.Rotate(Vector3.forward * rotAngle);
        StartCoroutine(Countdown());
    }
}
