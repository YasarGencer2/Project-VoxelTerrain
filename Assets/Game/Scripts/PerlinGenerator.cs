using UnityEngine;

public static class PerlinGenerator
{
    public static Voxel[,,] GeneratePerlinTerrain(int width, int height, int depth, float noiseScale, float heightMultiplier, Vector2 offset)
    {
        Voxel[,,] voxels = new Voxel[width, height, depth];

        for (int x = 0; x < width; x++)
            for (int z = 0; z < depth; z++)
            {
                float xCoord = (x + offset.x) * noiseScale;
                float zCoord = (z + offset.y) * noiseScale;
                float noise = Mathf.PerlinNoise(xCoord, zCoord);
                int columnHeight = Mathf.FloorToInt(noise * heightMultiplier);

                for (int y = 0; y < height; y++)
                {
                    bool isActive = y <= columnHeight;
                    VoxelType type = VoxelType.Air;

                    if (isActive)
                    {
                        if (y == columnHeight) type = VoxelType.Grass;
                        else type = VoxelType.Dirt;
                    }

                    voxels[x, y, z] = new Voxel(new Vector3Int(x, y, z), type);
                }
            }

        return voxels;
    }


}
