using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int playerSpeed;
    [SerializeField] private PhotonView view;
    [SerializeField] private Rigidbody rigid;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("IsWalking",true);
                //transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
                rigid.AddForce(1000 * Time.deltaTime, 0, 0);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
            if (Input.GetKey(KeyCode.C))
            {
                animator.SetTrigger("Clap");
            }
        }
        
    }
}
