using UnityEngine;
using VoxelTerrain;

public class TerrainManipulatorInput : MonoBehaviour
{
    Camera mainCamera => Camera.main;
    [SerializeField] TerrainManipulator terrainManipulator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Break();
        }
    }
    void Break()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 worldPos = hit.point;
            terrainManipulator.Break(worldPos);
        }
    }
}
