using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LukeFalling : MonoBehaviour
{
    [SerializeField] private float _dealy;
    private Rigidbody2D _rb2D;
    [SerializeField] private float _gravityScale;
    private AudioSource _audioSource;

    void Start()
    {
        _rb2D ??= transform.GetComponent<Rigidbody2D>();
        _audioSource ??= transform.GetComponent<AudioSource>();

        _rb2D.gravityScale = 0f;
        _audioSource.PlayDelayed(_dealy);

        StartCoroutine(DelayFall());
    }

    private IEnumerator DelayFall()
    {
        yield return new WaitForSeconds(_dealy);

        _rb2D.gravityScale = _gravityScale;
    }
}
