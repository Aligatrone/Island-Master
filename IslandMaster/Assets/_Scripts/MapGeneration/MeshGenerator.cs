using UnityEngine;

namespace _Scripts.MapGeneration
{
	public class MeshGenerator
	{
		public static MeshData GenerateMesh(float[,] heightMap, float heightMultiplier)
		{
			int width = heightMap.GetLength(0);
			int height = heightMap.GetLength(1);

			float topLeftX = (width - 1) / -2f;
			float topLeftZ = (height - 1) / 2f;

			MeshData meshData = new MeshData(width, height);
			
			for(int vertexIndex = 0, yCoordinate = 0; yCoordinate < height; yCoordinate++)
			for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
			{
				meshData.Vertices[vertexIndex] =
					new Vector3(topLeftX + xCoordinate, TerrainCurve.Evaluate(heightMap[xCoordinate, yCoordinate]) * heightMultiplier, topLeftZ - yCoordinate);
				
				meshData.UVs[vertexIndex] = new Vector2(xCoordinate / (float)width, yCoordinate / (float)height);

				if (xCoordinate < width - 1 && yCoordinate < height - 1)
				{
					meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}
				
				vertexIndex++;
			}

			return meshData;
		}
	}

	public static class TerrainCurve
	{
		public static float Evaluate(float value)
		{
			float h = 0.4f;
			
			float a = 1.7f;
			float b = 0.0f;

			float c = 1.7f;
			float d = 0.0f;
			
			return Mathf.Pow(a * Mathf.Max(0, value - h) + b, c) - d;
		}
	}
	
	public class MeshData
	{
		public Vector3[] Vertices;
		public int[] Triangles;
		public Vector2[] UVs;

		private int _triangleIndex;

		public MeshData(int width, int height)
		{
			Vertices = new Vector3[width * height];
			Triangles = new int[(width - 1) * (height - 1) * 6];
			UVs = new Vector2[width * height];
		}

		public void AddTriangle(int a, int b, int c)
		{
			Triangles[_triangleIndex] = a;
			Triangles[_triangleIndex + 1] = b;
			Triangles[_triangleIndex + 2] = c;

			_triangleIndex += 3;
		}

		public Mesh CreateMash()
		{
			Mesh mesh = new Mesh();
			
			mesh.vertices = Vertices;
			mesh.triangles = Triangles;
			mesh.uv = UVs;
			
			mesh.RecalculateNormals();

			return mesh;
		}
	}
}
