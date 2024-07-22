using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Photon.Realtime;

public class FilmingGameManager : MonoBehaviour
{
    public static FilmingGameManager instance;
    public CharacterController player;
    public Transform finalRoomPlayerPoint;
    public UnityAction OnStartFilming;
    public UnityAction OnStopFilming;
    public float waitFilming = 5f;
    public Material playerFade;
    public bool startedFilming { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        playerFade.DOColor(new Color( playerFade.color.r, playerFade.color.g, playerFade.color.b,0),3);
    }
    public IEnumerator WaitFilming()
    {
        yield return new WaitForSeconds(waitFilming);
        StopFilming();
    }
    public void StopFilming()
    {
        OnStopFilming.Invoke();
        player.enabled = false;
        player.Move(finalRoomPlayerPoint.position);
        player.transform.position= finalRoomPlayerPoint.position;
        player.transform.forward = finalRoomPlayerPoint.forward;
        playerFade.DOColor(new Color(playerFade.color.r, playerFade.color.g, playerFade.color.b, 1), 3).OnComplete(() => { playerFade.DOColor(new Color(playerFade.color.r, playerFade.color.g, playerFade.color.b, 0), 3); });
    }
    public void StartFilming()
    {
        startedFilming=true;
        StartCoroutine(WaitFilming());
        OnStartFilming.Invoke();
    }
}
