using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject baphoPrefab;

    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get { return _gameManager; }
        set { _gameManager = value; }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnRagDoll()
    {
        Instantiate(baphoPrefab);
    }
}
