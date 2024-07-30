using UnityEngine;

namespace _Scripts.Utils
{
    public static class TextureGenerator
    {
        public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) 
        {
            Texture2D texture = new Texture2D(width, height)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        public static Texture2D TextureFromHeightMap(float[,] heightMap) 
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);
    
            Color[] colorMap = new Color[width * height];
    
            for(int yCoordinate = 0; yCoordinate < height; yCoordinate++)
            for(int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                colorMap[yCoordinate * width + xCoordinate] =
                    Color.Lerp(Color.black, Color.white, heightMap[xCoordinate, yCoordinate]);

            return TextureFromColorMap(colorMap, width, height);
        }
    }
}