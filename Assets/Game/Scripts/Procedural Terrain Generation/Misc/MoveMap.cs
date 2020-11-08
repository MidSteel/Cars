using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralTerrainGeneratio.Map;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private int _xSize = 1;
    [SerializeField] private int _ySize = 1;
    [SerializeField] private GameObject _mapObjPrefab = null;
    [SerializeField] private GameObject _mapGeneratorPrefab = null;
    [SerializeField] private Transform _mapsParentTransform = null;
    [SerializeField] private Transform _playerStartPos = null;

    private Dictionary<Vector2, MapGenerator> _maps= new Dictionary<Vector2, MapGenerator>();
    private int _chunkSize = 0;

    private void Awake()
    {
        _chunkSize = MapGenerator.mapChunkSize - 1;
        InstantiateMaps();
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.transform.position = _playerStartPos.position;
        }
    }

    private void InstantiateMaps()
    {
        GameObject mapToInstantiate;
        GameObject mapGeneratorToInstantiate; 
        Vector3 posVector;
        Vector2 mapOffsetVector;

        posVector.y = 0f;

        for (int y = -_ySize; y <= _ySize; y++)
        {
            for (int x = -_xSize; x <= _xSize; x++)
            {
                mapToInstantiate = Instantiate(_mapObjPrefab, _mapsParentTransform);
                posVector.x = x;
                posVector.z = -y;
                PositionateMap(posVector, mapToInstantiate);
                mapGeneratorToInstantiate = Instantiate(_mapGeneratorPrefab, _mapsParentTransform);
                mapOffsetVector.x = _chunkSize * x;
                mapOffsetVector.y = _chunkSize * y;
                mapGeneratorToInstantiate.GetComponent<MapGenerator>().Offset = mapOffsetVector;
                SetMapDisplay(mapToInstantiate, mapGeneratorToInstantiate);
            }
        }
    }

    private void PositionateMap(Vector3 pos, GameObject mapObj)
    {
        mapObj.transform.position = pos * _chunkSize;
    }

    private void SetMapDisplay(GameObject mapObj, GameObject mapGeneratorObj)
    {
        MapDisplay display = mapGeneratorObj.GetComponent<MapDisplay>();
        display.TextureRenderer = mapObj.GetComponent<Renderer>();
        display.MeshRendere = mapObj.GetComponent<MeshRenderer>();
        display.MeshFilter = mapObj.GetComponent<MeshFilter>();
        mapGeneratorObj.GetComponent<MapGenerator>().GenerateMap();
    }
}
