using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendFeedbackScript : MonoBehaviour
{
    
    [SerializeField] private InputField _feedbackInputField; // Input field to insert the feedback
    [SerializeField] private GameObject _feedbackInputGameObject; // GameObject of the input field
    [SerializeField] private GameObject _feedbackSentGameObject; // Background of the text you sent the feedback
    [SerializeField] private Text _feedbackSentText; // Text you are sending the feedback / you have sent the feedback
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private bool _sendToOurForm; // If enabled the feedback is sent to our form
    [SerializeField] private bool _sendToProfessorForm; // If enabled the feedback is sent to professor's form
    
    private const string _videogameName = "DontLetLukeFall";
    
    IEnumerator SendFeedback() // Send feedback (this coroutine is activated when pushing return key)
    {
        _feedbackInputGameObject.SetActive(false);
        _feedbackSentGameObject.SetActive(true);
        if (_feedbackInputField.text.Length <= 1) // If empty feedback, operation cancelled
        {
            _feedbackSentText.text = "Operation cancelled";
            yield return new WaitForSeconds(1);
        }
        else
        { // Sending feedback
            _feedbackSentText.text = "Sending feedback...";
            yield return new WaitForSeconds(1);
            string feedback = _feedbackInputField.text;
            // string feedback will finish with an \n
            // string feedback to be adjusted with played infos TODO
            if (_sendToOurForm)
            {
                StartCoroutine(SendFeedbackToOurForm(_videogameName ,feedback));
            }
            if (_sendToProfessorForm)
            {
                StartCoroutine(SendFeedbackToProfessorForm(_videogameName ,feedback));
            }
            yield return new WaitForSeconds(1);
            _feedbackSentText.text = "Feedback sent!";
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("MainMenu"); // At the end return to main menu
    }

    IEnumerator SendFeedbackToOurForm(string videogame_name, string feedback) // Send feedback to our form
    {
        // Link to respond Google Form: https://docs.google.com/forms/d/e/1FAIpQLScOe-Kb8U0PrliETIQkI43tBiiwdRf61EBB6dl7CeANw7w0gg/viewform?usp=sf_link
        // field 1: 1682253545
        // field 2: 590915145
        // Example response via url: https://docs.google.com/forms/d/e/1FAIpQLScOe-Kb8U0PrliETIQkI43tBiiwdRf61EBB6dl7CeANw7w0gg/viewform?usp=pp_url&entry.1682253545=Simple+Game&entry.590915145=Very%0AGood!
        // visualize responses editor Google Sheet: https://docs.google.com/spreadsheets/d/1aDyEhqWwoNepSgifVOxi3JP_LWm6eYxd_c46bRJ6Msw/edit?usp=sharing
        
        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLScOe-Kb8U0PrliETIQkI43tBiiwdRf61EBB6dl7CeANw7w0gg/formResponse";
        
        WWWForm form = new WWWForm();

        form.AddField("entry.1682253545", videogame_name);
        form.AddField("entry.590915145", feedback);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    
    IEnumerator SendFeedbackToProfessorForm(string videogame_name, string feedback) // Send feedback to professor's form
    {
        // https://docs.google.com/forms/d/e/1FAIpQLSdyQkpRLzqRzADYlLhlGJHwhbKZvKJILo6vGmMfSePJQqlZxA/viewform?usp=pp_url&entry.631493581=Simple+Game&entry.1313960569=Very%0AGood!

        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLSdyQkpRLzqRzADYlLhlGJHwhbKZvKJILo6vGmMfSePJQqlZxA/formResponse";
        
        WWWForm form = new WWWForm();

        form.AddField("entry.631493581", videogame_name);
        form.AddField("entry.1313960569", feedback);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Send a feedback when pushing return key
        {
            StartCoroutine(SendFeedback());
        }
        if (!_eventSystem.currentSelectedGameObject) // Select automatically the input field
        {
            _eventSystem.SetSelectedGameObject(_feedbackInputGameObject);
        }
    }
    
}
