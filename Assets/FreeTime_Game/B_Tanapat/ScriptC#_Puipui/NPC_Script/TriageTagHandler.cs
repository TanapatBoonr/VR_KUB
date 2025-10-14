using UnityEngine;

// ***************************************************************
// หมายเหตุ: สคริปต์นี้สมมติว่าคุณได้สร้างไฟล์ TriageEnums.cs แล้ว
// และ TriageColor ถูกประกาศอยู่ในไฟล์นั้น
// ***************************************************************

public class TriageTagHandler : MonoBehaviour
{
    // กำหนดสีของบัตร Triage นี้ใน Inspector
    // (ใช้ TriageColor ที่ประกาศใน TriageEnums.cs)
    public TriageColor tagColor;

    // เมื่อป้าย Tag ชน (Collide) กับ NPC ที่มี Collider แบบ Is Trigger
    void OnTriggerEnter(Collider other)
    {
        // 1. พยายามดึงสคริปต์ PatientController จาก GameObject ที่ชน
        
        // ใช้ GreenPatientController เป็นตัวอย่าง
        GreenPatientController patient = other.GetComponent<GreenPatientController>();
        
        if (patient != null) 
        {
            // 2. สั่งให้ NPC รับ Tag สีนี้
            // ส่งค่า Enum TriageColor ในรูปแบบ string ไป
            patient.ReceiveTriageTag(tagColor.ToString()); 

            // 3. (Optional) ติดบัตรนี้เข้ากับ NPC
            
            // ยกเลิกการควบคุม RigidBody 
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            // Parent บัตรให้ติดกับส่วนใดส่วนหนึ่งของ NPC (เช่น ตัว Body)
            Transform patientBody = other.transform.Find("Body"); 
            if (patientBody != null)
            {
                transform.SetParent(patientBody);
            }
            else
            {
                transform.SetParent(other.transform);
            }
            
            // ปิดการทำงานของ Collider เพื่อไม่ให้เกิด Trigger ซ้ำซ้อน
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }
}