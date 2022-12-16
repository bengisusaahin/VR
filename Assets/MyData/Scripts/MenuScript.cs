using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuScript : MonoBehaviour
{
    public PlayFabManager playFabManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitButton()
    {
        Application.Quit(); 
    }
    public void ControlsButton()
    {
        SceneManager.LoadScene(2);
    }
    public void backToMenuButton()
    {
        SceneManager.LoadScene(1);
    }
    public void LeaderboardButton()
    {
        SceneManager.LoadScene(4);
    
    }
    public void getLeaderboard()
    {

        playFabManager.getLeaderboard();
    }
}
