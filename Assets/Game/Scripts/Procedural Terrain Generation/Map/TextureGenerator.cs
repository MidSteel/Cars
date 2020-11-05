using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralTerrainGeneratio.Map
{
    public static class TextureGenerator
    {
        //Cache
        private static Texture2D _texture = null;

        public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
        {
            _texture = new Texture2D(width, height);
            _texture.filterMode = FilterMode.Point;
            _texture.wrapMode = TextureWrapMode.Clamp;
            _texture.SetPixels(colorMap);
            _texture.Apply();
            return _texture;
        }
    }
}