using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public static TerrainGenerator Instance;
    [SerializeField] DefaultChunkData defaultChunkData;
    public DefaultChunkData DefaultChunkData => defaultChunkData;
    [SerializeField] GameObject cubePrefab;
    public GameObject CubePrefab => cubePrefab;

    public ChunkGenerator Generator { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Generator = new ChunkGenerator();
        Generator.CubePrefab = cubePrefab;

        int chunksX = 2;
        int chunksZ = 2;

        for (int cx = 0; cx < chunksX; cx++)
            for (int cz = 0; cz < chunksZ; cz++)
            {
                Vector2 offset = defaultChunkData.NoiseOffset + new Vector2(cx * defaultChunkData.Width, cz * defaultChunkData.Depth);
                GameObject chunkObj = new GameObject($"Chunk_{cx}_{cz}");
                chunkObj.transform.position = new Vector3(cx * defaultChunkData.Width, 0, cz * defaultChunkData.Depth);
                chunkObj.transform.parent = transform;

                Generator.RenderChunk(chunkObj.transform, offset);
            }
    }
}
