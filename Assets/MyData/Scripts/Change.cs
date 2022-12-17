using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    public static bool manSelected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changetoWoman()
    {
        manSelected = false;
        SceneManager.LoadScene(3);

    }
    public void changetoMan()
    {
        manSelected = true;
        SceneManager.LoadScene(3);


    }
}
