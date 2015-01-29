using UnityEngine;
using System;
using System.Collections.Generic;

public class discreteD : MonoBehaviour {
	private bool[,] obstacles;
	public int neighborhoodSize;
	private int nsize;
	private Node currPos;
	private Node goal;
	private List<Node> path;
	private float timeCheck = 0f;
	public float skip = 1.0f;
	private Node start = new Node(1,1);
	void Start() {
		path = Astar(currPos, goal);
		transform.position = new Vector3((start.x - nsize/2) * 5, 0, (start.y - nsize/2) * 5);
	}
	
	void Update() {
		timeCheck += Time.deltaTime;
		if(timeCheck > skip) {
			timeCheck = 0f;
			transform.position = new Vector3((path[0].x - nsize/2) * 5, 0, (path[0].y - nsize/2) * 5);
			path.RemoveAt(0);
		}
	}
	
	public class Node {
		public int x = -1;
		public int y = -1;
		public Node(int i, int j) {
			x = i;
			y = j;
		}
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
		g_score[start.x, start.y] = 0.0f;
		f_score[start.x, start.y] = hCost(start, goal);
		
		float tmp = 9999999;
		int index = 0;
		int currentx = 0;
		int currenty = 0;
		while(openset.Count != 0) {
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
				List<Node> ret = new List<Node>();
				ret.Add(current);
				while(current.x != -1) {
					current = came_from[current.x, current.y];
					ret.Add(current);
				}
				ret.Reverse();
				return ret;
			}
			
			openset.RemoveAt(index);
			opensetCheck[currentx, currenty] = false;
			closedset[currentx, currenty] = true;
			List<Node> neighbors = getNeighbors(current);
			foreach(Node n in neighbors) {
				if(!closedset[n.x, n.y]) {
					break;
				}
				float tentativeGScore = g_score[current.x, current.y] + dist(current, n);
				if(!opensetCheck[n.x, n.y] || tentativeGScore < g_score[n.x, n.y]) {
					came_from[n.x, n.y] = current;
					g_score[n.x, n.y] = tentativeGScore;
					f_score[n.x, n.y] = g_score[n.x, n.y] + hCost(n, goal);
					if(opensetCheck[n.x, n.y]) {
						openset.Add(n);
						opensetCheck[n.x, n.y] = true;
					}
				}
			}
		}
		return new List<Node>();
	}
	
	List <Node> getNeighbors (Node n) {
		List <Node> l = new List <Node>();
		if (neighborhoodSize >= 4) {
			if (n.x>0 && !obstacles[n.x-1,n.y]) l.Add(new Node(n.x-1, n.y));
			if (n.y>0 && !obstacles[n.x,n.y-1]) l.Add(new Node(n.x, n.y-1));
			if (n.x<nsize && !obstacles[n.x+1,n.y])  l.Add(new Node(n.x+1, n.y));
			if (n.y<nsize && !obstacles[n.x,n.y+1])  l.Add(new Node(n.x, n.y+1));   
		}
		if (neighborhoodSize >= 8) {
			if (n.x>0 && n.y>0 && !obstacles[n.x-1,n.y-1]) l.Add(new Node(n.x-1, n.y-1));
			if (n.x>0 && n.y<nsize && !obstacles[n.x-1,n.y+1]) l.Add(new Node(n.x-1, n.y+1));
			if (n.x<nsize && n.y>0 &&!obstacles[n.x+1,n.y-1])  l.Add(new Node(n.x+1, n.y-1));
			if (n.x<nsize && n.y<nsize && !obstacles[n.x+1,n.y+1])  l.Add(new Node(n.x+1, n.y+1));  
		}
		return l;
	}
	
	float hCost(Node start,Node goal){
		float a = (float) Mathf.Abs (goal.x - start.x);
		float b = (float) Mathf.Abs (goal.y - start.y);
		return (float) Math.Sqrt (a * a + b * b);
	}
	float dist(Node goal, Node start) {
		float x = (float) Mathf.Abs (goal.x - start.x);
		float y = (float) Mathf.Abs (goal.y - start.y);
		return (float) Math.Sqrt (x + y);
	}

	//map.txt to matrix

}