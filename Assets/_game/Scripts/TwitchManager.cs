using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class TwitchManager : MonoBehaviour
{
    public Client client;
    private string channel_name = "luisquid_";

    private PubSub _pubSub;

    private void Awake()
    {
        Application.runInBackground = true;
        ConnectionCredentials credentials = new ConnectionCredentials("luisquid_", Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channel_name);

        client.Connect();

        client.OnMessageReceived += MyMessageRecievedFunction;
        client.OnChatCommandReceived += OnChatCommandReceived;
        client.OnUserJoined += OnUserJoinedStream;
        client.OnUserLeft += OnUserLeftStream;

        _pubSub = new PubSub();

        _pubSub.OnChannelCommerceReceived += OnChannelPoints;

        _pubSub.Connect();
    }

    private void OnChannelPoints(object sender, TwitchLib.PubSub.Events.OnChannelCommerceReceivedArgs e)
    {
        print(e.ItemDescription);
    }

    private void OnUserJoinedStream(object sender, OnUserJoinedArgs e)
    {
        print("USER JOINED STREAM: " + e.Username);

        //EncoreManager.Instance.SpawnCrowdPerson(e.Username);
    }

    private void OnUserLeftStream(object sender, OnUserLeftArgs e)
    {
        print("USER LEFT STREAM: " + e.Username);

        //EncoreManager.Instance.DestroyCrowdPerson(e.Username);
    }

    private void MyMessageRecievedFunction(object sender, OnMessageReceivedArgs e)
    {
        print(e.ChatMessage.Username + ": " + e.ChatMessage.Message);

        EncoreManager.Instance.SpawnCrowdPerson(e.ChatMessage.Username);
    }

    private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        switch (e.Command.CommandText)
        {
            case "headbang":

                break;
            case "cheer":
                EncoreManager.Instance.SetCrowdPersonAnimation(e.Command.ChatMessage.Username, 0);
                break;
            case "cry":
                EncoreManager.Instance.SetCrowdPersonAnimation(e.Command.ChatMessage.Username, 1);
                break;
            case "encore":

                break;
            case "move":
                EncoreManager.Instance.SendRandomPosition(e.Command.ChatMessage.Username);
                break;
            case "dance":

                break;
            case "left":
                EncoreManager.Instance.TurnCamera(1);
                break;
            case "right":
                EncoreManager.Instance.TurnCamera(2);
                break;
            case "center":
                EncoreManager.Instance.TurnCamera(0);
                break;
            default:
                break;
        }
    }
}
