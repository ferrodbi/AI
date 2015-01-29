using UnityEngine;
using System.Collections;

public class discreteMove : MonoBehaviour {
	// Use this for initialization
	public GameObject goalObj;
	private Vector3 finish;
	void Start () {
		finish = goalObj.transform.position;
	}

	void Update() {
		if (transform.position != finish) {
			Vector3 tmp = finish-transform.position;
			if(tmp.x != 0) {
				moveDiscrete(new Vector3(1, 0, 0) * (Mathf.Sign(tmp.x)));
			}
			if(tmp.z != 0) {
				moveDiscrete(new Vector3(0, 0, 1) * (Mathf.Sign(tmp.z)));
			}
		} else {
			print ("Goal reached");
		}
	}
	
	// Update is called once per frame

	public void moveDiscrete(Vector3 dir) {
			transform.localPosition += dir;
	}
}
