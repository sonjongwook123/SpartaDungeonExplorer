using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class DynamicTerrain : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    [Header("Baking Settings")]
    public float updateDelay = 0.5f;
    private bool needsUpdate = false;


    public void TerrainChanged()
    {
        needsUpdate = true;
    }


    private void FixedUpdate()
    {
        if (needsUpdate)
        {
            StartCoroutine(UpdateNavMeshAfterDelay());
            needsUpdate = false; // 플래그 초기화
        }
    }

    private IEnumerator UpdateNavMeshAfterDelay()
    {
        yield return new WaitForSeconds(updateDelay);

        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }

}
