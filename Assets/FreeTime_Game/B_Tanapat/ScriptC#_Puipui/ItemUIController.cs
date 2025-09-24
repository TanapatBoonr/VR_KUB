using UnityEngine;
using TMPro; // สำคัญ: ต้องเพิ่มบรรทัดนี้เพื่อใช้งาน TextMeshPro

public class ItemUIController : MonoBehaviour
{
    // ตัวแปรสำหรับอ้างอิงถึง Text บน UI
    // เราจะลาก TextMeshProUGUI มาใส่ใน Inspector
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    // ฟังก์ชัน Public ที่เราจะเรียกใช้จากสคริปต์อื่น
    // เพื่ออัปเดตข้อมูลบน UI
    public void UpdateUI(string name, string description)
    {
        // ตรวจสอบว่ามี Text ที่อ้างอิงอยู่หรือไม่ ก่อนที่จะแก้ไขข้อความ
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