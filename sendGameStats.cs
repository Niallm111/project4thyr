using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendGameStats : Timer {

    private string user_URL = "http://localhost/playerStats/gamestats.php";

    public static int current_lap = 0;
    public static int beenTriggered = 0;

    public static bool gate_1 = false, gate_2 = false, gate_3 = false;

    //Checking to see if the triggers have been activated by the player
    void Update()
    {
        if (beenTriggered == 1)
        {
            gate_1 = true;
        }
        else if (beenTriggered == 2)
        {
            gate_2 = true;
        }
        else if (beenTriggered == 3)
        {
            gate_3 = true;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        //Trigger attached to finishline collider & checks if plaer has gone through the triggers through the map
        bool lap_completed = false;
        bool player_trigger = false;

        if (collider.tag == "player")
        {
            player_trigger = true;
        }

        if (player_trigger)
        {
            if (gate_1 = true && gate_2 == true && gate_3 == true)
            {
                lap_completed = true;
            }

            if (lap_completed == true)
            {
                gate_1 = false;
                gate_2 = false;
                gate_3 = false;
                beenTriggered = 0;

                //Updating laps & passing that to Timer.cs
                current_lap++;
                Timer.laps++;
                Timer.lap_updated = true;
            }
        }

        //if 3 laps have been completed pass to Timer.cs that the race has been completed
        if (current_lap == 3)
        {
            Timer.send_race_time = true;
        }
    }
}
