using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public Health healthBar;
    public GameObject startScreen;
    public AudioSource music;
    public AudioSource siren;
    public int HighScore { get; set; } = 0;
    public TextMeshProUGUI highScoreUI;
    public GameObject gameOverScreen;

    static Game instance = null;

    float score = 0.0f;

    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    public eState State { get; set; } = eState.Title;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        switch (State)
        {
            case eState.Title:
                startScreen.SetActive(true);
                gameOverScreen.SetActive(false);
                break;
            case eState.StartGame:
                score = 0;
                music.Play();
                startScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                State = eState.Game;
                break;
            case eState.Game:
               score += Time.deltaTime;
               scoreUI.text = string.Format("{0:D2}", (int)score);
                break;
            case eState.GameOver:
                music.Stop();
                siren.Stop();
                startScreen.SetActive(false);
                gameOverScreen.SetActive(true);
                if ((int)score > HighScore)
                {
                    HighScore = (int)score;
                    highScoreUI.text = string.Format("{0:D4}", HighScore);
                }
                break;
            default:
                break;
        }
    }

    public static Game Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddPoints(int points)
    {
/*        Score += points;
        scoreUI.text = string.Format("{0:D4}", Score);*/
    }

    public void StartGame()
    {
        State = eState.StartGame;
    }
}
