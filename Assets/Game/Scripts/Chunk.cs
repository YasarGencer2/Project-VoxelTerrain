using UnityEngine;

public class Chunk
{
    public readonly int width;
    public readonly int height;
    public readonly int depth;
    public Transform Parent;

    public Voxel[,,] voxels;

    public Chunk(int width, int height, int depth)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;

        voxels = new Voxel[width, height, depth];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < depth; z++)
                    voxels[x, y, z] = new Voxel(new Vector3Int(x, y, z));
    }

    public void SetVoxel(int x, int y, int z, bool isActive, VoxelType type)
    {
        if (InBounds(x, y, z))
        {
            voxels[x, y, z].IsActive = isActive;
            voxels[x, y, z].Type = type;
        }
    }

    public Voxel GetVoxel(int x, int y, int z)
    {
        if (InBounds(x, y, z))
            return voxels[x, y, z];
        return null;
    }

    bool InBounds(int x, int y, int z)
    {
        return x >= 0 && x < width &&
               y >= 0 && y < height &&
               z >= 0 && z < depth;
    }

    public void CreateVoxel(int x, int y, int z)
    {
        Voxel voxel = GetVoxel(x, y, z);
        if (voxel == null || !voxel.IsActive)
            return;
        GameObject voxelGO = Object.Instantiate(DataHelper.Instance.VoxelObjectDataSet.GetVoxelObjectData(voxel.Type).Prefab, Parent);
        voxelGO.transform.localPosition = new Vector3(x, y, z);
    }
}
