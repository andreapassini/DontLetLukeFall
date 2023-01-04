using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPath : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> Path;
    private float time = 0;
    private int actualTarget=0;
    private int nextTarget=1;
    private Vector2 offset;
    [SerializeField]
    private float speed = 0.25f;
    private void Awake()
    {
        offset = transform.position - (Vector3)Path[0];
    }
    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime * speed * speed / Vector2.Distance(Path[actualTarget],Path[nextTarget]);
        if (time < 1)
        {
            transform.position = Vector3.Lerp(Path[actualTarget] + offset, Path[nextTarget] + offset, time);
        }
        else
        {
            time = 0;
            actualTarget = actualTarget != Path.Count-1 ? actualTarget + 1 : 0;
            nextTarget = nextTarget != Path.Count-1 ? nextTarget + 1 : 0;
        }
        
    }
}
