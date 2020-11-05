using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapDisplay _mapDisplay = null;
        [SerializeField] [Range(0, 6)] private int _levelOfDetail = 1;
        [SerializeField] private int _octaves = 3;
        [SerializeField] private float _persistance = 0.3f;
        [SerializeField] private float _lacunarity = 2f;
        [SerializeField] private float _noiseScale = 0.3f;
        [SerializeField] private float _meshHeightMultiplier = 0f;
        [SerializeField] private int _seed = 0;
        [SerializeField] private Vector2 _offset = Vector2.zero;
        [SerializeField] private TerrainType[] _regions = null;
        [SerializeField] private AnimationCurve _meshHeightCurve = null;

        private const int _mapChunkSize = 241;

        //Cache
        private float[,] _noiseMap;
        private Color[] _colorMap;

        [ContextMenu("Generate Map")]
        private void GenerateMap()
        {
            _noiseMap = Noise.Noise.GenerateNoiseMap(_mapChunkSize, _mapChunkSize, _seed, _noiseScale, _octaves, _persistance, _lacunarity, _offset);
            _colorMap = new Color[_mapChunkSize * _mapChunkSize];

            for (int y = 0; y < _mapChunkSize; y++)
            {
                for (int x = 0; x < _mapChunkSize; x++)
                {
                    for (int i = 0; i < _regions.Length; i++)
                    {
                        if (_noiseMap[x, y] <= _regions[i].height)
                        {
                            _colorMap[y * _mapChunkSize + x] = _regions[i].color;
                            break;
                        }
                    }
                }
            }

            _mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(_noiseMap, _meshHeightMultiplier, _meshHeightCurve, _levelOfDetail), TextureGenerator.TextureFromColorMap(_colorMap, _mapChunkSize, _mapChunkSize));
        }

        private void OnValidate()
        {
            GenerateMap();
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}