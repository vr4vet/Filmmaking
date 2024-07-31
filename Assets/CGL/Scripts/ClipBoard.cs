using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClipBoard : MonoBehaviour
{
    private bool isSigned;
    [SerializeField] private DoorKnocker door;
    [SerializeField] private float stampResetCooldown;
    [SerializeField] private GameObject stampPlane;
    [SerializeField] private HVRSocket stampSocket;
    [SerializeField] private GameObject stampPrefab;
    [SerializeField] private Material beforeStampMaterial;
    [SerializeField] private Material stampedMaterial;


    // Start is called before the first frame update
    void Start()
    {
        SpawnStampAtSocket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSigned) return;
        if (other.gameObject.CompareTag("stamp"))
        {
            isSigned = true;
            Stamp();
        }
    }

    private void Stamp()
    {
        stampPlane.GetComponent<Renderer>().material = stampedMaterial;
        StartCoroutine(ResetStamp());
        door.ConfirmDelivery();
    }

    private IEnumerator ResetStamp()
    {
        yield return new WaitForSeconds(stampResetCooldown);
        isSigned = false;
        stampPlane.GetComponent<Renderer>().material = beforeStampMaterial;
        if (!stampSocket.CanAddGrabbable)
        {
            SpawnStampAtSocket();
        }
    }

    private void SpawnStampAtSocket()
    {
        GameObject stampInstance = Instantiate(stampPrefab);
        stampInstance.GetComponent<HVRSocketLink>().Socket = stampSocket;
        stampInstance.GetComponent<HVRSocketLink>().Setup();
    }
}
