using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour
{

    //Gathering the data from the game objects within Unity
    public InputField username;
    public InputField password;

    public Button login_submit_button;

    private string user_URL = "http://localhost/playerStats/login.php";

    public void LoginButtonClicked()
    {

        StartCoroutine(LoginPlayer());
        
    }

    public void checkInputLogin()
    {
        //basic valdiation checking
        login_submit_button.interactable = (username.text.Length >= 5 && password.text.Length >= 5);
    }

    IEnumerator LoginPlayer()
    {
        //pass login data given to unity to DB and see if it matches
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        WWW www = new WWW(user_URL, form);
        yield return www;

        //if no errors (returns 0) contine
        if (www.text[0] == '0')
        {
            DBManager.username = username.text;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }
}