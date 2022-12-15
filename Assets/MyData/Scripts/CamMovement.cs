using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    float rotationOnX;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * 2.4f;
        float mouseX = Input.GetAxis("Mouse X")  * 5f;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -85f, 85f);
        transform.localEulerAngles=new Vector3(rotationOnX, 0f, 0f);
        player.Rotate(Vector3.up, mouseX);
        
    }
}
