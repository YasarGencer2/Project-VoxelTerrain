using UnityEngine; 
namespace VoxelTerrain
{
    public class TerrainManipulator : MonoBehaviour
    {

        public Chunk FindChunk(Vector3 worldPos)
        {
            var chunkCoord = TerrainGenerator.Instance.GetChunkIndex(worldPos);
            return FindChunk(chunkCoord);
        }
        public Chunk FindChunk(Vector2Int chunkCoord)
        {
            if (TerrainGenerator.Instance.Chunks.TryGetValue(chunkCoord, out Chunk chunk))
            {
                return chunk;
            }
            return null;
        }
        public void Break(Vector3 worldPos)
        {  
            var chunk = FindChunk(worldPos);
            print($"Found {chunk}");
            if (chunk == null)
                return;
            var voxel = chunk.GetVoxel(worldPos);
            Break(chunk, voxel);
        }
        public void Break(Chunk chunk, Voxel voxel)
        {
            if (voxel == null || !voxel.IsSolid)
                return;
            chunk.BreakVoxel(voxel);
        }
    }
}
