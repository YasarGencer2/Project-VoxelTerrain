using UnityEngine;

public class DataHelper : MonoBehaviour
{
    public static DataHelper Instance;

    [SerializeField] private DefaultChunkData defaultChunkData;
    public DefaultChunkData DefaultChunkData => defaultChunkData;
    [SerializeField] private VoxelObjectDataSet voxelObjectDataSet;
    public VoxelObjectDataSet VoxelObjectDataSet => voxelObjectDataSet;
    void Awake()
    {
        Instance = this;
    }

}