using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Map
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private Renderer _textureRenderer = null;
        [SerializeField] private MeshFilter _meshFilter = null;
        [SerializeField] private MeshRenderer _meshRenderer = null;

        private MeshCollider _meshCollider = null;

        public Renderer TextureRenderer { get { return _textureRenderer; } set { _textureRenderer = value; } }
        public MeshFilter MeshFilter { get { return _meshFilter; } set { _meshFilter = value; } }
        public MeshRenderer MeshRendere { get { return _meshRenderer; } set { _meshRenderer = value; } }


        //Cache
        private int _drawMapWidth;
        private int _drawMapHeight;
        private Texture2D _drawMapTexture;
        private Color[] _drawMapColor;

        /// <summary>
        /// Draws the map.
        /// </summary>
        public void DrawTexture(Texture2D texture) 
        {
            //_textureRenderer.sharedMaterial.mainTexture = texture;
            //_textureRenderer.transform.localScale = new Vector3(texture.width, 1f, texture.height);
        }

        /// <summary>
        /// Draws Mesh.
        /// </summary>
        public void DrawMesh(MeshData meshData, Texture2D texture)
        {
            if (_meshFilter == null) { return; }
            _meshFilter.sharedMesh = meshData.CreateMesh();
            //_meshRenderer.sharedMaterial.mainTexture = texture;
            _textureRenderer.material.mainTexture = texture;
            if (_meshCollider == null) { _meshCollider = _meshFilter.GetComponent<MeshCollider>(); }
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }
    }
}