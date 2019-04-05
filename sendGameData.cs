using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendGameData : MonoBehaviour
{

    //Gathering the data from the game objects within Unity
    public InputField signup_input_username;
    public InputField signup_input_email;
    public InputField signup_input_password;
    public InputField signup_con_pass;

    public Button signup_submit_button;

    private string user_URL = "http://localhost/playerStats/addUsers.php";

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveInputBox();
        }
    }

    public void ButtonClicked()
    {
        bool verified = false;
        verified = validateSignUpInfo(false);

        if (verified)
        {
            Debug.Log("You have verified your email & password");
        }
        else
        {
            Debug.Log("Email & Password not verified");
        }
        //StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePOST", signup_input_username.text);
        form.AddField("emailPOST", signup_input_email.text);
        form.AddField("paswordPOST", signup_input_password.text);
        WWW www = new WWW(user_URL, form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("User created");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed :(. Error #" + www.text);
        }
    }*/
}