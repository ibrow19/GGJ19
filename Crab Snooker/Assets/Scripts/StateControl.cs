using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateControl : MonoBehaviour
{

    enum State
    {
        SETUP,
        START,
        PLAY,
        WIN,
        RESTART
    }

    // win conditions
    //bool blueWins 

    // crab instantiate
    public Crab redCrab;
    public Crab blueCrab;
    public MenuAppear menu;

    // setup sprite renderer
    private SpriteRenderer srenderer;

    // setup audio source
    private AudioSource audioSource;

    // audio sources
    public AudioClip setupSound;
    public AudioClip startSound;
    public AudioClip victorySound;
    public AudioClip backgroundMusic;

    // sprites
    public Sprite startSprite;
    public Sprite drawSprite;
    public Sprite redVictorySprite;
    public Sprite blueVictorySprite;

    // setup state and timer
    private State currentState = State.SETUP;
    private bool startPlayed;
    private float waitCount = 0;

    // wait times
    private const float setupTime = 3f;
    private const float startTime = 0.5f;
    private const float winTime = 5f;
    private const float restartTime = 30f;

    // start scene index
    private const int startScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        srenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        waitCount += Time.deltaTime;

        switch (currentState)
        {
            case State.SETUP:
                handleSetup();
                break;

            case State.START:
                handleStart();
                break;

            case State.PLAY:
                handlePlay();
                break;

            case State.WIN:
                handleWin();
                break;

            case State.RESTART:
                handleRestart();
                break;

        }

    }

    // Balls setup
    private void handleSetup()
    {
        if(!startPlayed)
        {
            //audioSource.PlayOneShot(setupSound, 1);
            startPlayed = true;
        }

        if (waitCount >= setupTime)
        {
            stateSelect(State.START);
            srenderer.enabled = true;
            srenderer.sprite = startSprite;
        }

    }

    // Play start sound and show start
    private void handleStart()
    {
        if (waitCount >= startTime)
        {
            stateSelect(State.PLAY);
            srenderer.enabled = false;
            redCrab.activate();
            blueCrab.activate();
        }

    }

    private void handlePlay()
    {
        if (!blueCrab.isAlive() && !redCrab.isAlive())
        {
            stateSelect(State.WIN);
            //audioSource.PlayOneShot(victorySound, 1);
            srenderer.enabled = true;
            srenderer.sprite = drawSprite;
        }

        if (!blueCrab.isAlive() && redCrab.isAlive())
        {
            stateSelect(State.WIN);
            //audioSource.PlayOneShot(victorySound, 1);
            srenderer.enabled = true;
            srenderer.sprite = redVictorySprite;
        }

        if (blueCrab.isAlive() && !redCrab.isAlive())
        {
            stateSelect(State.WIN);
            //audioSource.PlayOneShot(victorySound, 1);
            srenderer.enabled = true;
            srenderer.sprite = blueVictorySprite;
        }
    }

    private void handleWin()
    {
        if (waitCount >= winTime)
        {
            stateSelect(State.RESTART);
            srenderer.enabled = false;
            menu.activate();
        }
    }

    private void handleRestart()
    {
        if (waitCount >= restartTime)
        {
            SceneManager.LoadScene(startScene);
        }
    }

    private void stateSelect(State nextState)
    {
        currentState = nextState;
        waitCount = 0;
    }
}
