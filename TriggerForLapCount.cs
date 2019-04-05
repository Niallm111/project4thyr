using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerForLapCount : sendGameStats {

    //Checking triggers placed around the track to see if the player is going around the intended way
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player")
        {
            sendGameStats.beenTriggered ++;
        }
    }
}
