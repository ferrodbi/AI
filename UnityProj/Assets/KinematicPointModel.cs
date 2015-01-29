using UnityEngine;
using System.Collections;

public class KinematicPointModel : MonoBehaviour {

	// Use this for initialization
	public GameObject goalObj;
	public float v = 1.0f;
	private Vector3 finish;
	void Start () {
		finish = goalObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (finish, transform.position) > 0.5f) {
			transform.localPosition += (finish - transform.position).normalized * v * Time.deltaTime;
		}
	}
}
