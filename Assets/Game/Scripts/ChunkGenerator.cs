using UnityEngine;

public class ChunkGenerator
{ 
    DefaultChunkData chunkData = DataHelper.Instance.DefaultChunkData;
    public Chunk GeneratePerlinChunk(Vector2 offset)
    {
        Chunk chunk = new Chunk(chunkData.Width, chunkData.Height, chunkData.Depth);
        chunk.voxels = PerlinGenerator.GeneratePerlinTerrain(chunkData.Width, chunkData.Height, chunkData.Depth, chunkData.NoiseScale, chunkData.HeightMultiplier, offset);
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
