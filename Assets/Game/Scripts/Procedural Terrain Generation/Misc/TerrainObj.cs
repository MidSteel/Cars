using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObj : MonoBehaviour
{
    private MoveMap _moveMap = null;

    private void Awake()
    {
        _moveMap = GameObject.FindObjectOfType<MoveMap>();
    }
}
