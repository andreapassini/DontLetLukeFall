using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptForTransitionsBetweenMenuScenes : MonoBehaviour
{
    
    public void AnimationEndTransitionBetweenMenuScenes(string nextScene)
    {
        StartCoroutine(LoadNextScene(nextScene));
    }

    IEnumerator LoadNextScene(string nextScene)
    {
        gameObject.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(0.75f); // Do the animation of the transition
        SceneManager.LoadScene(nextScene);
    }
    
}
