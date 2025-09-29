using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // ค้นหากล้องหลักของโปรเจกต์
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // ทำให้ UI หันหน้าเข้าหากล้อง
        if (mainCameraTransform != null)
        {
            Vector3 directionToCamera = mainCameraTransform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = lookRotation * Quaternion.Euler(0, 180, 0);
        }
    }
}