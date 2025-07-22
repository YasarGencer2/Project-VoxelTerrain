using System.Collections.Generic;
using UnityEngine;

namespace VoxelTerrain
{
    public class TerrainGenerator : MonoBehaviour
    {
        public static TerrainGenerator Instance;
        DefaultChunkData chunkData => DataHelper.Instance.DefaultChunkData;

        public ChunkGenerator Generator { get; private set; }
        [SerializeField] int chunksX = 2;
        [SerializeField] int chunksZ = 2;
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
            var size = DataHelper.Instance.DefaultChunkData.VoxelSize;
            for (int cx = 0; cx < chunksX; cx++)
                for (int cz = 0; cz < chunksZ; cz++)
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
