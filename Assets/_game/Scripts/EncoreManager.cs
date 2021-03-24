using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class EncoreManager : MonoBehaviour
{
    public GameObject crowdPerson;

    private static EncoreManager _encoreManager;
    public static EncoreManager Instance
    {
        get { return _encoreManager; }
        set { _encoreManager = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void SpawnCrowPerson()
    {
        Instantiate(crowdPerson);
    }

    
}
