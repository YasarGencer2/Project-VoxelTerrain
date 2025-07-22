using UnityEngine;

[CreateAssetMenu(fileName = "VoxelObjectDataSet", menuName = "VoxelTerrain/VoxelObjectDataSet", order = 1)]
public class VoxelObjectDataSet : ScriptableObject
{
    public VoxelObjectData[] VoxelObjects;

    public VoxelObjectData GetVoxelObjectData(string name)
    {
        foreach (var voxelObject in VoxelObjects)
        {
            if (voxelObject.Name == name)
            {
                return voxelObject;
            }
        }
        Debug.LogWarning($"Voxel object with name {name} not found.");
        return default;
    }
    public VoxelObjectData GetVoxelObjectData(VoxelType type)
    {
        foreach (var voxelObject in VoxelObjects)
        {
            if (voxelObject.Type == type)
            {
                return voxelObject;
            }
        }
        Debug.LogWarning($"Voxel object with type {type} not found.");
        return default;
    }
    public Color GetColorByVoxelType(VoxelType type)
    {
        foreach (var voxelObject in VoxelObjects)
        {
            if (voxelObject.Type == type)
            {
                return voxelObject.Color;
            }
        }
        Debug.LogWarning($"Color for voxel type {type} not found.");
        return Color.white;  
    }
}
[System.Serializable]
public struct VoxelObjectData
{
    public string Name;
    public GameObject Prefab;
    public VoxelType Type;
    public Color Color;

    public VoxelObjectData(string name, GameObject prefab, VoxelType type, Color color)
    {
        Name = name;
        Prefab = prefab;
        Type = type;
        Color = color;
    }
}