using UnityEngine;
using System.Collections;

public class DifferentialDrive : MonoBehaviour {

	public float v;
	public float w;
	public GameObject goalObj;
	public GameObject intermediate;
	private Vector3 finish;
	// Use this for initialization
	void Start () {
		finish = goalObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpV = (finish - transform.position).normalized;
		float rotVec = Mathf.Sign(Vector3.Dot(transform.right, tmpV));
		float speed = Mathf.Max (0.0f, Vector3.Dot (transform.forward, tmpV)) * v;
		print (speed);
		transform.Translate (Vector3.forward * Time.deltaTime * speed);

		transform.Rotate (Vector3.up, rotVec*56 * Time.deltaTime);
	}
}
