using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public GameObject ball;
    public GameObject myHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ball.transform.SetParent(myHand.transform);
            ball.transform.localPosition = myHand.transform.localPosition;
        }

    }
}
