using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public static TerrainGenerator Instance;
    DefaultChunkData chunkData => DataHelper.Instance.DefaultChunkData;

    public ChunkGenerator Generator { get; private set; }
    [SerializeField] int chunksX = 2;
    [SerializeField] int chunksZ = 2;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Generator = new ChunkGenerator();


        for (int cx = 0; cx < chunksX; cx++)
            for (int cz = 0; cz < chunksZ; cz++)
            {
                Vector2 offset = chunkData.NoiseOffset + new Vector2(cx * chunkData.Width, cz * chunkData.Depth);
                GameObject chunkObj = new GameObject($"Chunk_{cx}_{cz}");
                chunkObj.transform.position = new Vector3(cx * chunkData.Width, 0, cz * chunkData.Depth);
                chunkObj.transform.parent = transform;

                Generator.RenderChunk(chunkObj.transform, offset);
            }
    }
}
