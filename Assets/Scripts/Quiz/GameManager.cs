using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    QuizManager quiz;
    EndScreen endScreen;

    private void Awake()
    {
        quiz = FindObjectOfType<QuizManager>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
} 
