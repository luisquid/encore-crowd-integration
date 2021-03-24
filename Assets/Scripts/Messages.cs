using System.Collections;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;

///Documentación de la librería: https://swiftyspiffy.com/TwitchLib/

public class Messages : MonoBehaviour
{
    public Client client;
    private VirtualInputManager VInputManager;
    private UIController uiController;
   
    private void Start()
    {
        Application.runInBackground = true;

        uiController = FindObjectOfType<UIController>();
        VInputManager = FindObjectOfType<VirtualInputManager>();

        if(PlayerPrefs.HasKey("ChannelName"))
        {
            Secrets.channel_name= PlayerPrefs.GetString("ChannelName");
            Initialize();
        }
    }

    #region UI
    /// <summary>
    /// Esta función borra el canal de los PlayerPrefs pero NO reemplaza la escritura del archivo de texto
    /// </summary>
    public void DeleteChannelName()
    {
        PlayerPrefs.DeleteAll();
        client.Disconnect();
        uiController.OpenRegister();
    }
    #endregion
  
    private void Initialize()
    {

        uiController.Connection();

        ConnectionCredentials credentials = new ConnectionCredentials(Secrets.channel_name, Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, Secrets.channel_name);
        
        ///Evento que se llama al recibir un comando desde el chat de twitch
        ///Es una función que se llama automáticamente al haber un comando de la forma !comando
        client.OnChatCommandReceived += OnChatCommandReceived;
        client.OnConnected += Client_OnConnected;
        client.OnConnectionError += Client_OnConnectionError;
        client.Connect();
    }

    private void Client_OnConnectionError(object sender, TwitchLib.Client.Events.OnConnectionErrorArgs e)
    {
        uiController.WriteStatus( $"<color='red'>Error!!! {e.Error.ToString()}");
    }

    private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        uiController.WriteStatus($"Online in {Secrets.channel_name}");
        client.SendMessage(client.JoinedChannels[0], "Twitch Plays by PunisherXA is ONLINE PogChamp");
    }

    /// <summary>
    /// función que es llamada al invocarse el evento de OnChatCommandReceived
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">El parámetro que contiene todos los datos referentes al mensaje, en este caso el comando</param>
    /// Es capaz de traer datos como: Nombre de usuario, id, string del mensaje, etc
    private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {

        switch(e.Command.CommandText)
        {
            ///Comandos que se reciben y tienen como finalidad llamar funciones ligadas a la simulación de las teclas
            #region TWITCH PLAYS
            case "w":
            VInputManager.PressW();
            break;
            case "a":
            VInputManager.PressA();
            break;
            case "s":
            VInputManager.PressS();
            break;
            case "d":
            VInputManager.PressD();
            break;
            case "sp":
            VInputManager.PressSpace();
            break;
            case "hw":
            VInputManager.holdkey("w");
            break;
            case "ha":
            VInputManager.holdkey("a");
            break;
            case "hs":
            VInputManager.holdkey("s");
            break;
            case "hd":
            VInputManager.holdkey("d");
            break;
            ///este no me sirve xD pero es un ejemplo de cómo funcionaría el mouse
            case "mouse":
            VInputManager.MoveMouse(int.Parse(e.Command.ChatMessage.ToString().Split(' ')[0]), int.Parse(e.Command.ChatMessage.ToString().Split(' ')[1]));
            break;
            case "cd":
            VInputManager.ClickDer();
            break;
            case "ci":
            VInputManager.ClickIzq();
            break;
            case "esc":
            VInputManager.holdkey("space");
            break;
            #endregion

            default:
            //client.SendMessage(e.Command.ChatMessage.Channel, $"{e.Command.ChatMessage.DisplayName} No conozco el comando {e.Command.CommandIdentifier}{e.Command.CommandText}");
            break;
        }
    }


    
}
