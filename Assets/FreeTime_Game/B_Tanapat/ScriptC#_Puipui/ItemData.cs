using UnityEngine;

public class ItemData : MonoBehaviour
{
    // ตัวแปรสำหรับเก็บข้อมูลของไอเทมแต่ละชิ้น
    // สามารถแก้ไขข้อมูลเหล่านี้ได้ใน Inspector
    public string itemName;
    public string itemDescription;

    // ตัวแปรสำหรับอ้างอิงถึง UI Prefab ที่เราจะสร้างขึ้นมา
    public GameObject uiPrefab;

    // ตัวแปรส่วนตัวเพื่อเก็บการอ้างอิงถึง UI ที่ถูกสร้างขึ้นในฉาก
    private GameObject currentUIInstance;

    // ฟังก์ชันนี้จะถูกเรียกเมื่อมี Collider อื่นเข้ามาในพื้นที่ Trigger
    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่า Collider ที่เข้ามานั้นมี Tag เป็น "Player"
        // (คุณต้องตั้ง Tag ของตัวละครผู้เล่นเป็น "Player" ด้วย)
        if (other.CompareTag("Player"))
        {
            // ถ้า UI ยังไม่ถูกสร้างขึ้น ให้สร้าง UI ใหม่
            if (currentUIInstance == null)
            {
                // สร้าง UI Prefab ขึ้นมาในฉาก
                // โดยให้มีตำแหน่งอยู่เหนือไอเทมเล็กน้อย
                currentUIInstance = Instantiate(uiPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

                // เรียกใช้ฟังก์ชัน UpdateUI จาก ItemUIController
                // เพื่อส่งข้อมูลชื่อและรายละเอียดของไอเทมไปแสดงผล
                ItemUIController controller = currentUIInstance.GetComponent<ItemUIController>();
                if (controller != null)
                {
                    controller.UpdateUI(itemName, itemDescription);
                }
            }
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อ Collider อื่นออกจากพื้นที่ Trigger
    private void OnTriggerExit(Collider other)
    {
        // ตรวจสอบว่า Collider ที่ออกไปนั้นเป็น "Player"
        if (other.CompareTag("Player"))
        {
            // ถ้า UI ยังคงอยู่ ให้ลบทิ้ง
            if (currentUIInstance != null)
            {
                Destroy(currentUIInstance);
            }
        }
    }
}