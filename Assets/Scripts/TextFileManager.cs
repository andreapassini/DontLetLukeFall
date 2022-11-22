using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileManager : MonoBehaviour
// This script is to create a txt file witch is used to log some infos to be attached in an even feedback
{

    private const string _fileName = "Log.txt";
    private static bool _createdTheFile = false;
    
    private void CreateText()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // Create file if doesn't exists
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }
        // Empty the file
        File.WriteAllText(path, "");
        // Content of the file
        string content = "\n" + "Started to play date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        File.AppendAllText(path, content);
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

    public static void AddDateFinishedToPlay()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // Content to add in the file
        string content = "Finished to play date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        File.AppendAllText(path, content);
    }

    public static void AddWitchLevelYouStartPlaying()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // Content to add in the file
        int numberOfTheLevel = GameManager.Instance.GetLevelToPlay();
        string content = "Started level " + numberOfTheLevel + " in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        File.AppendAllText(path, content);
    }

    public static void AddThatYouWonALevel()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // Content to add in the file
        string content = "Won the level in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        File.AppendAllText(path, content);
    }
    
    public static void AddThatYouLostALevel()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // Content to add in the file
        string content = "Lost the level in date: " + System.DateTime.Now + "\n";
        // Add some to text to it
        File.AppendAllText(path, content);
    }

    public static string ReadFileContent()
    {
        // Path of the file
        string path = Application.dataPath + "/" + _fileName;
        // StreamReader to read the file
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        string fileContent = reader.ReadToEnd();
        reader.Close();
        return fileContent;
    }

}
