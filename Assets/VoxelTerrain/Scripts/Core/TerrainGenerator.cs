using System.Collections.Generic;
using UnityEngine;

namespace VoxelTerrain
{
    public class TerrainGenerator : MonoBehaviour
    {
        public static TerrainGenerator Instance;
        VoxelChunkData chunkData => DataHelper.Instance.VoxelChunkData;
        VoxelTerrainData terrainData => DataHelper.Instance.VoxelTerrainData;

        public ChunkGenerator Generator { get; private set; }
        public Dictionary<Vector2Int, Chunk> Chunks = new();

        void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (var item in Chunks)
                {
                    Destroy(item.Value.GetMeshRender().gameObject);
                }
                Start();
            }
        }
        void Start()
        {
            Chunks.Clear();
            Generator = new ChunkGenerator();
            var size = DataHelper.Instance.VoxelChunkData.VoxelSize;
            for (int cx = 0; cx < terrainData.ChunkCountX; cx++)
                for (int cz = 0; cz < terrainData.ChunkCountY; cz++)
                {
                    Vector2 offset = chunkData.NoiseOffset + new Vector2(cx * chunkData.Width, cz * chunkData.Depth) + new Vector2(chunkData.Seed, chunkData.Seed) * chunkData.NoiseScale;
                    Vector3 worldPos = new Vector3(cx * chunkData.Width, 0, cz * chunkData.Depth) * size;
                    Chunk chunk = Generator.GeneratePerlinChunk(offset);
                    Vector2Int chunkCoord = new Vector2Int(cx, cz);
                    chunk.chunkCoord = chunkCoord;
                    Chunks.Add(chunkCoord, chunk);
                    Generator.RenderChunk(chunk, transform, worldPos);
                }
        }

    }
}
