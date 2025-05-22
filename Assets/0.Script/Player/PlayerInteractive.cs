using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractive : MonoBehaviour
{
    public LayerMask itemlayerMask;
    public LayerMask interactlayerMask;
    [SerializeField]
    private float maxCheckDistance = 3f;
    public event Action<ItemEffectSO> OnRayItem;
    public event Action<bool> OnRayInteract;
    public Transform curInteractGameObject;

    void LateUpdate()
    {
        CheckItem();
    }

    void CheckItem()
    {
        // 카메라 중앙을 중심으로 Ray를 생성
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // ray에 충돌된 게임오브젝트의 정보를 담아둘 변수
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, itemlayerMask))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) > 6f)
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
        else if (Physics.Raycast(ray, out hit, maxCheckDistance, interactlayerMask))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) > 6f)
            {
                if (hit.collider.transform != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.transform;
                    OnRayInteract?.Invoke(true);
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
        OnRayInteract?.Invoke(false);
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curInteractGameObject != null
        && curInteractGameObject.GetComponent<Interactable>() != null)
        {
            curInteractGameObject.GetComponent<Interactable>().Interact();
        }
    }
}
