using UnityEngine;
using System.Collections;

public class rotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(1,0,0), 1);
	}
}
