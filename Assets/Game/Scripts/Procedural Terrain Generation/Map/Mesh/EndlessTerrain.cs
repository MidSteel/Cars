using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Map
{
    public class EndlessTerrain : MonoBehaviour
    {
        public static Vector2 viewerPosition = Vector2.zero;

        [SerializeField] private float _maxViewDistance = 300f;
        [SerializeField] private Transform _viewer = null;

        private int _chunkSize = 0;
        private int _chunksVisibleInViewDistance = 0;

        private void Start()
        {
            _chunkSize = MapGenerator.mapChunkSize - 1;
            _chunksVisibleInViewDistance = Mathf.RoundToInt(_maxViewDistance / _chunkSize);

        }
    }
}