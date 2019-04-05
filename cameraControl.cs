using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {

    public Transform player;
    public Vector3 offset;
    public float rotateSpeed;

    public bool useOffset;

    private void Start()
    {
        //Hide Cursor on play
        //Cursor.lockState = CursorLockMode.Locked;

        if (!useOffset)
        {
            offset = player.position - transform.position;
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        player.Rotate(-vertical, 0, 0);

        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = player.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = player.position - (rotation * offset);


        //transform.position = player.position - offset;
        transform.LookAt(player);
    }
}
