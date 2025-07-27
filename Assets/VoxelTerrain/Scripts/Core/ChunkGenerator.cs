using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public IEnumerator RenderChunk(Chunk chunk, Transform parent, Vector3 worldPos)
        {
            GameObject chunkGO = new GameObject("Chunk");
            chunkGO.transform.parent = parent;
            chunkGO.transform.position = worldPos;

            MeshFilter mf = chunkGO.AddComponent<MeshFilter>();
            MeshCollider mc = chunkGO.AddComponent<MeshCollider>();
            MeshRenderer mr = chunkGO.AddComponent<MeshRenderer>();
            mr.material = DataHelper.Instance.DefaultMaterial;

            chunk.SetMeshFilter(mf);
            chunk.SetMeshCollider(mc);
            chunk.SetMeshRenderer(mr);

            var voxels = chunk.voxels;
            int count = 0;
            foreach (var voxel in voxels)
            {
                if (voxel != null && voxel.IsSolid)
                    chunk.AddVoxelMesh(voxel);

                count++;
                if (count % 500 == 0)
                    yield return null;
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
