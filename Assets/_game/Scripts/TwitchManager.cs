﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class TwitchManager : MonoBehaviour
{
    public Client client;
    private string channel_name = "luisquid_";
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
    }

    private void OnUserJoinedStream(object sender, OnUserJoinedArgs e)
    {
        print(e.Username);
    }

    private void MyMessageRecievedFunction(object sender, OnMessageReceivedArgs e)
    {
        print(e.ChatMessage.Username + ": " + e.ChatMessage.Message);
    }

    private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        switch (e.Command.CommandText)
        {
            case "headbang":

                break;
            case "hype":

                break;
            case "encore":

                break;
            case "dance":

                break;
            default:
                break;
        }
    }
}