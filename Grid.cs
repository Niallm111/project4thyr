using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    /* Taken from a tutorial online and modified for the game in unity
     * However, not in use
     */ 

    public Transform StartPos;
    public LayerMask Walls;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    public float DistanceBetweenNodes;
    public Rigidbody car;

    //The array of nodes that the A Star algorithm uses
    Nodes[,] NodeArray;
    public List<Nodes> FinalPath;


    float fNodeDiameter;
    int GridSizeX, GridSizeY;


    private void Start()
    {
        //Double the radius to get diameter
        fNodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / fNodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y / fNodeDiameter);
        //Draw the grid
        CreateGrid();

        car = GetComponent<Rigidbody>();
    }

    void CreateGrid()
    {
        NodeArray = new Nodes[GridSizeX, GridSizeY];
        //Get the world position of the bottom left of the grid.
        Vector3 bottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + NodeRadius) + Vector3.forward * (y * fNodeDiameter + NodeRadius);
                bool Wall = true;

                if (Physics.CheckSphere(worldPoint, NodeRadius, Walls))
                {
                    Wall = false;
                }

                NodeArray[x, y] = new Nodes(Wall, worldPoint, x, y);
            }
        }
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Nodes> GetNeighboringNodes(Nodes a_NeighborNode)
    {
        List<Nodes> NeighborList = new List<Nodes>();
        int icheckX;
        int icheckY;

        //Check the right side of the current node.
        icheckX = a_NeighborNode.iGridX + 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < GridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Left side of the current node.
        icheckX = a_NeighborNode.iGridX - 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < GridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Top side of the current node.
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY + 1;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < GridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Bottom side of the current node.
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY - 1;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < GridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        return NeighborList;
    }

    //Gets the closest node to the given world position.
    public Nodes NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float xPos = ((a_vWorldPos.x + GridWorldSize.x / 2) / GridWorldSize.x);
        float yPos = ((a_vWorldPos.z + GridWorldSize.y / 2) / GridWorldSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int ix = Mathf.RoundToInt((GridSizeX - 1) * xPos);
        int iy = Mathf.RoundToInt((GridSizeY - 1) * yPos);

        return NodeArray[ix, iy];
    }


    //Function that draws the wireframe
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

        if (NodeArray != null)
        {
            foreach (Nodes n in NodeArray)
            {
                if (n.bIsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }


                if (FinalPath != null)
                {
                    if (FinalPath.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }

                }
                Gizmos.DrawCube(n.vPosition, Vector3.one * (fNodeDiameter - DistanceBetweenNodes));
            }
        }
    }
}