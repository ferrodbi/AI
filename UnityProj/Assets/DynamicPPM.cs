using UnityEngine;
using System.Collections.Generic;

public class DynamicPPM : MonoBehaviour {

	public GameObject goalObj;
	public GameObject intermediate;
	public float v = 1.0f;
	public float maxF = 1f;
	public float mass = 1.0f;
	private Vector3 finish;
	private Vector3 inter;
	private Vector3 internalVelocity = new Vector3(0,0,0);
	private Vector3 fvec = new Vector3(0,0,0);
	private bool touchedIntermediate = false;
	// Use this for initialization
	void Start () {
		finish = goalObj.transform.position;
		inter = intermediate.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!touchedIntermediate) {
			if(Vector3.Distance(transform.position, inter) < 0.1f) {
				touchedIntermediate = true;
				print ("TOUCHED");
			}
			fvec = ((inter - transform.position).normalized - internalVelocity).normalized;
			internalVelocity += fvec *(maxF / mass) * Time.deltaTime;
		}
		if(touchedIntermediate) {
			fvec = ((finish - transform.position).normalized - internalVelocity).normalized;
			internalVelocity += fvec *(maxF / mass) * Time.deltaTime;
		}
		transform.localPosition += internalVelocity;
	}
}
