using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    [Header("Questions")]
    // Get question text
    [SerializeField] TextMeshProUGUI questionText;
    // Get question data
    [SerializeField] List<QuestionSO> questionDatas = new List<QuestionSO>();
    // Total amount of questions in each play
    [SerializeField] int questionsPerPlay; 
    // Get current question data
    QuestionSO currentQuestionData;

    [Header("Answers")]
    // Get answer button objects
    [SerializeField] GameObject[] answerButtons;
    // Keep track of correct answer index 
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Sprites")]
    // Sprites for answer box states
    [SerializeField] Sprite defaultAnswerSprite, correctAnswerSprite;
    
    [Header("Timer")]
    // Get timer image
    [SerializeField] Image timerImage;
    // Get timer controller script
    TimerController timer;

    [Header("Score")]
    // Get score text
    [SerializeField] TextMeshProUGUI scoreText;
    // Get score keeper script
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool isComplete = false;

    private void Awake()
    {
        // Find timer controller object
        timer = FindObjectOfType<TimerController>();
        // Find score keeper object
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        // Get max value of slider from total question count
        progressBar.maxValue = questionsPerPlay;
        progressBar.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswers(-1);
            SetButtonState(false);
        }
    }

    // Set question text and text on answers
    private void SetQuestionText()
    {
        // Set question text
        questionText.text = currentQuestionData.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionData.GetAnswer(i);
        }
    }
    
    // Change button state and sprite when an answer is chosen
    public void OnAnswerSelected(int index) 
    {
        hasAnsweredEarly = true;
        DisplayAnswers(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        //Debug.Log(scoreKeeper.CalculateScore());
    }

    // Change answer sprite and question text
    public void DisplayAnswers(int index)
    {
        Image buttonImage;

        if (index == currentQuestionData.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer!";
            // Show correct answer
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectQuestions();
            //Debug.Log("Correct questions: " + scoreKeeper.GetCorrectQuestions());
        }
        else
        {
            correctAnswerIndex = currentQuestionData.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestionData.GetAnswer(correctAnswerIndex);
            questionText.text = "Wrong answer!\n The correct answer is:\n" + correctAnswer;
            // Show correct answer
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    // Get the next question in line, reset button sprite, make button interactable again
    void GetNextQuestion()
    {
        if(questionDatas.Count > 0)
        {
            SetButtonState(true);
            ResetButtonSprites();
            GetRandomQuestion();
            SetQuestionText();

            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
            //Debug.Log("Current questions seen: " + scoreKeeper.GetQuestionsSeen());
        }
    }

    // Find random question from list
    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questionDatas.Count);
        currentQuestionData = questionDatas[index];
        
        if(questionDatas.Contains(currentQuestionData))
        {
            questionDatas.Remove(currentQuestionData);
        }
    }

    // Set button so it can be interacted or not
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void ResetButtonSprites()
    {
        Image buttonImage;

        for(int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
