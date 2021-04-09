using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;
using TMPro;

public class TwitchManager : MonoBehaviour
{
    public Client client;
    public TextMeshPro lastChatMessage;

    private void Awake()
    {
        Application.runInBackground = true;
        ConnectionCredentials credentials = new ConnectionCredentials(Secrets.channel_name, Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, Secrets.channel_name);

        client.Connect();

        client.OnMessageReceived += MyMessageRecievedFunction;
        client.OnChatCommandReceived += OnChatCommandReceived;
        client.OnUserJoined += OnUserJoinedStream;
        client.OnUserLeft += OnUserLeftStream;
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
        //print(e.ChatMessage.Username + ": " + e.ChatMessage.Message);

        if(e.ChatMessage.Message[0] != '!')
            lastChatMessage.text = e.ChatMessage.Message;
        EncoreManager.Instance.SpawnCrowdPerson(e.ChatMessage.Username, e.ChatMessage.UserId);

    }

    private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        string whoSentTheCommand = e.Command.ChatMessage.Username;
        string whoSentId = e.Command.ChatMessage.UserId;

        switch (e.Command.CommandText)
        {
            case "headbang":

                break;
            case "cheer":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Cheer);
                break;
            case "cry":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Crying);
                break;
            case "vibe":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Vibe);
                break;
            case "move":
                EncoreManager.Instance.SendRandomPosition(e.Command.ChatMessage.Username);
                break;
            case "dance":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Dancing);
                break;
            case "jump":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Jump);
                break;
            case "twerk":
                EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Twerk);
                break;
            case "fight":
                string fight = "Haha idiot -->";
                string userArgument = e.Command.ArgumentsAsList[0];
                if(userArgument.Contains("@"))
                {
                    if(userArgument.Split('@')[1] == whoSentTheCommand.ToLower())
                    {
                        fight = whoSentTheCommand + ", the worst fight we fight is the fight we fight with ourselves.";
                        EncoreManager.Instance.SetCrowdPersonAnimation(whoSentId, (int)TriggerId.Trigger_Dead);
                    }
                    else
                    {
                        fight = e.Command.ChatMessage.Username + " is going to fight " + e.Command.ArgumentsAsList[0].Split('@')[1];
                        EncoreManager.Instance.SendUserPosition(whoSentTheCommand.ToLower(), e.Command.ArgumentsAsList[0].Split('@')[1].ToLower());
                    }
                }
                lastChatMessage.text = fight;
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
            case "commands":
                client.SendMessage(e.Command.ChatMessage.Channel, "Comandos de personaje disponibles: !move, !dance, !cry, !cheer, !jump\nComandos de cámara disponibles: !left, !center, !right");
            break;
              
            default:
                break;
        }
    }
}
