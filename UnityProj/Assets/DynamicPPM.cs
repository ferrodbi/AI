using UnityEngine;
using System.Collections;

public class DynamicPPM : MonoBehaviour {

	public GameObject goalObj;
	public float v = 1.0f;
	public float maxF = 1.0f;
	public float mass = 1.0f;
	private Vector3 finish;
	private Vector3 internalVelocity = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
		finish = goalObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		internalVelocity += (finish - transform.position).normalized * (maxF / mass) * Time.deltaTime;
		transform.localPosition += internalVelocity;
	}
}
