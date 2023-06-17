using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctQuestions, questionsSeen;

    public int GetCorrectQuestions()
    {
        return correctQuestions;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementCorrectQuestions()
    {
        correctQuestions++;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctQuestions / (float)questionsSeen * 100);
    }
}
