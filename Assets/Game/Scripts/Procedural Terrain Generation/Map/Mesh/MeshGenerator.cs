using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Map
{
    public static class MeshGenerator
    {
        //Cache
        private static int _width;
        private static int _height;
        private static int _vertexIndex;
        private static float _topLeftX;
        private static float _topLeftZ;
        private static int _meshSimplificationIncrement;
        private static int _verteciesPerLine;

        public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int lod)
        {
            _width = heightMap.GetLength(0);
            _height = heightMap.GetLength(1);
            _vertexIndex = 0;
            _topLeftX = (_width - 1) / -2f;
            _topLeftZ = (_height - 1) / 2f;
            _meshSimplificationIncrement = ((lod > 0) ? lod * 2 : 1);
            _verteciesPerLine = (_width - 1) / _meshSimplificationIncrement + 1;
            MeshData meshData = new MeshData(_verteciesPerLine, _verteciesPerLine);

            for (int y = 0;  y < _height; y += _meshSimplificationIncrement)
            {
                for (int x = 0; x < _width; x += _meshSimplificationIncrement)
                {
                    meshData.vertecies[_vertexIndex] = new Vector3(_topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, _topLeftZ - y);
                    meshData.uvs[_vertexIndex] = new Vector2(x / (float)_width, y / (float)_height);

                    if (x < _width - 1 && y < _height - 1)
                    {
                        meshData.AddTriangle(_vertexIndex, _vertexIndex + _verteciesPerLine + 1, _vertexIndex + _verteciesPerLine);
                        meshData.AddTriangle(_vertexIndex + _verteciesPerLine + 1, _vertexIndex, _vertexIndex + 1);
                    }

                    _vertexIndex++;
                }
            }

            return meshData;
        }
    }

    public class MeshData
    {
        public  Vector3[] vertecies = null;
        public int[] triangle = null;
        public Vector2[] uvs = null;

        private int _triangleIndex = 0;

        public MeshData(int meshWidth, int meshHeight)
        {
            vertecies = new Vector3[meshWidth * meshHeight];
            uvs = new Vector2[meshWidth * meshHeight];
            triangle = new int[((meshWidth - 1) * (meshHeight - 1)) * 6];
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangle[_triangleIndex] = a;
            triangle[_triangleIndex + 1] = b;
            triangle[_triangleIndex + 2] = c;
            _triangleIndex+=3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertecies;
            mesh.triangles = triangle;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            
            return mesh;
        }
    }
}