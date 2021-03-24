using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdPerson : MonoBehaviour
{
    private TextMeshPro personName;
    Animator personAnim;
    int danceIdParam;

    private void Start()
    {
        personAnim = GetComponent<Animator>();
        personName = GetComponentInChildren<TextMeshPro>();
        danceIdParam = Animator.StringToHash("DanceId");    
    }

    public void Initialize(string userName)
    {
        AssignRandomDance();
        personName.text = userName; 
    }

    public void AssignRandomDance()
    {
        personAnim.SetInteger(danceIdParam, Random.Range(0, 5));
    }
}
