using System.Collections.Generic;
using _Scripts.MapGeneration.IslandTypes;
using _Scripts.Utils;
using UnityEngine;
using System.Linq;

namespace _Scripts.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public enum DrawMode 
        {
            NoiseMap,
            ColorMap,
            Mesh,
            FalloffMap,
            Map
        };

        public DrawMode drawMode;

        public int mapWidth;
        public int mapHeight;
        public float noiseScale;

        public int islandsWidth;
        public int islandsHeight;

        public int islandScale;

        [Range(0, 1)]
        public float falloffStart;
        
        [Range(0, 1)]
        public float falloffEnd;

        public bool autoUpdate;

        public int octaves;

        public bool useFalloffMap;

        [Range(0, 1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        private float[,] _falloffMap;

        public GameObject map;

        public Material mapMaterial;

        public IslandBiome[] biomes;

        private void Awake()
        {
            _falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight, falloffStart, falloffEnd);
        }

        public void GenerateMap()
        {
            List<int> listOfUsedBiomes = new();
            
            if(drawMode == DrawMode.Map)
            {
                System.Random islandsSeedGenerator = new System.Random(seed);
            
                for(int yCoordinate = 0; yCoordinate < islandsHeight; yCoordinate++)
                for(int xCoordinate = 0; xCoordinate < islandsWidth; xCoordinate++)
                {
                    int islandSeed = islandsSeedGenerator.Next(0, 10000);
                    
                    IslandBiome biome;
                    
                    if(yCoordinate == 1 && xCoordinate == 1)
                        biome = biomes[0];
                    else
                    {
                        int randomBiome = islandSeed % biomes.GetLength(0);

                        int offsetBiome = 1;
                        while(listOfUsedBiomes.Count(item => item.Equals(randomBiome)) > 1)
                        {
                            randomBiome = (islandSeed + offsetBiome) % biomes.GetLength(0);
                            offsetBiome++;

                            if(offsetBiome > 10)
                                break;
                        }

                        biome = biomes[randomBiome];
                        listOfUsedBiomes.Add(randomBiome);
                    }
                    
                    IslandData island = GenerateIsland(islandSeed, biome);

                    CreateIsland(island, xCoordinate, yCoordinate);
                }

                return;
            }
            
            IslandData islandToDisplay = GenerateIsland(seed, biomes[seed % biomes.GetLength(0)]);
            
            MapDisplay display = FindObjectOfType<MapDisplay>();
            
            if(drawMode == DrawMode.NoiseMap)
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(islandToDisplay.IslandHeightMap));
            else if(drawMode == DrawMode.ColorMap)
                display.DrawTexture(TextureGenerator.TextureFromColorMap(islandToDisplay.IslandColorMap, mapWidth, mapHeight));
            else if(drawMode == DrawMode.Mesh)
                display.DrawMesh(MeshGenerator.GenerateMesh(islandToDisplay.IslandHeightMap, islandToDisplay.Biome.meshHeightMultiplier), TextureGenerator.TextureFromColorMap(islandToDisplay.IslandColorMap, mapWidth, mapHeight));
            else if(drawMode == DrawMode.FalloffMap)
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(_falloffMap));
        }

        private void CreateIsland(IslandData island, int x, int y)
        {
            GameObject islandGameObject = new GameObject("Island");

            MeshFilter islandMeshFilter = islandGameObject.AddComponent<MeshFilter>();
            MeshRenderer islandMeshRenderer = islandGameObject.AddComponent<MeshRenderer>();
            MeshCollider islandMeshCollider = islandGameObject.AddComponent<MeshCollider>();

            islandGameObject.transform.position =
                new Vector3(x * islandScale * (mapWidth - 1), 0, y * islandScale * (mapHeight - 1));
            
            Mesh islandMesh = MeshGenerator.GenerateMesh(island.IslandHeightMap, island.Biome.meshHeightMultiplier).CreateMash();
            islandMeshFilter.mesh = islandMesh;
            islandMeshCollider.sharedMesh = islandMesh;

            islandMeshRenderer.sharedMaterial = new Material(mapMaterial);
            Texture2D islandTexture = TextureGenerator.TextureFromColorMap(island.IslandColorMap, mapWidth, mapHeight);
            islandMeshRenderer.sharedMaterial.SetTexture("_MainTex", islandTexture);

            islandGameObject.transform.parent = map.transform;
            islandGameObject.transform.localScale = Vector3.one * islandScale;
        }

        private IslandData GenerateIsland(int islandSeed, IslandBiome biome)
        {
            float[,] noiseMap = PerlinNoise.GetNoiseMap(mapWidth, mapHeight, islandSeed, noiseScale, octaves, persistance, lacunarity, offset);

            Color[] colorMap = new Color[mapHeight * mapWidth];
    
            for(int yCoordinate = 0; yCoordinate < mapHeight; yCoordinate++)
            for(int xCoordinate = 0; xCoordinate < mapWidth; xCoordinate++) 
            {
                if(useFalloffMap)
                    noiseMap[xCoordinate, yCoordinate] = Mathf.Clamp01(noiseMap[xCoordinate, yCoordinate] - _falloffMap[xCoordinate, yCoordinate]);
                
                float currentHeight = noiseMap[xCoordinate, yCoordinate];

                for(int i = 0; i < biome.regions.Length; i++) 
                {
                    if(currentHeight <= biome.regions[i].height) 
                    {
                        colorMap[yCoordinate * mapWidth + xCoordinate] = biome.regions[i].color;
                        break;
                    }
                }
            }
            
            return new IslandData(noiseMap, colorMap, biome);
        }

        private void OnValidate() 
        {
            if(mapWidth < 1)
                mapWidth = 1;

            if(mapHeight < 1)
                mapHeight = 1;

            if(lacunarity < 1)
                lacunarity = 1;

            if(octaves < 0)
                octaves = 0;

            _falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight, falloffStart, falloffEnd);
        }
    }

    public class IslandData
    {
        public float[,] IslandHeightMap;
        public Color[] IslandColorMap;
        public IslandBiome Biome;

        public IslandData(float[,] islandHeightMap, Color[] islandColorMap, IslandBiome biome)
        {
            IslandHeightMap = islandHeightMap;
            IslandColorMap = islandColorMap;
            Biome = biome;
        }
    }
}