using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CreatePolyMap : MonoBehaviour {
	public string filename = "polymap.txt";
	public int numbers = 23;
	private NodeFloat start;
	private NodeFloat goal;
	private List<NodeFloat> nodes; 
	private int[,] edges = new int[23,2];
	public GameObject ball;
	// Use this for initialization
	void Start () {
		StreamReader sr = new StreamReader(Application.dataPath + "/" + filename);
		string tmp = sr.ReadLine();
		string[] res = tmp.Split(',');
		start = new NodeFloat(float.Parse(res[0]), float.Parse(res[1]));
		tmp = sr.ReadLine();
		res = tmp.Split(',');
		goal = new NodeFloat(float.Parse(res[0]), float.Parse(res[1]));
		string line;
		nodes = new List<NodeFloat>();
		int firstEdge = 0;
		int y = 0;
		while((line = sr.ReadLine()) != null) {
			string[] t = line.Split('\t');
			Instantiate(ball, new Vector3(float.Parse (t[0]), 0, float.Parse (t[1])), Quaternion.identity);
			nodes.Add(new NodeFloat(float.Parse(t[0]), float.Parse(t[1])));
			edges[y, 0] = y;
			if((t[2] == "1")) {
				edges[y,1] = y+1;
			}
			else {
				edges[y,1] = firstEdge;
				firstEdge = y+1;
			}
			y++;
		}
		sr.Close();
	}
	
	// Update is called once per frame
}
