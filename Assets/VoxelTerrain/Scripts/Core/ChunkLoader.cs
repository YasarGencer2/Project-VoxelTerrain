using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VoxelTerrain
{
    public class ChunkLoader : MonoBehaviour
    {
        TerrainGenerator terrainGenerator => TerrainGenerator.Instance;

        Vector2Int CurrentChunkCoord;
        List<Vector2Int> ChunksToLoad = new List<Vector2Int>();
        List<Vector2Int> ChunksToUnload = new List<Vector2Int>();
        int chunksPerFrame = 1;
        bool isHandlingChunks = false;


        void Start()
        {
            CurrentChunkCoord = terrainGenerator.GetChunkIndex(transform.position);
            SetChunksToLoad();
            isHandlingChunks = false;
        }
        void Update()
        {
            CheckCurrentChunk();
            if (!isHandlingChunks && (ChunksToLoad.Count > 0 || ChunksToUnload.Count > 0))
            {
                StartCoroutine(HandleChunksCoroutine());
            }

        }
        void CheckCurrentChunk()
        {
            Vector2Int currentChunkCoord = terrainGenerator.GetChunkIndex(transform.position);
            if (CurrentChunkCoord == currentChunkCoord)
                return;
            CurrentChunkCoord = currentChunkCoord;
            SetChunksToLoad();
            SetChunksToUnload();
        }
        IEnumerator HandleChunksCoroutine()
        {
            isHandlingChunks = true;

            while (ChunksToLoad.Count > 0 || ChunksToUnload.Count > 0)
            {
                int loaded = 0;

                while (loaded < chunksPerFrame && ChunksToLoad.Count > 0)
                {
                    var chunkCoord = ChunksToLoad[0];
                    terrainGenerator.LoadChunk(chunkCoord.x, chunkCoord.y);
                    ChunksToLoad.RemoveAt(0);
                    loaded++;
                }

                int unloaded = 0;

                while (unloaded < chunksPerFrame && ChunksToUnload.Count > 0)
                {
                    var chunkCoord = ChunksToUnload[0];
                    terrainGenerator.UnloadChunk(chunkCoord.x, chunkCoord.y);
                    ChunksToUnload.RemoveAt(0);
                    unloaded++;
                }

                yield return null;
            }

            isHandlingChunks = false;
        }

        void SetChunksToLoad()
        {
            var LoadDistance = DataHelper.Instance.VoxelTerrainData.LoadDistance;
            for (int i = -LoadDistance; i < LoadDistance; i++)
            {
                for (int j = -LoadDistance; j < LoadDistance; j++)
                {
                    Vector2Int chunkCoord = new Vector2Int(CurrentChunkCoord.x + i, CurrentChunkCoord.y + j);
                    if (terrainGenerator.Chunks.ContainsKey(chunkCoord))
                        continue;
                    ChunksToLoad.Add(chunkCoord);
                }
            }
        }
        void SetChunksToUnload()
        {
            var UnloadDistance = DataHelper.Instance.VoxelTerrainData.UnloadDistance;
            Vector2Int currentChunkCoord = terrainGenerator.GetChunkIndex(transform.position);

            foreach (var chunk in terrainGenerator.Chunks)
            {
                if (Mathf.Abs(chunk.Key.x - currentChunkCoord.x) > UnloadDistance || Mathf.Abs(chunk.Key.y - currentChunkCoord.y) > UnloadDistance)
                {
                    ChunksToUnload.Add(chunk.Key);
                }
            }
        }
    }
}