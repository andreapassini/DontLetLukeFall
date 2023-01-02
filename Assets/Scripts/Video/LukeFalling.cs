using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class LukeFalling : MonoBehaviour
{
    [SerializeField] private float _dealy;
    [SerializeField] private float _dealyFeedbackSound;
    private Rigidbody2D _rb2D;
    [SerializeField] private float _gravityScale;
    private AudioSource _audioSource;
    private MMF_Player _fallingFeedback;

    void Start()
    {
        _rb2D ??= transform.GetComponent<Rigidbody2D>();
        _fallingFeedback = GetComponent<MMF_Player>();

        _rb2D.gravityScale = 0f;

        StartCoroutine(DelayFall());
        StartCoroutine(DealyFeedback());
    }

    private IEnumerator DelayFall()
    {
        yield return new WaitForSeconds(_dealy);

        _rb2D.gravityScale = _gravityScale;
    }

    private IEnumerator DealyFeedback()
    {
        yield return new WaitForSeconds(_dealyFeedbackSound);

        _fallingFeedback ??= GetComponent<MMF_Player>();
        _fallingFeedback.PlayFeedbacks();
    }
}
