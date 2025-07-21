using UnityEngine;

public class Chunk
{
    public readonly int width;
    public readonly int height;
    public readonly int depth;
    public Transform Parent;

    public bool[,,] voxels;

    public Chunk(int width, int height, int depth)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;

        voxels = new bool[width, height, depth];
    }

    public void SetVoxel(int x, int y, int z, bool value)
    {
        if (InBounds(x, y, z))
            voxels[x, y, z] = value;
    }

    public bool GetVoxel(int x, int y, int z)
    {
        if (InBounds(x, y, z))
            return voxels[x, y, z];
        return false;
    }

    bool InBounds(int x, int y, int z)
    {
        return x >= 0 && x < width &&
               y >= 0 && y < height &&
               z >= 0 && z < depth;
    }
    public void CreateVoxel(int x, int y, int z)
    {
        if (GetVoxel(x, y, z) == false)
            return;
        GameObject voxel = Object.Instantiate(TerrainGenerator.Instance.CubePrefab, Parent);
        voxel.transform.localPosition = new Vector3(x, y, z); 
    }
}
