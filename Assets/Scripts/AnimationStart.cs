using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStart : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetComponent<Animator>();

        StartCoroutine(ChangeAnimationAtRandom());
    }

    private IEnumerator ChangeAnimationAtRandom()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 5.3f));

            _animator.SetTrigger("ChangeAnimation");
        }
    }
}
