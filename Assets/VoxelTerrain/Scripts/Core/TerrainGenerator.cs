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
                CreateTerrain();
            }
        }
        void Start()
        {
            CreateTerrain();
        }
        void CreateTerrain()
        {
            Chunks.Clear();
            Generator = new ChunkGenerator();
            int startX = -terrainData.ChunkCountX / 2;
            int startZ = -terrainData.ChunkCountY / 2;
            int endX = startX * -1;
            int endZ = startZ * -1;
            for (int cx = startX; cx < endX; cx++)
                for (int cz = startZ; cz < endZ; cz++)
                {
                    LoadChunk(cx, cz);
                }
        }
        public void LoadChunk(int x, int z)
        {
            Vector2Int chunkCoord = new Vector2Int(x, z);
            if (Chunks.ContainsKey(chunkCoord))
                return;
            Vector3 worldPos = GetWorldPosition(chunkCoord);
            Vector2 offset = chunkData.NoiseOffset + new Vector2(x * chunkData.Width, z * chunkData.Depth) + new Vector2(chunkData.Seed, chunkData.Seed) * chunkData.NoiseScale;
            Chunk chunk = Generator.GeneratePerlinChunk(offset);
            chunk.chunkCoord = chunkCoord;
            Chunks.Add(chunkCoord, chunk);
            Generator.RenderChunk(chunk, transform, worldPos);
        }
        public void UnloadChunk(int x, int z)
        {
            Vector2Int chunkCoord = new Vector2Int(x, z);
            if (Chunks.TryGetValue(chunkCoord, out Chunk chunk))
            {
                // Destroy(chunk.GetMeshRender().gameObject);
                chunk.GetMeshRender().gameObject.SetActive(false);
                Chunks.Remove(chunkCoord);
            }
        }
        public Vector3 GetWorldPosition(Vector2Int chunkCoord)
        {
            return new Vector3(chunkCoord.x * chunkData.Width, 0, chunkCoord.y * chunkData.Depth) * chunkData.VoxelSize;
        }

        public Vector2Int GetChunkIndex(Vector3 worldPos)
        {
            int x = Mathf.FloorToInt(worldPos.x / (chunkData.Width * chunkData.VoxelSize));
            int z = Mathf.FloorToInt(worldPos.z / (chunkData.Depth * chunkData.VoxelSize));
            return new Vector2Int(x, z);
        }

        public Chunk GetChunk(Vector2Int chunkCoord)
        {
            if (Chunks.TryGetValue(chunkCoord, out Chunk chunk))
            {
                return chunk;
            }
            return null;
        }

    }
}
