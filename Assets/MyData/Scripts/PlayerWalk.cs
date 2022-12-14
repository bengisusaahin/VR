using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int playerSpeed;
    [SerializeField] private PhotonView view;
    [SerializeField] private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
                //rigid.AddForce(100 * Time.deltaTime, 0, 0);
            }
        }
        
    }
}
