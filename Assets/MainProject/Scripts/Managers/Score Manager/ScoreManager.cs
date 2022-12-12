using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;
    private bool gameOver;
    private int currentScore = 0;
    //private int topScore = 0;


    [SerializeField] private List<AudioSource> treasureSounds; //Tamir Added
    private int treasuresPickedUp = 0;


    [Header("UI Elements")]
    public TextMeshProUGUI HUDScoreText;
    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI loseScoreText;
    //public Text topScoreText;

    private static ScoreManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(_instance);
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        gameOver = false;
        //topScore = PlayerPrefs.GetInt("TopScore", 0);

        if (winScoreText && HUDScoreText && loseScoreText)
        {
            UpdateScore();
        }

        /*if (topScoreText)
        {
            topScoreText.text = topScore.ToString();
        }
        else { Debug.Log("Top Score Is: " + topScore); }
        */

    }

    public void AddScore(int addedScore)
    {
        _score = _score + addedScore;

        _scoreText.text = _score.ToString();





        Debug.Log("Current score: " + _score);

    }

    public void AddToScore(int scoreToAdd)
    {
        if (gameOver)
        { return; }

        _instance.currentScore += scoreToAdd;
        if (_instance.currentScore < 0)
        {
            _instance.currentScore = 0;
        }

        treasuresPickedUp++; // Tamir Added
        if (treasuresPickedUp >= Random.Range(5, 10))
        {
            treasuresPickedUp = 0;
            AudioSource random = treasureSounds[Random.Range(0, treasureSounds.Count)];
            random.Play();
        }

        UpdateScore();

    }

    public void UpdateScore()
    {
        if (winScoreText)
        {
            winScoreText.text = _instance.currentScore.ToString();
        }
        if (loseScoreText)
        {
            loseScoreText.text = _instance.currentScore.ToString();
        }
        if (HUDScoreText)
        {
            HUDScoreText.text = _instance.currentScore.ToString();
        }
        else
        {
            Debug.Log("Current Score is : " + _instance.currentScore);
        }
    }

    public static int GetCurrentScore()
    {
        return _instance.currentScore;
    }

}
