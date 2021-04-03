//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TwitchLib.Unity;
//using TwitchLib.PubSub.Events;

//using UnityEngine.Events;

//public class TwitchPubSub : MonoBehaviour
//{
//    PubSub pubSub;
//    WindowsVoice tts;
//    #region Events
//    #endregion

//    public GameObject brokenScreen;
//    void Start()
//    {
//        InitializePubSub();
//        tts = FindObjectOfType<WindowsVoice>();
//    }

//    public UnityEvent OnNewFollowerByTwitch;
//    public UnityEvent OnNewSub;
//    public UnityEvent Awa;

//    private void InitializePubSub()
//    {
//        pubSub = new PubSub();
//        pubSub.OnFollow += PubSub_OnFollow;
//        pubSub.OnRewardRedeemed += PubSub_OnRewardRedeemed;
//        pubSub.OnPubSubServiceConnected += PubSub_OnPubSubServiceConnected;
//        pubSub.OnBitsReceived += PubSub_OnBitsReceived;
//        pubSub.OnChannelSubscription += PubSub_OnChannelSubscription;
//        pubSub.OnLog += PubSub_OnLog;
//        pubSub.OnWhisper += PubSub_OnWhisper;

//        pubSub.Connect();
//    }

//    private void PubSub_OnWhisper(object sender, OnWhisperArgs e)
//    {
//        Debug.Log(sender.ToString());
//        Debug.Log(e.Whisper.ToString() + " by " + e.ChannelId);
//    }

//    private void PubSub_OnLog(object sender, OnLogArgs e)
//    {
//        Debug.Log(e.Data);
//    }

//    private void PubSub_OnRewardRedeemed(object sender, OnRewardRedeemedArgs e)
//    {
//        Debug.Log(e.RewardTitle.ToString());
//        switch(e.RewardTitle.ToString())
//        {
//            case "Desestresar a Puni":
//                FindObjectOfType<UnityChanAnimatorControl>().RestUnityChan();
//            break;
//            case "Grita a Puni":
//                Debug.Log(e.Message);
//                tts.Test(e.Message);
//            break;
//            case "Awa":
//            Awa.Invoke();
//            //brokenScreen.GetComponent<BrokenScreen>().StartBroken();
//            break;
//            case "RompePantallas":
//            brokenScreen.GetComponent<BrokenScreen>().StartBroken();
//            break;
//        }
//    }

//    private void PubSub_OnChannelSubscription(object sender, OnChannelSubscriptionArgs e)
//    {
//        OnNewSub.Invoke();
//    }

//    private void PubSub_OnBitsReceived(object sender, OnBitsReceivedArgs e)
//    {
//        Debug.Log(e.BitsUsed);
//    }

//    private void PubSub_OnFollow(object sender, OnFollowArgs e)
//    {
//        OnNewFollowerByTwitch.Invoke();
//    }

//    private void PubSub_OnPubSubServiceConnected(object sender, System.EventArgs e)
//    {
//        pubSub.ListenToFollows(Secrets.user_id);
//        pubSub.ListenToRewards(Secrets.user_id);
//        pubSub.ListenToBitsEvents(Secrets.user_id);
//        //pubSub.ListenToSubscriptions(Secrets.user_id);
//        pubSub.SendTopics(Secrets.channel_access_token);
//    }


//}
