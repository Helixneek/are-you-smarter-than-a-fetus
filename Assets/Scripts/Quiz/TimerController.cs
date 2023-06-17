using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] float timeToFinishQuestion = 30f;
    [SerializeField] float timeToShowAnswer = 10f;
    // Current value of timer
    float timerValue;

    [Header("Dont modify")]
    // Fraction for timer (timer uses values 0 to 1)
    public float fillFraction;
    // Check if player is currently answering the question
    public bool isAnsweringQuestion = false;
    // Tells to load the next question
    public bool loadNextQuestion;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // If currently answering a question
        if(isAnsweringQuestion)
        {
            // Update timer
            if(timerValue > 0)
            {
                // The timer uses values to 0 to 1 for fill
                fillFraction = timerValue / timeToFinishQuestion;
            } else // Change to show answer
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowAnswer;
            }
        } else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowAnswer;
            } else
            {
                // Change to answer question and load the next question
                isAnsweringQuestion = true;
                timerValue = timeToFinishQuestion;
                loadNextQuestion = true;
            }
        }

        //Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
