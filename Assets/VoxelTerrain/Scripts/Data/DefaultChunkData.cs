using UnityEngine;

namespace VoxelTerrain
{
    [CreateAssetMenu(fileName = "VoxelChunkData", menuName = "VoxelTerrain/VoxelChunkData", order = 1)]
    public class VoxelChunkData : ScriptableObject
    {
        public int Width = 16;
        public int Height = 16;
        public int Depth = 16;
        public float VoxelSize = .25f;
        public int Seed = 0;

        public float NoiseScale = 0.1f;
        public float HeightMultiplier = 8f;
        public Vector2 NoiseOffset;
    }
}