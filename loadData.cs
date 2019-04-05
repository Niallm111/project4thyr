using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadData : MonoBehaviour {

    public string[] dataValues;

    //Returns all data stored in the leaderboards table in database

	// Use this for initialization
	IEnumerator Start () {
        WWW leaderboardData = new WWW("http://localhost/playerStats/leaderboard.php");
        yield return leaderboardData;
        string leaderboardDataString = leaderboardData.text;
        print(leaderboardDataString);
        dataValues = leaderboardDataString.Split(';');
        print(GetIndividualValues(dataValues[0], "Username:"));
    }

    string GetIndividualValues (string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
