using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class EncoreManager : MonoBehaviour
{
    public GameObject crowdPerson;
    public Animator cameraAnimator;
    public GameObject corner1;
    public GameObject corner3;

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
        
        if(crowd.Length == 0)
        {
            temp.GetComponent<CrowdPerson>().Initialize(_name);
        }

        for (int i = 0; i < crowd.Length; i++)
        {
            if(Vector3.Distance(temp.transform.position, crowd[i].transform.position) < 1.5f)
            {
                SpawnCrowdPerson(_name);
            }
            else
            {
                temp.GetComponent<CrowdPerson>().Initialize(_name);
            }
        }
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

    public void SetCrowdPersonAnimation(string _userName, int animationId)
    {
        print("Animation ID: " + animationId);
        GameObject[] tempCrowd = GameObject.FindGameObjectsWithTag("CrowdPerson");

        for(int i = 0; i < tempCrowd.Length; i++)
        {
            if(tempCrowd[i].GetComponent<CrowdPerson>().personName == _userName)
            {
                switch (animationId)
                {
                    case 0:
                        tempCrowd[i].GetComponent<CrowdPerson>().CheerStream();
                        break;
                    case 1:
                        tempCrowd[i].GetComponent<CrowdPerson>().CryStream();
                        break;
                    default:

                        break;
                }
            }
        } 
    }

    public void TurnCamera(int _turnDirection)
    {
        if (_turnDirection == 0)
        {
            cameraAnimator.SetBool("TurnLeft", false);
            cameraAnimator.SetBool("TurnRight", false);
        }
        else if(_turnDirection == 1)
        {
            cameraAnimator.SetBool("TurnLeft", true);
            cameraAnimator.SetBool("TurnRight", false);
        }
        else
        {
            cameraAnimator.SetBool("TurnLeft", false);
            cameraAnimator.SetBool("TurnRight", true);
        }
    }
    
}
