using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TriggerId
{
    Trigger_Cheer,
    Trigger_Twerk,
    Trigger_Jump,
    Trigger_Vibe,
    Trigger_Dead,
    Trigger_Crying,
    Trigger_Dancing
};

public class CrowdPerson : MonoBehaviour
{
    public string personId;
    public string personName;
    public float speed;

    int activateAnimationParam;
    int danceIdParam;
    int triggerIdParam;

    int walkIdParam;

    private TextMeshPro personNameText;
    private Animator personAnim;
    
    private float journeyLength;
    private float startTime;

    private Vector3 initialPosition;
    private Vector3 endPosition;

    private bool canMove = false;

    private void Update()
    {
        if(canMove && Vector3.Distance(endPosition, transform.position) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(initialPosition, endPosition, fractionOfJourney);
        }
        
        else
        {
            canMove = false;
            transform.rotation = Quaternion.identity;
            personAnim.SetBool(walkIdParam, false);
        }
    }


    public void Initialize(string userName, string userId)
    {
        personAnim = GetComponent<Animator>();
        personNameText = GetComponentInChildren<TextMeshPro>();

        //CURRENT
        activateAnimationParam = Animator.StringToHash("TriggerAnimation");
        triggerIdParam = Animator.StringToHash("TriggerId");
        danceIdParam = Animator.StringToHash("DanceId");
        //

        walkIdParam = Animator.StringToHash("Walk");

        personId = userId;
        personName = userName;
        personNameText.text = personName; 
    }

    public void TriggerAnimation(TriggerId _animationId)
    {
        int randomDanceId = Random.Range(0, 6);
        print("Random Dance Id: " + randomDanceId);
        personAnim.SetInteger(danceIdParam, randomDanceId);
        personAnim.SetTrigger(activateAnimationParam);
        personAnim.SetInteger(triggerIdParam, (int)_animationId);
    }

    public void MoveToPosition(Vector3 _position)
    {
        canMove = true;
        personAnim.SetBool(walkIdParam, true);
        startTime = Time.time;
        
        initialPosition = transform.position;
        endPosition = _position;

        journeyLength = Vector3.Distance(initialPosition, endPosition);

        transform.LookAt(endPosition);
    }
}
