using UnityEngine;
using System.Collections;

public class HumanBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Mouse rotation
		this.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
		this.transform.Find("HumanCamera").Rotate(new Vector3(Input.GetAxis("Mouse Y")*-1, 0, 0));

		//Movement
		if (Input.GetKey (KeyCode.W)) {
			this.transform.Translate (Vector3.forward * Time.deltaTime * 4);
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.Translate (Vector3.back * Time.deltaTime * 4);
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.Translate (Vector3.left * Time.deltaTime * 2);
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.Translate (Vector3.right * Time.deltaTime * 2);
		}
	}
}
