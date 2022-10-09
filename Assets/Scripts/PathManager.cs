using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathManager : MonoBehaviour
{
    public Node beginning;
    public Node end;
    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();

    [SerializeField] AgentControllerWaypoints player;
    void Start()
    {
        SetupHeuristics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecutePathFinding()
    {
        FindPath(beginning, end);
    }

    List<Node> ReconstructPath(List<Node> closedList)
    {
        closedList.ForEach(n =>
        {
            Debug.Log(n);
            n.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        });
        player.SetPath(closedList.Select(n => n.gameObject).ToArray());
        return null;
    }

    List<Node> FindPath(Node start, Node goal)
    {
        openList.Clear();
        closedList.Clear();
        start.G = 0;
        openList.Add(start);
        start.setOpen();

        while(openList.Count != 0)
        {
            Node current = FindMinF(openList);
            //current.setCurrent();
            closedList.Add(current);
            current.setClosed();
            if (current == goal) return ReconstructPath(closedList);

            openList.Remove(current);
            foreach(Node neighbor in current.neighbors)
            {
                float tentativeG = distance(current, neighbor) + current.G; //start.G must be 0 but neighbor.G initially should be inifinity
                if(tentativeG < neighbor.G)
                {
                    //closedList.Add(current);//neighbor.predecessor = current; //should dbe at top no? why are we adding current every time?
                    neighbor.G = tentativeG;
                    neighbor.H = calculateH(neighbor, goal);
                    neighbor.F = neighbor.G + neighbor.H;//neighbor.F = tentativeG + neighbor.H;
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        neighbor.setOpen();
                    }
                }
            }
        }
        return null; //Failure
    }


    float distance(Node A, Node B)
    {
        return Vector3.Distance(A.transform.position, B.transform.position);
    }

    float calculateG(Node start, Node node)
    {
        return Vector3.Distance(start.transform.position, node.transform.position);
    }

    float calculateH(Node A, Node B)
    {
        return distance(A, B);
    }

    void SetupHeuristics()
    {

    }
    Node FindMinF(List<Node> list)
    {
        Node min = list[0];
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].F < min.F)
                min = list[i];
        }
        return min;
    }
}
