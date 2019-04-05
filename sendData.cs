using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendData : MonoBehaviour
{

    //Gathering the data from the game objects within Unity
    public InputField signup_input_username;
    public InputField signup_input_email;
    public InputField signup_input_password;
    public InputField signup_con_pass;

    public Button signup_submit_button;

    private string user_URL = "http://localhost/playerStats/addUsers.php";

    // Update is called once per frame
    void Update()
    {
        //Checking if tab has been pressed (For user convinence)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveInputBox();
        }
    }

    public void ButtonClicked()
    {

        bool verified = false;
        //Checking if data passed is valid
        verified = validateSignUpInfo(false);

        if (verified)
        {
            //If data is valid start corountine
            StartCoroutine(RegisterUser());
        }
        else
        {
            Debug.Log("Email & Password not verified");
        }
        
    }

    IEnumerator RegisterUser()
    {
        //Once data has been validated, send it to database
        WWWForm form = new WWWForm();
        form.AddField("usernamePOST", signup_input_username.text);
        form.AddField("emailPOST", signup_input_email.text);
        form.AddField("paswordPOST", signup_input_password.text);
        WWW www = new WWW(user_URL, form);
        yield return www;

        //If PHP echos 0 then no errors occured so everything was okay
        if (www.text == "0")
        {
            Debug.Log("User created");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed :(. Error #" + www.text);
        }
    }

    public void checkInputSignup()
    {
        //Part of validation. Checking that all input fields have atleast x amount of characters
        signup_submit_button.interactable = (signup_input_username.text.Length >= 5 && signup_input_email.text.Length >= 10 &&
            signup_input_password.text.Length >= 5 && signup_con_pass.text.Length >= 5);
    }

    private bool validateSignUpInfo(bool x)
    {

        bool verify = x;
        bool pass = false, email = false;
        bool hasAt = signup_input_email.text.IndexOf('@') > 0;
        bool hasDot = signup_input_email.text.IndexOf('.') > 0;

        //Checking if Password & Confirm password are equal
        if (signup_input_password.text == signup_con_pass.text)
        {
            pass = true;
        }
        //if email has atleast 9 characters
        if (signup_input_email.text.Length <= 9)
        {
            email = false;
        }
        //Checking if email has atleast 1 '@' & 1 '.'
        else if (hasAt == true && hasDot == true)
        {
            email = true;
        }
        //If email & password pass the tests then they are valid
        if (email == true && pass == true)
        {
            verify = true;
        }
        return verify;
    }

    //Transition between input boxes
    private void MoveInputBox()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Switch Username to Password
            if (signup_input_username.GetComponent<InputField>().isFocused)
            {
                signup_input_password.GetComponent<InputField>().Select();
            }
            //Switch Password to Confirm Password
            if (signup_input_password.GetComponent<InputField>().isFocused)
            {
                signup_con_pass.GetComponent<InputField>().Select();
            }
            //Switch Confirm Password to Email
            if (signup_con_pass.GetComponent<InputField>().isFocused)
            {
                signup_input_email.GetComponent<InputField>().Select();
            }
            //Switch Email back to start
            if (signup_input_email.GetComponent<InputField>().isFocused)
            {
                signup_input_username.GetComponent<InputField>().Select();
            }
        }
    }
}