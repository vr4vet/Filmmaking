using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketOnStart : MonoBehaviour
{

    public HVRSocket socket;
    public HVRGrabbable grabble;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Socket());
    }
    IEnumerator Socket()
    {
        yield return new WaitForEndOfFrame();
        socket.Attach(grabble);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
