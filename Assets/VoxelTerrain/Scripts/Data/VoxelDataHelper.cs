using UnityEngine;
namespace VoxelTerrain
{
    public class DataHelper : MonoBehaviour
    {
        public static DataHelper Instance;

        [SerializeField] private DefaultChunkData defaultChunkData;
        public DefaultChunkData DefaultChunkData => defaultChunkData;
        [SerializeField] private VoxelObjectDataSet voxelObjectDataSet;
        public VoxelObjectDataSet VoxelObjectDataSet => voxelObjectDataSet;
        [SerializeField] private Material defaultMaterial;
        public Material DefaultMaterial => defaultMaterial;
        void Awake()
        {
            Instance = this;
        }

    }
}