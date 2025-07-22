using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Voxel
{
    public Vector3Int Position { get; private set; } 
    public VoxelType Type { get; set; }
    public Vector3[] Vertices { get; private set; }
    public bool IsSolid => Type != VoxelType.Air;

    public Voxel(Vector3Int position, VoxelType type = VoxelType.Air)
    {
        Position = position;
        Type = type;
        InitializeVertices();
        // Debug.Log($"Voxel created at {position} with type {type}");
    }
    void InitializeVertices()
    {
        var size = DataHelper.Instance.DefaultChunkData.VoxelSize;
        Vertices = new Vector3[8];
        Vertices[0] = new Vector3(0, 0, 0) * size;
        Vertices[1] = new Vector3(1, 0, 0) * size;
        Vertices[2] = new Vector3(1, 1, 0) * size;
        Vertices[3] = new Vector3(0, 1, 0) * size;
        Vertices[4] = new Vector3(0, 0, 1) * size;
        Vertices[5] = new Vector3(1, 0, 1) * size;
        Vertices[6] = new Vector3(1, 1, 1) * size;
        Vertices[7] = new Vector3(0, 1, 1) * size;
    }
}
public enum VoxelType
{
    Air,
    Dirt,
    Grass,
    Stone,
    Water,
    Sand,
}
