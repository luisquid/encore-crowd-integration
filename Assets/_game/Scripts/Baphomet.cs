using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baphomet : MonoBehaviour
{
    private Animator baphoAnim;
    private int actionID;
    public void Start()
    {
        baphoAnim = GetComponent<Animator>();
        actionID = Animator.StringToHash("Action");
    }

    public void DoTheThing()
    {
        baphoAnim.SetTrigger(actionID);
    }
}
