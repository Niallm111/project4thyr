using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    /* Taken from a tutorial online and modified for the game in unity
     * However, not in use
     */

    //X & Y positions for the grid of nodes
    public int gridX;
    public int GridY;

    //Checking for the walls within the game that define
    //where the track is outlined
    public bool IsWall;
    public Vector3 Position;

    //Track the last node visited
    public Nodes ParentNode;

    //The cost of moving to the next square.
    public int gCost;
    //The distance to the goal from this node.
    public int hCost;

    //Calculate the FCost, gCost + hCost. For guided search
    public int FCost { get { return gCost + hCost; } }

    public Nodes(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        IsWall = a_IsWall;
        Position = a_Pos;
        gridX = a_gridX;
        GridY = a_gridY;
    }
}