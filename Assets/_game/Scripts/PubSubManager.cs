using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.PubSub.Events;

public class PubSubManager : MonoBehaviour
{
    PubSub pubSub;

    private void Start()
    {
        pubSub = new PubSub();

        pubSub.OnLog += PS_OnLog;
        pubSub.OnFollow += PS_OnFollow;
        pubSub.OnWhisper += PS_OnWhisper;
        pubSub.OnRewardRedeemed += PS_OnRewardRedeemed;
        pubSub.OnPubSubServiceConnected += PS_OnSubPubServiceConnected;

        pubSub.Connect();
    }

    private void PS_OnLog(object sender, OnLogArgs e)
    {
        //print(e.Data.ToString());
    }

    private void PS_OnFollow(object sender, OnFollowArgs e)
    {

    }

    private void PS_OnWhisper(object sender, OnWhisperArgs e)
    {
        Debug.Log(sender.ToString());
        Debug.Log(e.Whisper.ToString() + " by " + e.ChannelId);
    }

    private void PS_OnRewardRedeemed(object sender, OnRewardRedeemedArgs e)
    {
        print("REWARD: " + e.RewardTitle);
        switch(e.RewardTitle.ToString())
        {
            case "test":
                EncoreManager.Instance.SpawnBapho();
            break;

            default:

            break;
        }
    }

    private void PS_OnSubPubServiceConnected(object sender, System.EventArgs e)
    {
        pubSub.ListenToFollows(Secrets.user_id);
        pubSub.ListenToRewards(Secrets.user_id);
        pubSub.SendTopics(Secrets.bot_access_token);
    }
}
