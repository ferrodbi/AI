using UnityEngine;
using System.Collections;
using System.Linq;

public class NodeTree : MonoBehaviour {
	private float x,y,vx, vy;
	private int costToGoal;
	private List<NodeTree> neighbors; // cost 1
	public float maxspeed = 10f;
	public List<NodeTree> exploredNodes = new List<NodeTree>();
	public List<NodeTree> pendingNodes;
	public int neighborhoodSize = 4;
	public float[][] nodemap;

	public class Node (int i, int j){
		x=i;
		y=j;
}
NodeTree start = new Node(x0, y0);
NodeTree goal = new Node (xf, yf);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
//Tree for kinematic model

public float explore (Node n, int stepcost) {
	if (n.x == goal.x && n.y==goal.y) return stepcost;
	else if (!exploreNodes.Contains(n)){
		exploredNodes.Add(n);
		createNeighbors(n);
		ForEach node in neighbors {
			explore(node,1);
		}
		List<Node>  pending = objListOrder.OderBy(neighbors.cost).ToList();
		//now pending has an ordered list of nodes by its cost to the goal
		Node[] queue = pending.ToArray();
		return stepcost + explore[0];
	} 
	return 9999f;
}




public void createNeighbors (Node n) {
	if (neighborhoodSize >= 4) {
		if (n.x>0 && !obstacles[n.x-1,n.y]) neighbors.Add(new Node(n.x-1, n.y));
		if (n.y>0 && !obstacles[n.x,n.y-1]) neighbors.Add(new Node(n.x, n.y-1));
		if (n.x<nsize-1 && !obstacles[n.x+1,n.y])  neighbors.Add(new Node(n.x+1, n.y));
		if (n.y<nsize-1 && !obstacles[n.x,n.y+1])  neighbors.Add(new Node(n.x, n.y+1));   
	}
	// ... and so on with 8 and 16
	
}