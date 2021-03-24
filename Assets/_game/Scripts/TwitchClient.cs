using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;
using System;

public class TwitchClient : MonoBehaviour
{
    public Client client;
    public Baphomet bapho;

    private string channel_name = "luisquid_";

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials("luisquid_", Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channel_name);

        client.Connect();

        client.OnMessageReceived += MyMessageRecievedFunction;
        client.OnChatCommandReceived += OnChatCommandReceived;
        
    }

    private void MyMessageRecievedFunction(object sender, OnMessageReceivedArgs e)
    {
        print(e.ChatMessage.Username + ": " + e.ChatMessage.Message);
    }

    private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        switch(e.Command.CommandText)
        {
            case "hola":
                bapho.DoTheThing();
                break;
            case "break":
                GameManager.Instance.SpawnRagDoll();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            client.SendMessage(client.JoinedChannels[0], "This is a message from the bot");
        }
    }
}
