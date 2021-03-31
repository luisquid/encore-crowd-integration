using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamScript : MonoBehaviour
{
    static WebCamTexture webCamTex;

    // Start is called before the first frame update
    void Start()
    {
        if (webCamTex == null)
        {
            webCamTex = new WebCamTexture();
            webCamTex.requestedWidth = 1280;
            webCamTex.requestedHeight = 720;
            webCamTex.requestedFPS = 60;
            GetComponent<MeshRenderer>().material.mainTexture = webCamTex;

            if (!webCamTex.isPlaying)
                webCamTex.Play();
        }
    }
}
