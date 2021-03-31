using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class EncoreManager : MonoBehaviour
{
    public GameObject crowdPerson;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject corner3;
    public GameObject corner4;

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

    public void SpawnCrowdPerson(string _name)
    {
        GameObject[] crowd = GameObject.FindGameObjectsWithTag("CrowdPerson");

        for (int i = 0; i < crowd.Length; i++)
        {
            if (crowd[i].GetComponent<CrowdPerson>().personName == _name)
            {
                return;
            }
        }

        GameObject temp = Instantiate(crowdPerson, new Vector3(Random.Range(corner1.transform.position.x, corner3.transform.position.x), 
                                                               0.1f, 
                                                               Random.Range(corner1.transform.position.z, corner3.transform.position.z)), Quaternion.identity);
        temp.GetComponent<CrowdPerson>().Initialize(_name);
    }

    public void DestroyCrowdPerson(string _name)
    {
        GameObject[] crowd = GameObject.FindGameObjectsWithTag("CrowdPerson");

        for(int i = 0; i < crowd.Length; i++)
        {
            if(crowd[i].GetComponent<CrowdPerson>().personName == _name)
            {
                Destroy(crowd[i]);
                break;
            }
        }
    }
    
}
