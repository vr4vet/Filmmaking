using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PyrotechnicsScreen : MonoBehaviour
{
    [SerializeField] private Image barImage;
    

    [SerializeField] private Color minBarColor;
    [SerializeField] private Color maxBarColor;
    private Color currentColor;
    [SerializeField] private float timeForMaxBar;

    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float maxForce;
    [SerializeField] private float maxParticles;

    private float currentFillAmount = 0;

    private float startTime;

    bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        barImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        PingPongBar();
    }

    private void PingPongBar()
    {
        float elapsedTime = Time.time - startTime;
        float time = Mathf.PingPong(elapsedTime / timeForMaxBar, 1);
        currentFillAmount = Mathf.Lerp(0f, 1f, time);
        currentColor = Color.Lerp(minBarColor, maxBarColor, currentFillAmount);
        barImage.color = currentColor;

        barImage.fillAmount = currentFillAmount;
    }

    public void ActivateScreen()
    {
        startTime = Time.time;
        currentFillAmount = 0f;
        barImage.gameObject.SetActive(true);
        isRunning = true;
    }

    public void DeactivateScreen()
    {
        barImage.gameObject.SetActive(false);
        isRunning = false;
    }

    public void Explode()
    {
        var main = explosion.main;
        main.startSpeed = currentFillAmount * maxForce;
        var emission = explosion.emission;
        emission.rateOverTime = currentFillAmount * maxParticles;
        explosion.Play();
    }

}
