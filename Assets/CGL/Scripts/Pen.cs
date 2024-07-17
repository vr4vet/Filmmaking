using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    [SerializeField] private GameObject penTipPrefab;
    [SerializeField] private Transform penTipTransform;

    private GameObject penTip;
    // Start is called before the first frame update
    void Start()
    {
        penTip = Instantiate(penTipPrefab, penTipTransform.position, penTipTransform.rotation);
    }
    private void Update()
    {
        penTip.transform.position = penTipTransform.position;
        penTip.transform.rotation = penTipTransform.rotation;
    }

}
