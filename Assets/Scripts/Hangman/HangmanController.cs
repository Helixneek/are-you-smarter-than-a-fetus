using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Random = UnityEngine.Random;

public class HangmanController : MonoBehaviour
{
    public TextMeshProUGUI textToFind;
    //public TextAsset wordBankFile = Resources.Load("Assets/Resources/WordBank.txt") as TextAsset;
    public GameObject[] hangmanObject;
    public GameObject winScreen;
    public GameObject loseScreen;

    private string[] wordBank = File.ReadAllLines(Application.streamingAssetsPath + "/wordBank.txt");
    //private string[] wordBank;
    private string chosenWord, hiddenWord;
    private int wrongTries = 0;
    private bool gameEnd = false;

    private void Start()
    {
        //ReadFile();
        gameEnd = false;
        GetRandomWord();
    }

    //public void ReadFile()
    //{
    //    try
    //    {
    //        string path = Application.dataPath + "/Resources/wordBank.txt";

    //        // Create an instance of StreamReader to read from a file.
    //        // The using statement also closes the StreamReader.
    //        using (StreamReader sr = new StreamReader(path))
    //        {
    //            int i = 0;
    //            string line;
    //            // Read and display lines from the file until the end of
    //            // the file is reached.
    //            while ((line = sr.ReadLine()) != null)
    //            {
    //                wordBank[i] = line;
    //                i++;
    //            }
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        // Let the user know what went wrong.
    //        Debug.Log("The file could not be read:");
    //        Debug.Log(e.Message);
    //    }
    //}

    private void GetRandomWord()
    {
        // Randomize and chose from bank
        int index = Random.Range(0, wordBank.Length);
        chosenWord = wordBank[index];
        Debug.Log(chosenWord);

        // Print dashes to guess
        PrintPlaceForText();
    }

    private void PrintPlaceForText()
    {
        textToFind.text = "";

        for(int i=0; i < chosenWord.Length; i++)
        {
            if(char.IsWhiteSpace(chosenWord[i]))
            {
                hiddenWord += " ";
            } else
            {
                hiddenWord += "_";
            }
        }

        textToFind.text = hiddenWord;
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && !gameEnd)
        {
            string pressedKey = e.keyCode.ToString();
            Debug.Log("key pressed" + pressedKey);

            if (chosenWord.Contains(pressedKey))
            {
                int i = chosenWord.IndexOf(pressedKey);

                while (i != -1)
                {
                    // replace the _ in hidden word to the letter
                    hiddenWord = hiddenWord.Substring(0, i) + pressedKey + hiddenWord.Substring(i + 1);

                    // for some reason, the tutorial wants to swap between both of em, i guess its to detect completion later lol
                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);

                    i = chosenWord.IndexOf(pressedKey);
                }

                textToFind.text = hiddenWord;

            }

            // Adding the hangman body parts
            else
            {
                hangmanObject[wrongTries].SetActive(true);
                wrongTries++;
            }

            // Lose game
            if(wrongTries == hangmanObject.Length)
            {
                loseScreen.SetActive(true);
                gameEnd = true;
            }

            // Win game
            if(!hiddenWord.Contains("_"))
            {
                winScreen.SetActive(true);
                gameEnd = true;
            }
        }
    }

}
