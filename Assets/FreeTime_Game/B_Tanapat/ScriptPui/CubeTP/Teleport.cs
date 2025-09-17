using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // ตัวแปรเหล่านี้จะถูกกำหนดค่าใน Unity Inspector
    public Transform playerObject; 
    public Transform destinationObject; 

    // ตัวแปรสำหรับ Cooldown
    public float cooldownTime = 5.0f; // ตั้งค่าเวลาหน่วง (เป็นวินาที)
    private bool canTeleport = true; // สถานะการเทเลพอร์ต เริ่มต้นที่สามารถเทเลพอร์ตได้

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่เข้ามาใน Trigger มี Tag เป็น "Player"
        // และตรวจสอบว่าสามารถเทเลพอร์ตได้หรือไม่
        if (other.gameObject.CompareTag("Player") && canTeleport)
        {
            // ทำการเทเลพอร์ต
            playerObject.position = destinationObject.position;
            Debug.Log("Teleported player to " + destinationObject.name);

            // เมื่อเทเลพอร์ตเสร็จสิ้น ให้เปลี่ยนสถานะเป็นไม่สามารถเทเลพอร์ตได้
            canTeleport = false;

            // เริ่ม Coroutine เพื่อจัดการเวลา Cooldown
            StartCoroutine(TeleportCooldown());
        }
    }

    // Coroutine สำหรับการนับเวลา Cooldown
    IEnumerator TeleportCooldown()
    {
        // รอเป็นเวลาตามที่กำหนดใน cooldownTime
        yield return new WaitForSeconds(cooldownTime);

        // เมื่อครบกำหนดเวลา ให้เปลี่ยนสถานะกลับเป็นสามารถเทเลพอร์ตได้
        canTeleport = true;
    }
}