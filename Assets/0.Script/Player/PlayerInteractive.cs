using System;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField]
    private float maxCheckDistance = 3f;
    public event Action<ItemEffectSO> OnRayItem;
    public Transform curInteractGameObject;

    void LateUpdate()
    {
        CheckInfo();
    }

    void CheckInfo()
    {
        // 카메라 중앙을 중심으로 Ray를 생성
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // ray에 충돌된 게임오브젝트의 정보를 담아둘 변수
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) > 8f)
            {
                if (hit.collider.transform != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.transform;
                    OnRayItem?.Invoke(hit.collider.GetComponent<Item>().itemData);
                }
            }
            else
            {
                DeCheckInfo();
            }
        }
        else
        {
            DeCheckInfo();
        }
    }

    void DeCheckInfo()
    {
        curInteractGameObject = null;
        OnRayItem?.Invoke(null);
    }

}
