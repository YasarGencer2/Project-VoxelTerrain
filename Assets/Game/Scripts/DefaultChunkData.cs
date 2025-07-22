using UnityEngine; 

[CreateAssetMenu(fileName = "DefaultChunkData", menuName = "VoxelTerrain/DefaultChunkData", order = 1)]
public class DefaultChunkData : ScriptableObject
{
    public int Width = 16;
    public int Height = 16;
    public int Depth = 16;
    public float VoxelSize = .25f;

    public float NoiseScale = 0.1f;
    public float HeightMultiplier = 8f;
    public Vector2 NoiseOffset;
}