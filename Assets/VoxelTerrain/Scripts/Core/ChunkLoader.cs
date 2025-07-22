using System.Collections.Generic;
using UnityEngine;
using VoxelTerrain;

public class ChunkLoader : MonoBehaviour
{
    TerrainGenerator terrainGenerator => TerrainGenerator.Instance;

    Vector2Int CurrentChunkCoord;

    void Update()
    {
        CheckCurrentChunk();
    }
    void CheckCurrentChunk()
    {
        Vector2Int currentChunkCoord = terrainGenerator.GetChunkIndex(transform.position);
        if (CurrentChunkCoord == currentChunkCoord)
            return;
        CurrentChunkCoord = currentChunkCoord;
        LoadChunks();
        UnloadChunks();
    }
    void LoadChunks()
    {
        var LoadDistance = DataHelper.Instance.VoxelTerrainData.LoadDistance;
        for (int i = -LoadDistance; i < LoadDistance; i++)
        {
            for (int j = -LoadDistance; j < LoadDistance; j++)
            {
                Vector2Int chunkCoord = new Vector2Int(CurrentChunkCoord.x + i, CurrentChunkCoord.y + j);
                terrainGenerator.LoadChunk(chunkCoord.x, chunkCoord.y);
            }
        }
    }
    void UnloadChunks()
    {
        var UnloadDistance = DataHelper.Instance.VoxelTerrainData.UnloadDistance;
        Vector2Int currentChunkCoord = terrainGenerator.GetChunkIndex(transform.position);
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (var chunk in terrainGenerator.Chunks)
        {
            if (Mathf.Abs(chunk.Key.x - currentChunkCoord.x) > UnloadDistance || Mathf.Abs(chunk.Key.y - currentChunkCoord.y) > UnloadDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkCoord in chunksToRemove)
        {
            terrainGenerator.UnloadChunk(chunkCoord.x, chunkCoord.y);
        }
    }
}