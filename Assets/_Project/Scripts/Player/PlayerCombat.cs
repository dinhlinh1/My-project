using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;

    [Header("Combat Settings")]
    public Transform attackPoint;      // Điểm đặt nắm đấm
    public float attackRange = 0.5f;   // Tầm đánh của tay
    public int damage = 10;            // Sát thương đấm
    public LayerMask enemyLayers;     // Lớp (Layer) của quái vật

    [Header("Dodge Settings")]
    public float dodgeSpeed = 15f;     // Tốc độ lướt
    public float dodgeDuration = 0.2f; // Thời gian lướt
    private bool isDodging = false;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        anim = GetComponent<Animator>(); // Lấy component Animator từ nhân vật
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Cập nhật tốc độ di chuyển vào Animator để chuyển từ Idle sang Run
        float moveSpeed = moveDirection.magnitude;
        anim.SetFloat("Speed", moveSpeed);

        
        if (isDodging) return; // Nếu đang né thì không nhận input khác

        // 1. Xử lý Di chuyển cơ bản (ví dụ đơn giản)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 2. Nhấn Mouse0 để Đấm
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        // 3. Nhấn LeftShift để Né
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveDirection.magnitude > 0)
        {
            StartCoroutine(Dodge());
        }
    }

    void Attack()
    {
        // Kích hoạt Trigger "Attack" trong Animator
        anim.SetTrigger("Attack");

        // Hiệu ứng đấm (Play Animation "Attack" ở đây)
        Debug.Log("Đấm!");

        // Phát hiện kẻ địch trong tầm đánh
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Trúng quái: " + enemy.name);
            // Gọi hàm nhận sát thương của quái (Ví dụ: enemy.GetComponent<Enemy>().TakeDamage(damage);)
        }
    }

    System.Collections.IEnumerator Dodge()
    {
        isDodging = true;
        float startTime = Time.time;

        anim.SetBool("isDodging", true); // Bật animation né

        while (Time.time < startTime + dodgeDuration)
        {
            // Di chuyển nhân vật cực nhanh theo hướng đang đi
            controller.Move(moveDirection * dodgeSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dodgeDuration);
        anim.SetBool("isDodging", false); // Tắt animation né

        isDodging = false;
    }

    // Vẽ vòng tròn tầm đánh để dễ quan sát trong Editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red; // Thêm màu đỏ cho dễ nhìn
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}