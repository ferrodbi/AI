using UnityEngine;
using System.Collections;

public class DifferentialDrive : MonoBehaviour {

	public float v;
	public float w;
	public GameObject goalObj;
	private Vector3 finish;
	private float g = 0.0f;
	// Use this for initialization
	void Start () {
		finish = goalObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpV = (finish - transform.position).normalized;
		float tmpA = Vector3.Angle (tmpV, Vector3.forward);
		//g += tmpA * Time.deltaTime;
		transform.Translate (Vector3.forward * Time.deltaTime * v * Mathf.Cos(g));
		transform.Translate (Vector3.right * Time.deltaTime * v * Mathf.Sin (g));
		//transform.Rotate (Vector3.up, tmpA * Time.deltaTime);
		print (tmpA);
	}
}
