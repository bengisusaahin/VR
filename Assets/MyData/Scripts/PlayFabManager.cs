using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    public Text messageText;
    public TextMesh playerName;
    public InputField email;
    public InputField password;
    public InputField DisplayName;

    private void Update()
    {
        if(playerName != null)
        {
            playerName.text = name;
        }
    }
    public void RegisterButton() //Checks if the password is less than 6 characters and creates a request.
    {
        if (password.text.Length < 6)
        {
            messageText.text = "Password too short";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            RequireBothUsernameAndEmail = false

        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError); //This request used for API.
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result) //Throws Registered message 
    {
        messageText.text = "Registered! Press login";
    }
    public void LoginButton() // Playfabid yi alýr.
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void OnLoginSuccess(LoginResult result) //If login successfully, throws logged in message
                                                   //and check the name parameter. If name is null, leads to
                                                   //username creation scene, else it leads you directly to the main menu
    {
        messageText.text = "Logged in";
        Debug.Log("logged in");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.PlayerId;
            
        }
        if (name == null)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SubmitNameButton() //Replaces the null username in the database with the username entered by the user.
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = DisplayName.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result) //Throws name updated message
    {
        Debug.Log("Name updated");
        SceneManager.LoadScene(1);
    }


    void OnError(PlayFabError error) //Throws error message
    {
        Debug.Log("Error while logging in/creating account!");
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    public void SendLeaderboard(int score) //Oyundaki veriyi clouda gönderiyo
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate{
            StatisticName = "PlatformScore",
            Value = score
            }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) //Throws log message 
    {
        Debug.Log("Successfull leaderboard sent");

    }
    public void getLeaderboard() //Get leaderboard from database 
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 99

        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    public void OnLeaderboardGet(GetLeaderboardResult result) //Print the leaderboard.
                                                              //It does not print after 99 data. It destroys it directly.
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
