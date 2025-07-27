using UnityEngine;
namespace VoxelTerrain
{
    public static class PerlinGenerator
    {
        public static Voxel[,,] GeneratePerlinTerrain(int width, int height, int depth, float noiseScale, float heightMultiplier, Vector2 offset)
        {
            var terrainData = DataHelper.Instance.VoxelTerrainData;
            Voxel[,,] voxels = new Voxel[width, height, depth];
            int minHeight = height / 4;
            for (int x = 0; x < width; x++)
                for (int z = 0; z < depth; z++)
                {
                    float xCoord = (x + offset.x) * noiseScale;
                    float zCoord = (z + offset.y) * noiseScale;
                    float noise = Mathf.PerlinNoise(xCoord, zCoord);
                    int columnHeight = Mathf.FloorToInt(noise * heightMultiplier);
                    // columnHeight = Mathf.Clamp(columnHeight, minHeight, height);

                    for (int y = 0; y < height; y++)
                    {
                        bool isActive = y <= columnHeight;
                        VoxelType type = VoxelType.Air;

                        if (isActive)
                        {
                            int surfaceY = columnHeight;
                            if (y == surfaceY && y >= terrainData.GrassHeight) type = VoxelType.Grass;
                            else if (y > surfaceY - terrainData.DirtHeight) type = VoxelType.Dirt;
                            else type = VoxelType.Stone;
                        }

                        voxels[x, y, z] = new Voxel(new Vector3Int(x, y, z), type);
                    }
                }

            return voxels;
        }
    }
}
