using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }

    public void ShowFinalScore()
    {
        finalScore.text = "You scored " + scoreKeeper.CalculateScore() + "%";
    }
}
