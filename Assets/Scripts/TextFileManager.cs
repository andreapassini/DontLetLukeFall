using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileManager : MonoBehaviour
// This script is to log some infos to be attached in an even feedback
{

    private static bool _createdTheFile = false; // To avoid to recreate the file more times
    private static string _file = ""; // Content of the file
    
    private void CreateText()
    {
        _file = "";
        // Content of the file
        string content = "\n" + "Started to play date: " + System.DateTime.Now + "\n"; // When you started to play
        // Add some to text to it
        _file = _file + content;
    }
    
    // At the beginning create the text file
    private void Start()
    {
        if (!_createdTheFile)
        {
            CreateText();
            _createdTheFile = true;
        }
    }

    public static void AddDateFinishedToPlay() // To add in the log file when you finished to play
    // This function is called when you are going to send the feedback
    {
        // Content to add in the file
        string content = "Finished to play date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        _file = _file + content;
    }

    public static void AddWitchLevelYouStartPlaying() // To add in the log file witch level you start to play
    {
        // Content to add in the file
        int numberOfTheLevel = GameManager.Instance.GetLevelToPlay() - 1;
        string content = "Started level " + numberOfTheLevel + " in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        _file = _file + content;
    }

    public static void AddThatYouWonALevel() // To add in the log file that you won
    {
        // Content to add in the file
        string content = "Won the level in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        _file = _file + content;
    }
    
    public static void AddThatYouLostALevel() // To add in the log file that you lost
    {
        // Content to add in the file
        string content = "Lost the level in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        _file = _file + content;
    }

    public static string ReadFileContent() // To read the log file content
    {
        return _file;
    }

}
