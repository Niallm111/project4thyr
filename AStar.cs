using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    /* Taken from a tutorial online and modified for the game in unity
     * However, not in use
     */

    //Reference to Grid class
    Grid GridReference;
    public Transform StartPosition;
    public Transform TargetPosition;

    private void Awake()
    {
        GridReference = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Nodes StartNode = GridReference.NodeFromWorldPoint(a_StartPos);
        Nodes TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);

        List<Nodes> OpenList = new List<Nodes>();
        HashSet<Nodes> ClosedList = new HashSet<Nodes>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Nodes CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Nodes NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))
            {
                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.igCost = MoveCost;
                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.ParentNode = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }

        }
    }



    void GetFinalPath(Nodes a_StartingNode, Nodes a_EndNode)
    {
        List<Nodes> FinalPath = new List<Nodes>();
        Nodes CurrentNode = a_EndNode;

        while (CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.ParentNode;
        }
        //Reverse the path to get the correct order from start to front
        FinalPath.Reverse();

        //Set the final path
        GridReference.FinalPath = FinalPath;
    }

    int GetManhattenDistance(Nodes a_nodeA, Nodes a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);

        return ix + iy;
    }
}