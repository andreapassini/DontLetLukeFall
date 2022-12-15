using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject _actionUI;
    [SerializeField] private GameObject _platformBar;
    [SerializeField] private Sprite[] _tutorialImages;
    [SerializeField] private Image _imageForTutorial;
    [SerializeField] private GameObject _imageForTutorialGameObject;
    
    // Begin with the tutorial
    void Start()
    {
        StartCoroutine(part1());
    }

    IEnumerator part1()
    {
        _actionUI.SetActive(false);
        _platformBar.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part2());
    }

    IEnumerator part2()
    {
        _imageForTutorial.sprite = _tutorialImages[0];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(part3());
    }
    
    IEnumerator part3()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part4());
    }
    
    IEnumerator part4()
    {
        _imageForTutorial.sprite = _tutorialImages[1];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(part5());
    }
    
    IEnumerator part5()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part6());
    }
    
    IEnumerator part6()
    {
        _actionUI.SetActive(true);
        _imageForTutorial.sprite = _tutorialImages[3];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(part7());
    }
    
    IEnumerator part7()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part8());
    }
    
    IEnumerator part8()
    {
        _imageForTutorial.sprite = _tutorialImages[4];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(12);
        StartCoroutine(part9());
    }
    
    IEnumerator part9()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part10());
    }
    
    IEnumerator part10()
    {
        _platformBar.SetActive(true);
        _imageForTutorial.sprite = _tutorialImages[5];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        StartCoroutine(part11());
    }
    
    IEnumerator part11()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part12());
    }
    
    IEnumerator part12()
    {
        _imageForTutorial.sprite = _tutorialImages[6];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        StartCoroutine(part13());
    }
    
    IEnumerator part13()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(part14());
    }
    
    IEnumerator part14()
    {
        _imageForTutorial.sprite = _tutorialImages[7];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        StartCoroutine(part15());
    }
    
    IEnumerator part15()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(4);
        StartCoroutine(part16());
    }
    
    IEnumerator part16()
    {
        _imageForTutorial.sprite = _tutorialImages[8];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        StartCoroutine(part17());
    }
    
    IEnumerator part17()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine(part18());
    }
    
    IEnumerator part18()
    {
        _imageForTutorial.sprite = _tutorialImages[9];
        _imageForTutorialGameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(part19());
    }
    
    IEnumerator part19()
    {
        _imageForTutorialGameObject.SetActive(false);
        yield return new WaitForSeconds(0);
    }
    
}
