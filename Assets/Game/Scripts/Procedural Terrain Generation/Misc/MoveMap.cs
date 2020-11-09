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
    [SerializeField] private float _viewDistance = 100f;

    private PlayerTest _player = null;
    private Dictionary<Vector2, MapGenerator> _maps = new Dictionary<Vector2, MapGenerator>();
    private int _chunkSize = 0;
    private List<TerrainPos> _terrains = new List<TerrainPos>();
    private List<TerrainPos> _temp = new List<TerrainPos>();

    private void Awake()
    {
        _chunkSize = MapGenerator.mapChunkSize - 1;
        InstantiateMaps();
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerTest>();

        if (_player != null)
        {
            _player.transform.position = _playerStartPos.position;
        }
    }

    private void InstantiateMaps()
    {
        GameObject mapToInstantiate;
        GameObject mapGeneratorToInstantiate;
        Vector3 posVector;
        Vector2 mapOffsetVector;
        TerrainPos terrainPos;

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
                mapToInstantiate.AddComponent<TerrainObj>();
                terrainPos.coordinates.x = posVector.x;
                terrainPos.coordinates.y = posVector.z;
                terrainPos.terrain = mapToInstantiate;
                _terrains.Add(terrainPos);
            }
        }
    }

    private void LateUpdate()
    {
        UpdateMap();
    }

    //Cache
    float _distance = 0f;
    float _dotProduct = 0f;
    public void UpdateMap()
    {
        if (_player == null) { return; }

        for (int i = 0; i < _terrains.Count; i++)
        {
            _distance = Vector3.Distance(_player.transform.position, _terrains[i].terrain.transform.position);
            _dotProduct = Vector3.Dot(_player.transform.position, _terrains[i].terrain.transform.position);


            if ((_distance <= _viewDistance && _dotProduct >= 0f) || _terrains[i].terrain == _player.CurrentTerrain.gameObject)
            {
                _terrains[i].terrain.SetActive(true);
            }
            else { _terrains[i].terrain.SetActive(false); }
        }
    }

    private void MoveTerrainChunks()
    {

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

    [System.Serializable]
    public struct TerrainPos
    {
        public Vector2 coordinates;
        public GameObject terrain;
    }
}
