using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdPerson : MonoBehaviour
{
    public string personName;
    private TextMeshPro personNameText;
    private Animator personAnim;
    int danceIdParam;

    public void Initialize(string userName)
    {
        personAnim = GetComponent<Animator>();
        personNameText = GetComponentInChildren<TextMeshPro>();
        danceIdParam = Animator.StringToHash("DanceId");

        AssignRandomDance();
        personName = userName;
        personNameText.text = personName; 
    }

    public void AssignRandomDance()
    {
        personAnim.SetInteger(danceIdParam, Random.Range(0, 5));
    }
}
