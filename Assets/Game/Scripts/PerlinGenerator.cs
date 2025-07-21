using UnityEngine;

public static class PerlinGenerator
{
    public static bool[,,] GeneratePerlinTerrain(int width, int height, int depth, float noiseScale, float heightMultiplier, Vector2 offset)
    {
        bool[,,] voxels = new bool[width, height, depth];

        for (int x = 0; x < width; x++)
            for (int z = 0; z < depth; z++)
            {
                float xCoord = (x + offset.x) * noiseScale;
                float zCoord = (z + offset.y) * noiseScale;
                float noise = Mathf.PerlinNoise(xCoord, zCoord);
                int columnHeight = Mathf.FloorToInt(noise * heightMultiplier);

                for (int y = 0; y < height; y++)
                    voxels[x, y, z] = y <= columnHeight;
            }

        return voxels;
    }
}
