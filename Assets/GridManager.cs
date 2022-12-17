using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gridCellPrefab;
    [SerializeField]
    private int _sizeGridX;
    [SerializeField]
    private int _sizeGridY;
    // Start is called before the first frame update
    void Start()
    {
        int startPositionX = (int)transform.position.x;
        int startPositionY = (int)transform.position.y;
        for (int i = 0; i < _sizeGridX; i++)
        {
            for(int j = 0; j < _sizeGridY; j++)
            {
                GameObject gridCell = Instantiate<GameObject>(_gridCellPrefab);
                gridCell.transform.position = new Vector3(startPositionX+i, startPositionY+j, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
