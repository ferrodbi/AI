using UnityEngine;
using System.Collections;
using System.IO;

public class CreateObstaclesCourse : MonoBehaviour {
	public string filename="discMap.txt";
	private bool[,] obstacles;
	private Node start;
	private Node goal;
	private int nsize;
	public GameObject mover;
	public GameObject goel;
	public GameObject wall;

	public class Node {
		public int x = -1;
		public int y = -1;
		public Node(int i, int j) {
			x = i;
			y = j;
		}
	}       
	void Start() {
		StreamReader sr = new StreamReader("C:/Users/Fernando/Documents/Assignments/discMap.txt");
		//StreamReader sr = new StreamReader(Application.dataPath + "/" + filename);
		string tmp = sr.ReadLine();
		string[] res = tmp.Split(',');
		start = new Node(int.Parse(res[0]), int.Parse(res[1]));
		tmp = sr.ReadLine();
		res = tmp.Split(',');
		goal = new Node(int.Parse(res[0]), int.Parse(res[1]));
		string line;
		int y = 0;
		obstacles = new bool[20, 20];
		while((line = sr.ReadLine()) != null) {
			string[] t = line.Split('\t');
			for(int i = 0; i < t.Length; i++) {
				obstacles[i,y] = (t[i] == "1");
			}
			y++;
		}
		sr.Close();
		
		for(int i = 0; i < nsize; i++) {
			for(int j = 0; j < nsize; j++) {
				if(obstacles[i,j]) {
					Instantiate(wall, new Vector3((i - nsize/2)*5, 0, (j - nsize/2)*5), Quaternion.identity);
				}
			}
		}
		Instantiate(mover, new Vector3((start.x - nsize/2)*5, 0, (start.y - nsize/2)*5), Quaternion.identity);
		Instantiate(goel, new Vector3((goal.x - nsize/2)*5, 0, (goal.y - nsize/2)*5), Quaternion.identity);
	}
}
