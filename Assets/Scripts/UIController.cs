using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI channel_name;
    [SerializeField]
    GameObject inputPanel;
    [SerializeField]
    GameObject statusPanel;
    [SerializeField]
    TMPro.TextMeshProUGUI statusText;

  
    public void OpenRegister()
    {
        inputPanel.SetActive(true);
        statusPanel.SetActive(false);
    }

    public void Connection()
    {
        inputPanel.SetActive(false);
        statusPanel.SetActive(true);
    }

    public void WriteStatus(string status)
    {
        statusText.text = status;
    }

}
