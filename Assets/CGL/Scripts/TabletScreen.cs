using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletScreen : MonoBehaviour
{
    [SerializeField] private List<Sprite> tutorialImages = new List<Sprite>();
    [SerializeField] private Image tutorialImage;
    [SerializeField] private GameObject tutorialDisplay;
    [SerializeField] private GameObject sceneScriptPanel;

    private int imageIndex = 0;


    private void Start()
    {
        tutorialDisplay.SetActive(false);
    }
    public void NextImage()
    {
        imageIndex = (imageIndex + 1) % tutorialImages.Count;

        tutorialImage.sprite = tutorialImages[imageIndex];

    }

    public void PrevImage()
    {
        imageIndex = (imageIndex - 1 + tutorialImages.Count) % tutorialImages.Count;

        tutorialImage.sprite = tutorialImages[imageIndex];
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

    public void enableTutorial()
    {
            tutorialDisplay.SetActive(true);
    }
    public void disableTutorial()
    {
        tutorialDisplay.SetActive(false);
    }

    public void enableSceneScript()
    {
        sceneScriptPanel.SetActive(true);
    }

    public void disableSceneScript()
    {
        sceneScriptPanel.SetActive(false);
    }
}
