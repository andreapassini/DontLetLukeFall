using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLevelJustToTestGameManagerScript : MonoBehaviour
{ 
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            GameManager.Instance.UpdateGameState(GameState.Win);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }
    
}
