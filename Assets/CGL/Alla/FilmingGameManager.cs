using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilmingGameManager : MonoBehaviour
{
    public static FilmingGameManager instance;
    public GameObject player;
    public Transform finalRoomPlayerPoint;
    public UnityAction OnStartFilming;
    public UnityAction OnStopFilming;
    public float waitFilming = 5f;
    public bool startedFilming { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public IEnumerator WaitFilming()
    {
        yield return new WaitForSeconds(waitFilming);
        StopFilming();
    }
    public void StopFilming()
    {
        OnStopFilming.Invoke();
        player.transform.position = finalRoomPlayerPoint.position;
        player.transform.forward = finalRoomPlayerPoint.forward;
    }
    public void StartFilming()
    {
        startedFilming=true;
        StartCoroutine(WaitFilming());
        OnStartFilming.Invoke();
    }
}
