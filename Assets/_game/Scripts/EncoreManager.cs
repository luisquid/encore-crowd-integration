using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class EncoreManager : MonoBehaviour
{
    [Header("Crowd List")]
    public List<CrowdPerson> spawnedPeople;
    public GameObject crowdPerson;

    [Header("Camera Movement")]
    public Animator cameraAnimator;
    
    [Header("Crowd Spawn Area")]
    public GameObject corner1;
    public GameObject corner3;
    
    [Header("Baphomet Event")]
    public GameObject baphoPerson;
    public GameObject spawnPoint;

    #region SINGLETON
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
    #endregion

    private void Start()
    {
        spawnedPeople = new List<CrowdPerson>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.GetKeyDown(KeyCode.F1))
        {
            TwerkAll();
        }
    }

    /*public void DanceAll()
    {
        for (int i = 0; i < spawnedPeople.Count; i++)
        {
            spawnedPeople[i].AssignRandomDance();
        }
    }*/

    public void TwerkAll()
    {
        for (int i = 0; i < spawnedPeople.Count; i++)
        {
            spawnedPeople[i].TriggerAnimation(TriggerId.Trigger_Twerk);
        }
    }

    public bool CheckIfSpawned(string _id)
    {
        bool isSpawned = false;
        for(int i = 0; i < spawnedPeople.Count; i++)
        {
            if(spawnedPeople[i].personId == _id)
            {
                isSpawned = true;
            }
        }
        return isSpawned;
    }

    public void SpawnCrowdPerson(string _name, string _id)
    {
        if (CheckIfSpawned(_id))
            return;

        GameObject temp = Instantiate(crowdPerson, new Vector3(Random.Range(corner1.transform.position.x, corner3.transform.position.x), 
                                                               0.05f, 
                                                               Random.Range(corner1.transform.position.z, corner3.transform.position.z)), Quaternion.identity);

        temp.GetComponent<CrowdPerson>().Initialize(_name, _id);
        spawnedPeople.Add(temp.GetComponent<CrowdPerson>());
        
        /*
        if(spawnedPeople.Count == 0)
        {
            spawnedPeople.Add(temp.GetComponent<CrowdPerson>());
            temp.GetComponent<CrowdPerson>().Initialize(_name, _id);
        }

        else
        {
            for (int i = 0; i < spawnedPeople.Count; i++)
            {
                if (Vector3.Distance(temp.transform.position, spawnedPeople[i].gameObject.transform.position) < 1.5f)
                {
                    SpawnCrowdPerson(_name, _id);
                }
                else
                {
                    spawnedPeople.Add(temp.GetComponent<CrowdPerson>());
                    temp.GetComponent<CrowdPerson>().Initialize(_name, _id);
                }
            }
        }    */
        
    }

    //Recursive function to assign position that isn't held yet
    public void AssignRandomPosition()
    {

    }

    public void SendRandomPosition(string _username)
    {
        for (int i = 0; i < spawnedPeople.Count; i++)
        {
            if (spawnedPeople[i].personName == _username)
            {
                spawnedPeople[i].MoveToPosition(new Vector3(Random.Range(corner1.transform.position.x, corner3.transform.position.x),
                                                               0.05f,
                                                               Random.Range(corner1.transform.position.z, corner3.transform.position.z)));
            }
        }
    }

    public void SendUserPosition(string fighter, string tobefought)
    {
        GameObject[] crowd = GameObject.FindGameObjectsWithTag("CrowdPerson");

        for (int i = 0; i < crowd.Length; i++)
        {
            for(int j = 0; j < crowd.Length; j++)
            {
                if (crowd[i].GetComponent<CrowdPerson>().personName == fighter && crowd[j].GetComponent<CrowdPerson>().personName == tobefought)
                {
                    crowd[i].GetComponent<CrowdPerson>().MoveToPosition(crowd[j].transform.position);
                }
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

    public void SetCrowdPersonAnimation(string _userId, int animationId)
    {
        TriggerId animId = (TriggerId)animationId;

        for(int i = 0; i < spawnedPeople.Count; i++)
        {
            if(spawnedPeople[i].personId == _userId)
            {
                spawnedPeople[i].TriggerAnimation(animId);
            }
        }    
    }

    public void SpawnBapho()
    {
        Instantiate(baphoPerson, spawnPoint.transform.position, Quaternion.identity);
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
