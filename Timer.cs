using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private string send_stats_URL = "http://localhost/playerStats/sendGameStats.php";

    public Text race_timer_text;
    public static bool send_race_time = false, lap_updated = false;
    public static int laps = 0;

    private float start_time;
    private Text race_time;

	// Use this for initialization
	void Start () {
        start_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float time_passed = Time.time - start_time;

        //To calculate the in game time
        string mins = ((int)time_passed / 60).ToString();
        string secs = (time_passed % 60).ToString("f1");

        race_timer_text.text = mins + ":" + secs;

        //Getting laps completed from sendGameStats.cs
        if (lap_updated)
        {
            lap_updated = false;
        }
        //If race completed from sendGameStats, calc the race time and pass to DB
        if (send_race_time)
        {
            string total_race_time = race_timer_text.text;

            //Call sendRaceTime passing our race time as a string
            sendRaceTime(total_race_time);

            //Letting Unity know the time has been pased and another race can start
            send_race_time = false;
            
        }

    }

    //Take our race time, gets the user of who's playing and calculates the "Rank" of their time
    //passes it to the coroutine to send the data to db
    private void sendRaceTime(string _total_race_time)
    {
        //Getting username of person logged in
        string user = DBManager.username;
        //passing time of the race to calculate rank
        int ranking = CalculateRanking(_total_race_time);
        //Passing the returned rank, username & race time to coroutine
        StartCoroutine(sendUserStats(user, _total_race_time, ranking));
    }

    IEnumerator sendUserStats(string _user, string _final_race_time, int _ranking)
    {
        //Creating the form that will be passed to a PHP file and inserted into a db
        WWWForm form = new WWWForm();
        form.AddField("usernamePOST", _user);
        form.AddField("lapTimePOST", _final_race_time);
        form.AddField("rankingPOST", _ranking);
        WWW www = new WWW(send_stats_URL, form);
        //Waiting for any returend information from the DB
        yield return www;
        //Printing out any returned data
        Debug.Log("Returned info #" + www.text);
    }

    //Calculating rank of race based upon time
    private int CalculateRanking(string _time)
    {

        bool good_rank = false, medium_rank = false, average_rank = false;
        int rank = 0;

        //Calculate first rank (Good player skill)
        if (_time.Contains("0:") || (_time.Contains("1:")))
        {
            good_rank = true;
        }
        //Calculate first rank (Medium player skill)
        if (_time.Contains("2:"))
        {
            medium_rank = true;
        }
        else if (_time.Contains("3:"))
        {
            medium_rank = true;
        }
        else if (_time.Contains("4:") || (_time.Contains("5:")))
        {
            medium_rank = true;
        }
        //Average rank
        else
        {
            average_rank = true;
        }
        //Assigning rank to send to DB
        if (good_rank)
        {
            rank = 10;
        }
        else if (medium_rank)
        {
            rank = 5;
        }
        else if (average_rank)
        {
            rank = 2;
        }
        return rank;
    }
}
