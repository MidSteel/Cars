using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Noise
{
    public static class Noise
    {
        //Cache
        private static float[,] _noiseMap;
        private static float _sampleX;
        private static float _sampleY;
        private static float _perlinValue;
        private static float _amplitude;
        private static float _frequncy;
        private static float _noiseHeight;
        private static float _maxNoiseHeight;
        private static float _minNosieHeight;
        private static System.Random _prng;
        private static Vector2[] _octaveOffsets;
        private static float _offsetX;
        private static float _offsetY;
        private static float _maxPossibleHeight;

        /// <returns>
        /// returns randomly Generated noiseMap.
        /// </returns>
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
            _maxPossibleHeight = 0f;
            _noiseMap = new float[mapWidth, mapHeight];
            _prng = new System.Random(seed);
            _octaveOffsets = new Vector2[octaves];

            for (int i = 0; i < octaves; i++)
            {
                _offsetX = _prng.Next(-100000, 100000) + offset.x;
                _offsetY = _prng.Next(-100000, 100000) + offset.y;
                _octaveOffsets[i].x = _offsetX;
                _octaveOffsets[i].y = _offsetY; 
                _maxPossibleHeight += _amplitude;
                _amplitude *= persistance;
            }

            scale = (scale <= 0) ? 0.0001f : scale;
            _minNosieHeight = float.MaxValue;
            _maxNoiseHeight = float.MinValue;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    _amplitude = 1f;
                    _frequncy = 1f;
                    _noiseHeight = 0f;

                    for (int i = 0; i < octaves; i++)
                    {
                        _sampleX = (x - (mapWidth / 2f) * _frequncy + _octaveOffsets[i].x) / scale;
                        _sampleY = (y - (mapWidth / 2f) * _frequncy + _octaveOffsets[i].y) / scale;

                        _perlinValue = Mathf.PerlinNoise(_sampleX, _sampleY) * 2f - 1f;
                        _noiseHeight += _perlinValue * _amplitude;
                        _amplitude *= persistance;
                        _frequncy *= lacunarity;
                    }

                    if  (_noiseHeight > _maxNoiseHeight) { _maxNoiseHeight = _noiseHeight; }
                    else if (_noiseHeight < _minNosieHeight) { _minNosieHeight = _noiseHeight; }

                    _noiseMap[x, y] = _noiseHeight;
                }
            }

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0;  x < mapWidth; x++)
                {
                    //_noiseMap[x, y] = Mathf.InverseLerp(_minNosieHeight, _maxNoiseHeight, _noiseMap[x, y]);
                    _noiseMap[x, y] = (_noiseMap[x, y] + 1) / (2f * _maxPossibleHeight / 1.5f);
                }
            }

            return _noiseMap;
        }
    }
}