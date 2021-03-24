using System;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    TextAsset textFile;
    string text;
    string[] lines;

    UIController uiController;
    void OnEnable()
    {
        uiController = FindObjectOfType<UIController>();
        string datapath = Application.dataPath;
        Debug.Log(datapath);
        //textFile = Resources.Load<TextAsset>($"{datapath}/secrets") as TextAsset; // Loads file
        text = File.ReadAllText(Application.streamingAssetsPath + "/secrets.txt");
        //text = textFile.ToString(); // Converts to string
        lines = text.Split('\n');

        if(lines != null && text!=null)
        {
            WriteChannelName();

        } else
        {
            uiController.WriteStatus($"<color='red'> Error!!! File or Channel Name not found");

        }

        
        //Secrets.bot_channel_name = lines[1];
        //Secrets.bot_access_token = lines[2];
    }

    public void WriteChannelName()
    {
        Secrets.channel_name = lines[0];
        PlayerPrefs.SetString("ChannelName", Secrets.channel_name);
    }

}
