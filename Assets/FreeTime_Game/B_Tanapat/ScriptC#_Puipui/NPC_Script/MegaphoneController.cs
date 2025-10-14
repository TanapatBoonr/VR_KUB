using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // ต้องมี XR Interaction Toolkit

// สคริปต์นี้จะถูกเรียกเมื่อ Player หยิบโทรโข่ง
public class MegaphoneController : MonoBehaviour
{
    // Event ที่จะแจ้งให้ NPC ทราบสถานะของโทรโข่ง
    public static event System.Action<bool> OnMegaphoneStateChanged;

    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    public AudioClip commandClip; // เสียง "ใครเดินได้ เดินออกมาก่อนเลยครับ"

    void Awake()
    {
        // ตรวจสอบว่ามี AudioSource และ XR Grab Interactable
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // ผูกฟังก์ชันเมื่อผู้เล่นหยิบ/วางโทรโข่ง
            grabInteractable.selectEntered.AddListener(OnPickedUp);
            grabInteractable.selectExited.AddListener(OnPutDown);
        }
    }

    // -----------------------------------------------------------
    // 2.2.1: เมื่อหยิบโทรโข่ง
    // -----------------------------------------------------------
    private void OnPickedUp(SelectEnterEventArgs args)
    {
        if (audioSource != null && commandClip != null)
        {
            audioSource.clip = commandClip;
            audioSource.loop = true; // Loop ไปเรื่อยๆ
            audioSource.Play();
        }

        // แจ้งให้ NPC ทุกตัวทราบว่าโทรโข่งถูกหยิบขึ้นมา (Active)
        if (OnMegaphoneStateChanged != null)
        {
            OnMegaphoneStateChanged.Invoke(true);
        }
    }

    // -----------------------------------------------------------
    // 2.2.6: ถ้า Player ไม่หยิบ โทรโข่ง ขึ้นมา (หรือวางลง)
    // -----------------------------------------------------------
    private void OnPutDown(SelectExitEventArgs args)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        // แจ้งให้ NPC ทุกตัวทราบว่าโทรโข่งถูกวางลงแล้ว (Inactive)
        if (OnMegaphoneStateChanged != null)
        {
            OnMegaphoneStateChanged.Invoke(false);
        }
    }
}