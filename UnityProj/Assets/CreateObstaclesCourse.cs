using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CreateObstaclesCourse : MonoBehaviour {
	public string filename="discMap.txt.txt";
	private bool[,] obstacles;
	private Node start;
	private Node goal;
	private int nsize = 20;
	public GameObject mover;
	public GameObject goel;
	public GameObject wall;
	public GameObject pathball;
	private GameObject mv;
	private GameObject gl;
	private float timeCheck = 0f;
	private List<Node> path;
	public float skip = 1.0f;
	public int neighborhoodSize = 6;

	void Start() {
		//StreamReader sr = new StreamReader("C:/Users/Johan/Downloads/AI-master/AI-master/UnityProj/Assets/discMap.txt.txt");
		StreamReader sr = new StreamReader(Application.dataPath + "/" + filename);
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
					Instantiate(wall, new Vector3((i - nsize/2)*1.5f, 0, (j - nsize/2)*1.5f), Quaternion.identity);
				}
			}
		}
		mv = Instantiate(mover, new Vector3((start.x - nsize/2)*1.5f, 0, (start.y - nsize/2)*1.5f), Quaternion.identity) as GameObject;
		gl = Instantiate(goel, new Vector3((goal.x - nsize/2)*1.5f, 0, (goal.y - nsize/2)*1.5f), Quaternion.identity) as GameObject;
		path = Astar (start, goal);
		foreach (Node n in path) {
			if(n != null) Instantiate(pathball, new Vector3((n.x - nsize/2)*1.5f, 0, (n.y - nsize/2)*1.5f), Quaternion.identity);
		}
	}

	void Update() {
		timeCheck += Time.deltaTime;
		if(path.Count > 0 && timeCheck > skip) {
			timeCheck = 0f;
			mv.transform.position = new Vector3((path[0].x - nsize/2) * 1.5f, 0, (path[0].y - nsize/2) * 1.5f);
			path.RemoveAt(0);
		}
	}
	
	public void revealBoard(Node s, Node g, bool[,] obst) {
		obstacles = obst;
		start = s;
		goal = g;
	}       
	
	List<Node> Astar(Node start, Node goal) {
		List<Node> openset = new List<Node>();
		bool[,] opensetCheck = new bool[nsize, nsize];
		bool[,] closedset = new bool[nsize, nsize];
		Node[,] came_from = new Node[nsize,nsize];
		float[,] g_score = new float[nsize, nsize];
		float[,] f_score = new float[nsize, nsize];
		
		openset.Add(start);
		opensetCheck[start.x, start.y] = true;
		for(int i = 0; i < nsize; i++) {
			for(int j = 0; j < nsize; j++) {
				g_score[i, j] = 9999999f;
			}
		}
		g_score[start.x, start.y] = 0.0f;
		f_score[start.x, start.y] = hCost(start, goal);

		print (openset.Count);
		while(openset.Count != 0) {
			float tmp = 9999999;
			int index = 0;
			int currentx = 0;
			int currenty = 0;
			for(int s = 0; s < openset.Count; s++) {
				Node n = openset[s];
				if(f_score[n.x, n.y] < tmp) {
					tmp = f_score[n.x, n.y];
					currentx = n.x;
					currenty = n.y;
					index = s;
				}
			}
			Node current = new Node(currentx, currenty);
			if(current.x == goal.x && current.y == goal.y) {
				print ("WIN!");
				print (g_score[2,2]);
				print ("g_score: " + g_score[current.x, current.y]);
				List<Node> ret = new List<Node>();
				while(current != null) {
					ret.Add(current);
					current = came_from[current.x, current.y];
				}
				ret.Reverse();
				print (ret.Count);
				return ret;
			}
			
			openset.RemoveAt(index);
			opensetCheck[currentx, currenty] = false;
			closedset[currentx, currenty] = true;
			List<Node> neighbors = getNeighbors(current);
			foreach(Node n in neighbors) {
				if(!opensetCheck[n.x, n.y] && !closedset[n.x, n.y]) {
					float tentativeGScore = g_score[current.x, current.y] + dist(current, n);
					if(tentativeGScore < g_score[n.x, n.y]) {
						came_from[n.x, n.y] = current;
						g_score[n.x, n.y] = tentativeGScore;
						f_score[n.x, n.y] = g_score[n.x, n.y] + 0;//hCost(n, goal);
						openset.Add(n);
						opensetCheck[n.x, n.y] = true;
					}
				}
			}
		}
		print ("fail");
		return new List<Node>();
	}
	
	List <Node> getNeighbors (Node n) {
		List <Node> l = new List <Node>();
		if (neighborhoodSize >= 4) {
			if (n.x>0 && !obstacles[n.x-1,n.y]) l.Add(new Node(n.x-1, n.y));
			if (n.y>0 && !obstacles[n.x,n.y-1]) l.Add(new Node(n.x, n.y-1));
			if (n.x<nsize-1 && !obstacles[n.x+1,n.y])  l.Add(new Node(n.x+1, n.y));
			if (n.y<nsize-1 && !obstacles[n.x,n.y+1])  l.Add(new Node(n.x, n.y+1));   
		}
		if (neighborhoodSize >= 8) {
			if (n.x>0 && n.y>0 && !obstacles[n.x-1,n.y-1]) l.Add(new Node(n.x-1, n.y-1));
			if (n.x>0 && n.y<nsize-1 && !obstacles[n.x-1,n.y+1]) l.Add(new Node(n.x-1, n.y+1));
			if (n.x<nsize-1 && n.y>0 &&!obstacles[n.x+1,n.y-1])  l.Add(new Node(n.x+1, n.y-1));
			if (n.x<nsize-1 && n.y<nsize-1 && !obstacles[n.x+1,n.y+1])  l.Add(new Node(n.x+1, n.y+1));  
		}
		if (neighborhoodSize >= 12) { // we must use 16 instead
			if (n.x>1 && !obstacles[n.x-2,n.y] && !obstacles[n.x-1,n.y]) l.Add(new Node(n.x-2, n.y));
			if (n.y>1 && !obstacles[n.x,n.y-2] && !obstacles[n.x,n.y-1]) l.Add(new Node(n.x, n.y-2));
			if (n.x<nsize-2 && !obstacles[n.x+2,n.y] && !obstacles[n.x+1,n.y])  l.Add(new Node(n.x+2, n.y));
			if (n.y<nsize-2 && !obstacles[n.x,n.y+2] && !obstacles[n.x,n.y+1])  l.Add(new Node(n.x, n.y+2));  
		}
		return l;
	}
	
	float hCost(Node start,Node goal){
		float a = (float) Mathf.Abs (goal.x - start.x);
		float b = (float) Mathf.Abs (goal.y - start.y);
		return (float) Mathf.Sqrt ((a * a) + (b * b));
	}
	float dist(Node a, Node b) {
		float x = (float) Mathf.Abs (a.x - b.x);
		float y = (float) Mathf.Abs (a.y - b.y);
		return (float) Mathf.Sqrt ((x *x) + (y*y)); // there was not sqrt before
	}
}
