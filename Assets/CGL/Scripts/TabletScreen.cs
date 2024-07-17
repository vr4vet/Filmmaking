using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletScreen : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private Image display;

    private int imageIndex = 0;

    public void NextImage()
    {
        imageIndex = (imageIndex + 1) % images.Count;

        display.sprite = images[imageIndex];

    }

    public void PrevImage()
    {
        imageIndex = (imageIndex - 1 + images.Count) % images.Count;

        display.sprite = images[imageIndex];
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PrevImage();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            NextImage();
        }
    }
}
