using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Text user_logged_in;

    private void Start()
    {
        //Getting the user logged in
        if (DBManager.LoggedIn)
        {
            user_logged_in.text = "Player: " + DBManager.username;
        }
    }

    public void changeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}