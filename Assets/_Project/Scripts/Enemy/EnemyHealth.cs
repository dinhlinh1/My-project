using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    public EnemyHealthBar healthBar;
    private bool isDead = false;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (healthBar != null) healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Nếu đã chết thì không nhận thêm sát thương

        currentHealth -= damage;
        if (healthBar != null) healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die(); // Chỉ gọi hàm chết
        }
        else
        {
            anim.SetTrigger("Hit"); // Chỉ chạy anim trúng đòn khi còn máu
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die"); // Dùng Trigger thay vì Bool để phản ứng nhanh hơn
        GetComponent<Collider>().enabled = false;
        if (healthBar != null) healthBar.gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }
}