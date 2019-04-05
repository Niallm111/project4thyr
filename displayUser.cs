using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayUser : MonoBehaviour {

    public Text username;

    void Update()
    {
        //Getting the username of the person logged in and displaying it in game
        username.text = "Player: " + DBManager.username;
    }
}
