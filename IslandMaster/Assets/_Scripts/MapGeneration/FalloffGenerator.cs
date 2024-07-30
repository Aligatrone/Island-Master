using UnityEngine;

namespace _Scripts.MapGeneration
{
	public static class FalloffGenerator
	{
		public static float[,] GenerateFalloffMap(int width, int height, float falloffStart, float falloffEnd)
		{
			float[,] map = new float[width, height];
			
			for(int yCoordinate = 0; yCoordinate < height; yCoordinate++)
			for(int xCoordinate = 0; xCoordinate < width; xCoordinate++)
			{
				float nx = (float)2 * xCoordinate / width - 1;
				float ny = (float)2 * yCoordinate / height - 1;

				float value = Mathf.Max(Mathf.Abs(nx), Mathf.Abs(ny));
				
				if(value < falloffStart)
					map[xCoordinate, yCoordinate] = 0;
				else if(value > falloffEnd)
					map[xCoordinate, yCoordinate] = 1;
				else
					map[xCoordinate, yCoordinate] =
						1 - Mathf.SmoothStep(1, 0, Mathf.InverseLerp(falloffStart, falloffEnd, value));
			}

			return map;
		}
	}
}