using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmingGameManager : MonoBehaviour
{
    public static FilmingGameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(this);
    }
    public void StartFilming()
    {

    }
}
