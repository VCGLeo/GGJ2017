﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotographerController : MonoBehaviour {

    public float introTime = 2f;
    public float waitTime = 5f;
    public float photoFreeze = 3f;
    private float remainingTime;
    public string nextScene = "Test Scene";
    public Text countdownUI;
    public AudioSource clock;
    private SpriteRenderer Overlay;
    public Object speechBaloon;
    private bool speech = false;
    public VictoryCondition victoryScript;

    public GameObject flash;

    private bool hasFlashed;

	// Use this for initialization
	void Start () {
        remainingTime = waitTime + introTime;
        GlobalVariables.canMove = false;
        flash.SetActive(false);
        Overlay = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        remainingTime -= Time.deltaTime;
        if(countdownUI) countdownUI.text = Mathf.Round(remainingTime).ToString();
        if (remainingTime < waitTime && remainingTime > 0)
        {
            //Allow movement
            hasFlashed = false;
            GlobalVariables.canMove = true;
            if (!clock.isPlaying)
            {
                clock.Play();
            }
        }
        if (remainingTime < 0 && !hasFlashed)
        {
            //Snap photo
            if (flash) flash.SetActive(true);
            GlobalVariables.canMove = false;
            countdownUI.text = "";
            clock.Stop();
            clock.time = 0.0f;
            speech = false;
            Overlay.color = new Color(1f, 1f, 1f, 0f);
            
        }

        if (remainingTime < -photoFreeze / 2)
        {
            Debug.Log("werkt misschien");
            if (victoryScript.isVictorious2())
            { 
                Instantiate(speechBaloon);
                Debug.Log("werkt");
            }
        }

        if (remainingTime < -photoFreeze)
        {
            //Transition to next scene
            if (Config.savePhotos) Application.CaptureScreenshot(SceneManager.GetActiveScene().name + ".png");
            SceneManager.LoadScene(nextScene);
            Overlay.color = new Color(1f, 1f, 1f, 1f);
        }
	}
}
