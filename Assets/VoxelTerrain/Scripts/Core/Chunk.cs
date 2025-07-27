using System;
using System.Collections.Generic;
using UnityEngine;
namespace VoxelTerrain
{
    public class Chunk
    {
        public readonly int width;
        public readonly int height;
        public readonly int depth;
        public Transform Parent;

        public Voxel[,,] voxels;

        Mesh mesh;
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;
        MeshCollider meshCollider;
        List<Vector3> vertices = new();
        List<int> triangles = new();
        // List<Vector2> uvs = new();

        List<Color> colors = new List<Color>();



        public Vector2Int chunkCoord;


        public void SetMeshFilter(MeshFilter mf) => meshFilter = mf;
        public void SetMeshRenderer(MeshRenderer mr) => meshRenderer = mr;
        public void SetMeshCollider(MeshCollider mc) => meshCollider = mc;
        public MeshFilter GetMeshFilter() => meshFilter;
        public MeshRenderer GetMeshRender() => meshRenderer;

        static readonly int[][] FaceTriangles = new int[][]
        {
            new int[] { 0, 1, 2, 3 }, // Back
            new int[] { 5, 4, 7, 6 }, // Front
            new int[] { 4, 0, 3, 7 }, // Left
            new int[] { 1, 5, 6, 2 }, // Right
            new int[] { 3, 2, 6, 7 }, // Top
            new int[] { 4, 5, 1, 0 }, // Bottom
        };

        public Chunk(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            voxels = new Voxel[width, height, depth];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    for (int z = 0; z < depth; z++)
                        voxels[x, y, z] = new Voxel(new Vector3Int(x, y, z));
        }

        public void SetVoxel(int x, int y, int z, VoxelType type)
        {
            if (InBounds(x, y, z))
            {
                voxels[x, y, z].Type = type;
            }
        }

        public Voxel GetVoxel(int x, int y, int z)
        {
            if (InBounds(x, y, z))
                return voxels[x, y, z];
            return null;
        }
        public Voxel GetVoxel(Vector3 worldPos)
        {
            var localPos = meshRenderer.transform.InverseTransformPoint(worldPos);
            var size = DataHelper.Instance.VoxelChunkData.VoxelSize;
            int x = Mathf.FloorToInt(localPos.x / size);
            int y = Mathf.FloorToInt(localPos.y / size);
            int z = Mathf.FloorToInt(localPos.z / size);
            return GetVoxel(x, y, z);
        }

        bool InBounds(int x, int y, int z)
        {
            return x >= 0 && x < width &&
                   y >= 0 && y < height &&
                   z >= 0 && z < depth;
        }
        bool InBounds(Vector3Int pos)
        {
            return pos.x >= 0 && pos.x < width &&
                   pos.y >= 0 && pos.y < height &&
                   pos.z >= 0 && pos.z < depth;
        }

        public void AddVoxelMesh(Voxel voxel)
        {
            Vector3 basePos = (Vector3)voxel.Position * DataHelper.Instance.VoxelChunkData.VoxelSize;
            int vCount = vertices.Count;

            for (int face = 0; face < 6; face++)
            {
                Vector3Int neighborPos = voxel.Position + GetDirection(face);
                if (!IsVoxelSolid(neighborPos))
                {
                    int[] faceVerts = FaceTriangles[face];

                    for (int i = 0; i < 4; i++)
                        vertices.Add(basePos + voxel.Vertices[faceVerts[i]]);

                    triangles.Add(vCount + 2);
                    triangles.Add(vCount + 1);
                    triangles.Add(vCount + 0);
                    triangles.Add(vCount + 0);
                    triangles.Add(vCount + 3);
                    triangles.Add(vCount + 2);

                    vCount += 4;

                    var color = DataHelper.Instance.VoxelObjectDataSet.GetColorByVoxelType(voxel.Type);
                    for (int i = 0; i < 4; i++)
                        colors.Add(color);
                }
            }
        }
        public bool IsVoxelSolid(Vector3Int pos)
        {
            if (InBounds(pos))
                return voxels[pos.x, pos.y, pos.z].IsSolid;

            Vector3Int localPos = pos;
            Vector2Int neighborChunkCoord = TerrainGenerator.Instance.Generator.GetNeighborChunkCoord(ref localPos, chunkCoord);
            if (TerrainGenerator.Instance.Chunks.TryGetValue(neighborChunkCoord, out Chunk neighborChunk))
            {
                if (neighborChunk.InBounds(localPos))
                    return neighborChunk.voxels[localPos.x, localPos.y, localPos.z].IsSolid;
            }
            return false;
        }


        public void UpdateMesh()
        {
            if (mesh == null)
                mesh = new Mesh();
            else
                mesh.Clear();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetColors(colors);
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            vertices.Clear();
            triangles.Clear();
            // uvs.Clear();
            colors.Clear();
        }

        Vector3Int GetDirection(int face)
        {
            return face switch
            {
                0 => Vector3Int.back,
                1 => Vector3Int.forward,
                2 => Vector3Int.left,
                3 => Vector3Int.right,
                4 => Vector3Int.up,
                5 => Vector3Int.down,
                _ => Vector3Int.zero
            };
        }

        public void BreakVoxel(Voxel voxel)
        {
            voxel.Type = VoxelType.Air;
            UpdateMesh();
        }
    }

}
