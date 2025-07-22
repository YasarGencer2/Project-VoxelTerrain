using System.Collections.Generic;
using UnityEngine;
namespace VoxelTerrain
{
    public class ChunkGenerator
    {
        VoxelChunkData chunkData => DataHelper.Instance.VoxelChunkData;
        VoxelTerrainData terrainData => DataHelper.Instance.VoxelTerrainData;


        public Chunk GeneratePerlinChunk(Vector2 offset)
        {
            Chunk chunk = new Chunk(chunkData.Width, chunkData.Height, chunkData.Depth);
            chunk.voxels = PerlinGenerator.GeneratePerlinTerrain(chunkData.Width, chunkData.Height, chunkData.Depth, chunkData.NoiseScale, chunkData.HeightMultiplier, offset);
            return chunk;
        }

        public void RenderChunk(Chunk chunk, Transform parent, Vector3 worldPos)
        {

            GameObject chunkGO = new GameObject("Chunk");
            chunkGO.transform.parent = parent;
            chunkGO.transform.position = worldPos;

            MeshFilter mf = chunkGO.AddComponent<MeshFilter>();
            MeshRenderer mr = chunkGO.AddComponent<MeshRenderer>();
            mr.material = DataHelper.Instance.DefaultMaterial;

            chunk.SetMeshFilter(mf);
            chunk.SetMeshRenderer(mr);

            for (int x = 0; x < chunk.width; x++)
                for (int y = 0; y < chunk.height; y++)
                    for (int z = 0; z < chunk.depth; z++)
                    {
                        Voxel voxel = chunk.GetVoxel(x, y, z);
                        if (voxel != null && voxel.IsSolid)
                            chunk.AddVoxelMesh(voxel);
                    }

            chunk.UpdateMesh();
        }
        public Vector2Int GetNeighborChunkCoord(ref Vector3Int localPos, Vector2Int currentChunkCoord)
        {
            int chunkWidth = chunkData.Width;
            int chunkDepth = chunkData.Depth;

            Vector2Int neighborChunkCoord = currentChunkCoord;

            if (localPos.x < 0)
            {
                neighborChunkCoord.x -= 1;
                localPos.x += chunkWidth;
            }
            else if (localPos.x >= chunkWidth)
            {
                neighborChunkCoord.x += 1;
                localPos.x -= chunkWidth;
            }

            if (localPos.z < 0)
            {
                neighborChunkCoord.y -= 1;
                localPos.z += chunkDepth;
            }
            else if (localPos.z >= chunkDepth)
            {
                neighborChunkCoord.y += 1;
                localPos.z -= chunkDepth;
            }

            return neighborChunkCoord;
        }

    }
}
