using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Agora.Rtc;
using Agora.Util;
using Logger = Agora.Util.Logger;

public class VoiceChatManager : MonoBehaviour
{
    private string appID = "c3dbc891e45c417c96311f8f4b791e2a";
    public static VoiceChatManager Instance;
    internal IRtcEngine RtcEngine = null;
    internal Logger Log;
    public Text LogText;



    private void Start()
    {
        if (CheckAppId())
        {
            InitRtcEngine();
            SetBasicConfiguration();
        }
    }
    private bool CheckAppId()
    {
        Log = new Logger(LogText);
        return Log.DebugAssert(appID.Length > 10, "Please fill in your appId in API-Example/profile/appIdInput.asset!!!!!");
    }
    private void SetBasicConfiguration()
    {
        RtcEngine.EnableAudio();
        RtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
        RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
    }
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    private void InitRtcEngine()
    {
        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
        UserEventHandler handler = new UserEventHandler(this);
        RtcEngineContext context = new RtcEngineContext(appID, 0,
                                    CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
        RtcEngine.Initialize(context);
        RtcEngine.InitEventHandler(handler);
    }
}
#region
internal class UserEventHandler : IRtcEngineEventHandler
{
    private readonly VoiceChatManager _audioSample;

    internal UserEventHandler(VoiceChatManager audioSample)
    {
        _audioSample = audioSample;
    }

    public override void OnError(int err, string msg)
    {
        _audioSample.Log.UpdateLog(string.Format("OnError err: {0}, msg: {1}", err, msg));
    }

    public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        int build = 0;
        _audioSample.Log.UpdateLog(string.Format("sdk version: ${0}",
            _audioSample.RtcEngine.GetVersion(ref build)));
        _audioSample.Log.UpdateLog(
            string.Format("OnJoinChannelSuccess channelName: {0}, uid: {1}, elapsed: {2}",
                            connection.channelId, connection.localUid, elapsed));
    }

    public override void OnRejoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        _audioSample.Log.UpdateLog("OnRejoinChannelSuccess");
    }

    public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
    {
        _audioSample.Log.UpdateLog("OnLeaveChannel");
    }

    public override void OnClientRoleChanged(RtcConnection connection, CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole)
    {
        _audioSample.Log.UpdateLog("OnClientRoleChanged");
    }

    public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
    {
        _audioSample.Log.UpdateLog(string.Format("OnUserJoined uid: ${0} elapsed: ${1}", uid, elapsed));
    }

    public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
    {
        _audioSample.Log.UpdateLog(string.Format("OnUserOffLine uid: ${0}, reason: ${1}", uid,
            (int)reason));
    }
}
#endregion


