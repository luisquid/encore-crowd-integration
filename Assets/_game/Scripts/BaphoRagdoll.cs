using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaphoRagdoll : MonoBehaviour
{
    public float secondsToDie;
    private WaitForSeconds seconds;

    void Start()
    {
        seconds = new WaitForSeconds(secondsToDie);
        StartCoroutine(WaitToDie());
    }

    IEnumerator WaitToDie()
    {
        yield return seconds;

        Destroy(gameObject);
    }
}
