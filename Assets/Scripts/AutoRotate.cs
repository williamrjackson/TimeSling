using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
    public float degreesPerSecond = 45;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles += Vector3.back * degreesPerSecond * Time.deltaTime;
    }
}
