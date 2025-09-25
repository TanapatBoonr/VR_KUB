using UnityEngine;
using TMPro;

public class ItemUIController : MonoBehaviour
{
    // ตัวแปรสำหรับอ้างอิงถึง Text บน UI
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    // ตัวแปรส่วนตัวสำหรับเก็บการอ้างอิงถึงกล้องหลักของผู้เล่น
    private Transform mainCameraTransform;

    // ฟังก์ชัน Awake จะถูกเรียกก่อน Start()
    void Awake()
    {
        // ค้นหากล้องหลักของโปรเจกต์และเก็บการอ้างอิงไว้
        // **สำคัญ**: XR Interaction Toolkit มักจะใช้ Main Camera เป็นกล้องหลัก
        // หรือ XR Origin ที่มีกล้อง
        mainCameraTransform = Camera.main.transform;
    }

    // ฟังก์ชัน Update จะถูกเรียกทุกเฟรม
void Update()
{
    // ทำให้ UI หันหน้าเข้าหากล้อง
    if (mainCameraTransform != null)
    {
        // คำนวณทิศทางจาก UI ไปหากล้อง
        Vector3 directionToCamera = mainCameraTransform.position - transform.position;

        // สร้างการหมุน (Rotation) เพื่อให้แกน Z ของ UI หันไปในทิศทางนั้น
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

        // **แก้ไข**: ทำให้ UI หันกลับด้าน 180 องศาบนแกน Y เพื่อแก้ไขการกลับหัว
        // การคูณ Quaternion.Euler(0, 180, 0) จะเป็นการพลิกด้าน
        transform.rotation = lookRotation * Quaternion.Euler(0, 180, 0);
    }
}

    // ฟังก์ชันสำหรับอัปเดตข้อความบน UI
    public void UpdateUI(string name, string description)
    {
        if (itemNameText != null)
        {
            itemNameText.text = name;
        }

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = description;
        }
    }
}