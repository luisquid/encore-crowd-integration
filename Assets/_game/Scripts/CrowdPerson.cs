using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdPerson : MonoBehaviour
{
    public string personName;
    public float speed;

    int danceIdParam;
    int cheerIdParam;
    int cryIdParam;
    int walkIdParam;

    private TextMeshPro personNameText;
    private Animator personAnim;
    
    private float journeyLength;
    private float startTime;

    private Vector3 initialPosition;
    private Vector3 endPosition;
    
    private void Update()
    {
        if(Vector3.Distance(endPosition, transform.position) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(initialPosition, endPosition, fractionOfJourney);
        }
        
        else
        {
            print("I AM LOOKING TO THE FRONT");
            transform.rotation = Quaternion.identity;
            personAnim.SetBool(walkIdParam, false);
        }
    }


    public void Initialize(string userName)
    {
        personAnim = GetComponent<Animator>();
        personNameText = GetComponentInChildren<TextMeshPro>();
        danceIdParam = Animator.StringToHash("DanceId");
        cheerIdParam = Animator.StringToHash("Cheer");
        cryIdParam = Animator.StringToHash("Crying");
        walkIdParam = Animator.StringToHash("Walk");

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

    public void MoveToPosition(Vector3 _position)
    {
        personAnim.SetBool(walkIdParam, true);
        startTime = Time.time;
        
        initialPosition = transform.position;
        endPosition = _position;

        journeyLength = Vector3.Distance(initialPosition, endPosition);

        transform.LookAt(endPosition);
    }
}
