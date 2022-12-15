using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject _actionUI;
    [SerializeField] private GameObject _platformBar;
    [SerializeField] private Sprite[] _tutorialImages;
    
    // Begin with the tutorial
    void Start()
    {
        StartCoroutine(part1());
    }

    IEnumerator part1()
    {
        _actionUI.SetActive(false);
        _platformBar.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine(part2());
    }

    IEnumerator part2()
    {
        _actionUI.SetActive(true);
        _platformBar.SetActive(true);
        yield return new WaitForSeconds(2);
    }
    
}
