using UnityEngine;

namespace _Scripts.MapGeneration.IslandTypes
{
	[CreateAssetMenu(fileName = "New IslandBiome", menuName = "IslandBiome")]
	public class IslandBiome : ScriptableObject
	{
		public TerrainType[] regions;

		public float meshHeightMultiplier;
	}
	
	[System.Serializable]
	public struct TerrainType
	{
		public float height;
		public Color color;
	}
}