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

    public void RenderChunk(Transform parent, Vector3 offset, Vector3 worldPos)
    {
        Chunk chunk = GeneratePerlinChunk(offset);

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
                    if (voxel != null && voxel.IsActive)
                        chunk.AddVoxelMesh(voxel);
                }

        chunk.UpdateMesh();
    }

}
