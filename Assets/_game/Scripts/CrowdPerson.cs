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
    int cheerIdParam;
    int cryIdParam;


    public void Initialize(string userName)
    {
        personAnim = GetComponent<Animator>();
        personNameText = GetComponentInChildren<TextMeshPro>();
        danceIdParam = Animator.StringToHash("DanceId");
        cheerIdParam = Animator.StringToHash("Cheer");
        cryIdParam = Animator.StringToHash("Crying");

        //AssignRandomDance();
        personName = userName;
        personNameText.text = personName; 
    }

    public void AssignRandomDance()
    {
        personAnim.SetInteger(danceIdParam, Random.Range(0, 5));
    }

    public void CheerStream()
    {
        personAnim.SetTrigger(cheerIdParam);
    }

    public void CryStream()
    {
        personAnim.SetTrigger(cryIdParam);
    }
}
