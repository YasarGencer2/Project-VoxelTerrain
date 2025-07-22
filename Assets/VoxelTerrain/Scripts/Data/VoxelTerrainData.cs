using UnityEngine;

namespace VoxelTerrain
{
    [CreateAssetMenu(fileName = "VoxelTerrainData", menuName = "VoxelTerrain/VoxelTerrainData", order = 1)]
    public class VoxelTerrainData : ScriptableObject
    {
        public int ChunkCountX = 16; 
        public int ChunkCountY = 16; 
    }
}