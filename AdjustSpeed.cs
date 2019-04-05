using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpeed : AiMove {

    static bool easy_called = false;
    static bool medium_called = false;
    static bool hard_called = false;

    //Adjust the base force given the ranking from AiMove.cs
    public static void easy ()
    {
        float multiplier = 5f;

        if (easy_called)
        {
            Debug.Log("Adjust difficulty has been accessed already :)");
        }
        else if (!easy_called)
        {
            AiMove.moveTourge = AiMove.moveTourge / multiplier;
        }      
        easy_called = true;
    }
    //Adjust the base force given the ranking from AiMove.cs
    public static void medium()
    {
        float multiplier = 1.5f;

        if (medium_called)
        {
            Debug.Log("Adjust difficulty has been accessed already :)");
        }
        else if (!medium_called)
        {
            AiMove.moveTourge = AiMove.moveTourge * multiplier;
        }
        medium_called = true;       
    }
    //Adjust the base force given the ranking from AiMove.cs
    public static void hard()
    {
        float multiplier = 3f;

        if (hard_called)
        {
            Debug.Log("Adjust difficulty has been accessed already :)");
        }
        else if (!hard_called)
        {
            Debug.Log("Speed should be 5,000, is: " + AiMove.moveTourge);
            AiMove.moveTourge = AiMove.moveTourge * multiplier;
            Debug.Log("Speed should be 15,000, is: " + AiMove.moveTourge);
        }
        hard_called = true;
    }
}
