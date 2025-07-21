using UnityEngine;

public class ChunkGenerator
{
    public GameObject CubePrefab;

    public Chunk GeneratePerlinChunk(Vector2 offset)
    {
        var data = TerrainGenerator.Instance.DefaultChunkData;
        Chunk chunk = new Chunk(data.Width, data.Height, data.Depth);
        chunk.voxels = PerlinGenerator.GeneratePerlinTerrain(data.Width, data.Height, data.Depth, data.NoiseScale, data.HeightMultiplier, offset);
        return chunk;
    }

    public void RenderChunk(Transform parent, Vector3 offset)
    {
        Chunk chunk = GeneratePerlinChunk(offset);
        chunk.Parent = parent;
        for (int x = 0; x < chunk.width; x++)
            for (int y = 0; y < chunk.height; y++)
                for (int z = 0; z < chunk.depth; z++)
                    chunk.CreateVoxel(x, y, z);
    }
}
