using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Make dangerous things red
        Wrj.Utils.MapToCurve.Linear.ChangeColor(transform, Color.red, 0);
	}

}
