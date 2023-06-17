using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Data", menuName = "Question SO")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string questionText = "Question text";

    [SerializeField] private string[] answerText = new string[4];
    [SerializeField] private int correctAnswerIndex;

    // To get question text
    public string GetQuestion()
    {
        return questionText;
    }

    // To get correct answer index
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    // To get answer text on specific index
    public string GetAnswer(int index)
    {
        return answerText[index];
    }
}
