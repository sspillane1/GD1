using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public int movementspeed = 100;

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
             if (Input.GetKey (KeyCode.A)) {
         transform.Translate (Vector3.left * movementspeed * Time.deltaTime); 
     }
     if(Input.GetKey (KeyCode.D)) {
         transform.Translate (Vector3. right *   movementspeed * Time.deltaTime);
     }
	}
}
