using System.Linq;
using UnityEngine;

namespace _Scripts.Utils
{
    public static class PerlinNoise
    {
        public static float[,] GetNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) 
        {
            float[,] noiseMap = new float[width, height];

            System.Random pseudoRandomNumberGenerator = new System.Random(seed);
            Vector2[] octaveOffsets = new Vector2[octaves];

            for(int octaveNumber = 0; octaveNumber < octaves; octaveNumber++) 
            {
                float xOffset = pseudoRandomNumberGenerator.Next(-10000, 10000) + offset.x;
                float yOffset = pseudoRandomNumberGenerator.Next(-10000, 10000) + offset.y;

                octaveOffsets[octaveNumber] = new Vector2(xOffset, yOffset);
            }

            if (scale <= 0)
                scale = 0.0001f;

            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

            float amplitude = 1;
            float frequency = 1;

            for(int octaveNumber = 0; octaveNumber < octaves; octaveNumber++) 
            {
                for(int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                for(int xCoordinate = 0; xCoordinate < width; xCoordinate++) 
                {
                    float xPerlinLocation = (xCoordinate - halfWidth) / scale * frequency + octaveOffsets[octaveNumber].x;
                    float yPerlinLocation = (yCoordinate - halfHeight) / scale * frequency + octaveOffsets[octaveNumber].y;
            
                    float perlinValue = Mathf.PerlinNoise(xPerlinLocation, yPerlinLocation) * 2 - 1;
                    noiseMap[xCoordinate, yCoordinate] += perlinValue * amplitude;
                }
        
                amplitude *= persistance;
                frequency *= lacunarity;
            }
    
            float maxNoiseHeight = noiseMap.Cast<float>().Max();
            float minNoiseHeight = noiseMap.Cast<float>().Min();
    
            for(int yCoordinate = 0; yCoordinate < height; yCoordinate++)
            for(int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                noiseMap[xCoordinate, yCoordinate] =
                    Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[xCoordinate, yCoordinate]);
    
            return noiseMap;
        }
    }
}