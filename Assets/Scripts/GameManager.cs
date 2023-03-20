using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{ 
    public UnityEvent onGameEnd;

    public UnityEvent onGameStart;

    private bool gameStarted = false;


    // Start is called before the first frame update
    public override void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        base.Awake();
    }

    public void Start() {
        Time.timeScale = 0f;
//        MusicManager.GetInstance().PlayMusic();
    }

    public void StartGame() {
        Time.timeScale = 1;
        gameStarted = true;

        onGameStart.Invoke();
    }

    public void EndGame() {
        Time.timeScale = 0f;
//        MusicManager.GetInstance().StopMusic();
        onGameEnd.Invoke();
    }
}
