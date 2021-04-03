using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdPerson : MonoBehaviour
{
    public string personName;
    public float speed;

    int activateDanceParam;
    int danceIdParam;
    int cheerIdParam;
    int cryIdParam;
    int walkIdParam;
    int jumpIdParam;
    int vibeIdParam;
    int twerkIdParam;

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


    public void Initialize(string userName)
    {
        personAnim = GetComponent<Animator>();
        personNameText = GetComponentInChildren<TextMeshPro>();

        activateDanceParam = Animator.StringToHash("ActivateDance");
        danceIdParam = Animator.StringToHash("DanceId");
        cheerIdParam = Animator.StringToHash("Cheer");
        cryIdParam = Animator.StringToHash("Crying");
        walkIdParam = Animator.StringToHash("Walk");
        jumpIdParam = Animator.StringToHash("Jump");
        vibeIdParam = Animator.StringToHash("Vibe");
        twerkIdParam = Animator.StringToHash("Twerk");

        //AssignRandomDance();
        personName = userName;
        personNameText.text = personName; 
    }

    public void AssignRandomDance()
    {
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetTrigger(activateDanceParam);
        personAnim.SetInteger(danceIdParam, Random.Range(0, 5));
    }

    public void CheerStream()
    {
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetTrigger(cheerIdParam);
    }

    public void VibeStream()
    {
        personAnim.SetBool(vibeIdParam, true);
        StartCoroutine(WaitToStopVibing());
    }

    IEnumerator WaitToStopVibing()
    {
        yield return new WaitForSeconds(10f);
        personAnim.SetBool(vibeIdParam, false);
    }

    public void CryStream()
    {
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetTrigger(cryIdParam);
    }

    public void JumpStream()
    {
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetTrigger(jumpIdParam);
    }

    public void TwerkStream()
    {
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetTrigger(twerkIdParam);
    }

    public void MoveToPosition(Vector3 _position)
    {
        canMove = true;
        personAnim.SetBool(vibeIdParam, false);
        personAnim.SetBool(walkIdParam, true);
        startTime = Time.time;
        
        initialPosition = transform.position;
        endPosition = _position;

        journeyLength = Vector3.Distance(initialPosition, endPosition);

        transform.LookAt(endPosition);
    }
}
