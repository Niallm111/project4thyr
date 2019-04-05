using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMove : MonoBehaviour {

    private string get_ranking_URL = "http://localhost/playerStats/getRating.php";

    public Transform path;
    public float steerAngle = 45f;
    public WheelCollider FR;
    public WheelCollider FL;
    public WheelCollider RR;
    public WheelCollider RL;
    public static float moveTourge = 5000.0f;
    private string difficult_setting;

    private List<Transform> nodes;
    private int currentNode = 0;

    private void Awake()
    {
        //Get the difficulty of the game for the player
        getDifficulty();
    }

	// Use this for initialization
	private void Start () {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        //Get the nodes for the cars to follow
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
        ApplySteering();
        CarDrive();
        CheckNodeDistance();
	}

    private void getDifficulty()
    {
        //Starts coroutine to get the rank
        StartCoroutine(getRank());
    }

    IEnumerator getRank()
    {
        //Gets username of person currently logged in
        string _user = DBManager.username;

        //pass username to DB to get rank
        WWWForm form = new WWWForm();
        form.AddField("username", _user);

        WWW www = new WWW(get_ranking_URL, form);
        //Await return from DB
        yield return www;

        //Based on returned number, adjust the game difficulty
        if (www.text.Contains("2"))
        {
            AdjustSpeed.easy();
        }
        else if (www.text.Contains("5"))
        {
            AdjustSpeed.medium();
        }
        else if (www.text.Contains("10"))
        {
            AdjustSpeed.hard();
        }
    }

    //To adjust the steering for the cars to navigate the map
    private void ApplySteering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteering = (relativeVector.x / relativeVector.magnitude) * steerAngle;
        FL.steerAngle = newSteering;
        FR.steerAngle = newSteering;
        RL.steerAngle = newSteering;
        RR.steerAngle = newSteering;
    }

    private void CarDrive()
    {
        //Apply the force for the cars to move
        FL.motorTorque = moveTourge;
        FR.motorTorque = moveTourge;
    }

    private void CheckNodeDistance()
    {
        //Cycle through the nodes that the cars follow and update the current node to go to
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 20.0f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
