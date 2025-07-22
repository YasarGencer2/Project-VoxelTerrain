using UnityEngine;

namespace VoxelTerrain
{
    public class DataHelper : MonoBehaviour
    {
        public static DataHelper Instance;

        [SerializeField] private VoxelTerrainData voxelTerrainData;
        [SerializeField] private VoxelChunkData defaultChunkData;
        [SerializeField] private VoxelObjectDataSet voxelObjectDataSet;
        [SerializeField] private Material defaultMaterial;


        public VoxelTerrainData VoxelTerrainData => voxelTerrainData;
        public VoxelChunkData VoxelChunkData => defaultChunkData;
        public VoxelObjectDataSet VoxelObjectDataSet => voxelObjectDataSet;
        public Material DefaultMaterial => defaultMaterial;
        void Awake()
        {
            Instance = this;
        }

    }
}