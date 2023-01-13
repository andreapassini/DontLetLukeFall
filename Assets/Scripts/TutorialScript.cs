using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject _actionUI;
    [SerializeField] private GameObject _platformBar;
    [SerializeField] private Sprite[] _tutorialImages;
    [SerializeField] private Sprite _transparentImage;
    [SerializeField] private Image _imageForTutorial;
    [SerializeField] private Image _coverImageForTutorial;
    [SerializeField] private Text _textForTutorial;
    [SerializeField] private GameObject _imageForTutorialGameObject;
    [SerializeField] private GameObject _coverImageForTutorialGameObject;
    
    // Begin with the tutorial
    void Start()
    {
        _textForTutorial.text = "";
        StartCoroutine(part1());
    }

    IEnumerator part1()
    {
        _actionUI.SetActive(false);
        _platformBar.SetActive(false);
        _coverImageForTutorialGameObject.SetActive(true);
        _coverImageForTutorial.sprite = _transparentImage;
        yield return new WaitForSeconds(1);
        StartCoroutine(part2());
    }

    IEnumerator part2()
    {
        _textForTutorial.text = "Welcome in Don't let Luke fall!";
        yield return new WaitForSeconds(2);
        StartCoroutine(part3());
    }
    
    IEnumerator part3()
    {
        _textForTutorial.text = "";
        yield return new WaitForSeconds(1);
        StartCoroutine(part4());
    }
    
    IEnumerator part4()
    {
        _textForTutorial.text = "In this game Luke moves automatically";
        yield return new WaitForSeconds(2);
        StartCoroutine(part5());
    }
    
    IEnumerator part5()
    {
        _textForTutorial.text = "";
        yield return new WaitForSeconds(1);
        StartCoroutine(part6());
    }
    
    IEnumerator part6()
    {
        _actionUI.SetActive(true);
        _imageForTutorial.sprite = _tutorialImages[1];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "This is Action Ul, a bar where you can see witch action Luke will perform";
        yield return new WaitForSeconds(4);
        StartCoroutine(part7());
    }
    
    IEnumerator part7()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        yield return new WaitForSeconds(1);
        StartCoroutine(part8());
    }
    
    IEnumerator part8()
    {
        _imageForTutorial.sprite = _tutorialImages[1];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "And this one is the current Luke's action";
        yield return new WaitForSeconds(11);
        StartCoroutine(part9());
    }
    
    IEnumerator part9()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        yield return new WaitForSeconds(1);
        StartCoroutine(part10());
    }
    
    IEnumerator part10()
    {
        _platformBar.SetActive(true);
        _imageForTutorial.sprite = _tutorialImages[2];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "This is the platform bar, where you can find some platforms to drag";
        yield return new WaitForSeconds(4);
        StartCoroutine(part11());
    }
    
    IEnumerator part11()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(part12());
    }
    
    IEnumerator part12()
    {
        _imageForTutorial.sprite = _tutorialImages[2];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "Your objective is to drag platforms into the level to avoid Luke to fall and to bring him to the arrive!";
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(part13());
    }
    
    IEnumerator part13()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        _coverImageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(part14());
    }
    
    IEnumerator part14()
    {
        _imageForTutorial.sprite = _tutorialImages[3];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "Be careful! Here there is an hole, so why don't you try to drag now a platform to avoid Luke to fall?";
        yield return new WaitForSeconds(5.5f);
        StartCoroutine(part15());
    }
    
    IEnumerator part15()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        yield return new WaitForSeconds(4);
        StartCoroutine(part16());
    }
    
    IEnumerator part16()
    {
        _textForTutorial.text = "Perfect! Now let Luke to reach the door, witch is the arrive!";
        yield return new WaitForSeconds(5);
        StartCoroutine(part17());
    }
    
    IEnumerator part17()
    {
        _textForTutorial.text = "";
        yield return new WaitForSeconds(1);
        StartCoroutine(part18());
    }
    
    IEnumerator part18()
    {
        _imageForTutorial.sprite = _tutorialImages[4];
        _imageForTutorialGameObject.SetActive(true);
        _textForTutorial.text = "Be careful! If Luke performs this action, he will die!";
        yield return new WaitForSeconds(3);
        StartCoroutine(part19());
    }
    
    IEnumerator part19()
    {
        _imageForTutorialGameObject.SetActive(false);
        _textForTutorial.text = "";
        yield return new WaitForSeconds(0);
    }
    
}
