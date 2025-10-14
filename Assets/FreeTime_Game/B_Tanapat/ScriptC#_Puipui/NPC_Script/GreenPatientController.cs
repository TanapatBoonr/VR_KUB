using UnityEngine;
using UnityEngine.AI; 

// *ไม่ต้องเพิ่ม TriageColor เข้าไปในไฟล์นี้แล้ว เพราะมันอยู่ใน TriageEnums.cs*

public class GreenPatientController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // พารามิเตอร์ใน Animator
    private const string PARAM_MOVE = "Move"; 
    
    // การตั้งค่าใน Inspector (เหลือแค่ตัวแปรที่จำเป็น)
    public Transform greenTreatmentArea; 
    
    // สถานะของ NPC
    private bool hasStoodUp = false;
    private bool isTagged = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false; 
            animator.Play("Sitting");
        }
    }

    void OnEnable()
    {
        MegaphoneController.OnMegaphoneStateChanged += OnMegaphoneStateChanged;
    }

    void OnDisable()
    {
        MegaphoneController.OnMegaphoneStateChanged -= OnMegaphoneStateChanged;
    }

    private void OnMegaphoneStateChanged(bool isActive)
    {
        if (isActive && !hasStoodUp)
        {
            Debug.Log(gameObject.name + " ได้ยินโทรโข่ง: เริ่ม Standing Up");
            StartStandingUpSequence();
        }
        else if (!isActive && hasStoodUp && !isTagged)
        {
            StopMovementAndGoIdle();
        }
    }

    private void StartStandingUpSequence()
    {
        animator.Play("Standing Up");
        hasStoodUp = true;
        
        Invoke("StartInitialMovement", 1.5f); 
    }
    
    private void StartInitialMovement()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true; 
            
            Vector3 walkDestination = transform.position + transform.forward * 5f; 
            navMeshAgent.SetDestination(walkDestination);
            
            animator.SetBool(PARAM_MOVE, true); 
            
            Invoke("StopMovementAndGoIdle", 5f); 
        }
    }

    private void StopMovementAndGoIdle()
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            animator.SetBool(PARAM_MOVE, false); 
        }
    }

    // รับค่าสี Tag ที่ส่งมาจาก TriageTagHandler.cs
    public void ReceiveTriageTag(string tagReceived)
    {
        // NPC สีเขียวต้องถูกติด Tag สีเขียวเท่านั้น
        // แก้ไข: TriageColor ถูกดึงมาจาก TriageEnums.cs
        if (tagReceived != TriageColor.Green.ToString())
        {
            Debug.Log(gameObject.name + " ถูกติด Tag ผิดสี: " + tagReceived);
            return; 
        }

        if (isTagged) return; 

        isTagged = true;
    
        Debug.Log("Player ได้คะแนนจากการติด Green Tag"); 

        if (greenTreatmentArea != null)
        {
            if (navMeshAgent != null)
            {
                CancelInvoke(); 
                navMeshAgent.enabled = true;
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(greenTreatmentArea.position);
                animator.SetBool(PARAM_MOVE, true); 
            }
        }
    }
}