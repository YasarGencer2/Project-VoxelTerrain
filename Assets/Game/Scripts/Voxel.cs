using UnityEngine;

public class Voxel
{
    public Vector3Int Position { get; private set; }
    public bool IsActive { get; set; }
    public VoxelType Type { get; set; }

    public Voxel(Vector3Int position, bool isActive = false, VoxelType type = VoxelType.Air)
    {
        Position = position;
        IsActive = isActive;
        Type = type;
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
