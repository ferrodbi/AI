using UnityEngine;
using System.Collections;

public class KinematicCarModel : MonoBehaviour {
	public GameObject goalObj;
	public float vMax = 1.0f;
	public float wMax = 1.0f;
	private Vector3 finish;
	public Transform backRot;
	public Transform frontRot;
	private float l;
	// Use this for initialization
	void Start () {
		finish = goalObj.transform.position;
		l = Vector3.Distance (frontRot.localPosition, new Vector3(0,0,0));
		print (l);
		wMax /= 57.296f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpV = (finish - transform.position).normalized;
		float rotVec = Mathf.Sign(Vector3.Dot(backRot.right, tmpV));
		backRot.Translate(Vector3.forward * vMax * Time.deltaTime);
		backRot.Rotate (Vector3.up, rotVec * vMax * 0.2f);
	}
}
