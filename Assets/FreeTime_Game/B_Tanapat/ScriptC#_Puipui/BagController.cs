using UnityEngine;
using UnityEngine.UI;

public class BagController : MonoBehaviour
{
    public GameObject topBagpack;
    public GameObject socketsParent;

    // ตัวแปรส่วนตัวสำหรับอ้างอิงถึงปุ่มที่ถูกสร้างโดย UIBagTrigger
    private Button openButton;
    private Button closeButton;

    // ฟังก์ชันสำหรับเปิดกระเป๋า
    public void OpenBag()
    {
        topBagpack.SetActive(false);
        if (socketsParent != null)
        {
            socketsParent.SetActive(true);
        }
        // ซ่อนปุ่ม Open และแสดงปุ่ม Close
        if (openButton != null) openButton.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(true);
    }

    // ฟังก์ชันสำหรับปิดกระเป๋า
    public void CloseBag()
    {
        topBagpack.SetActive(true);
        if (socketsParent != null)
        {
            socketsParent.SetActive(false);
        }
        // ซ่อนปุ่ม Close และแสดงปุ่ม Open
        if (openButton != null) openButton.gameObject.SetActive(false); // ซ่อนปุ่ม Open เพื่อป้องกันการแสดงผล
        if (closeButton != null) closeButton.gameObject.SetActive(false); // ซ่อนปุ่ม Close
    }
}