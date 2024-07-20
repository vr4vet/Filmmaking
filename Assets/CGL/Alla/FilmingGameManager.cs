using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FilmingGameManager : MonoBehaviour
{
    public static FilmingGameManager instance;

    public UnityAction OnStartFilming;
    public UnityAction OnStopFilming;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(this);
    }
  
    public void StopFilming()
    {
        OnStopFilming.Invoke();
    }
    public void StartFilming()
    {
        OnStartFilming.Invoke();
    }
}
