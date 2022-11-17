using DLLF;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelEndScript : MonoBehaviour
{
    public enum ExitState
    {
        Completed,
        Failed
    }

    [SerializeField] private ExitState _exitState;
    
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //Player has finished level
            string eventToTrigger = _exitState == ExitState.Failed
                ? LevelManager.OnLevelFailedEventName
                : LevelManager.OnLevelCompletedEventName;
            EventManager.TriggerEvent(eventToTrigger);
        }
    }
}
