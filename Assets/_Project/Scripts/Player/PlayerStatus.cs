using UnityEngine;
using UnityEngine.UI; // Cần thư viện này để dùng Slider

public class PlayerStatus : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegenRate = 5f;

    [Header("UI References")]
    public Slider healthSlider;
    public Slider staminaSlider;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;

        // Khởi tạo giá trị UI
        if (healthSlider) healthSlider.maxValue = maxHealth;
        if (staminaSlider) staminaSlider.maxValue = maxStamina;
    }

    void Update()
    {
        // Tự động hồi thể lực
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        // Cập nhật UI mỗi khung hình
        if (healthSlider) healthSlider.value = currentHealth;
        if (staminaSlider) staminaSlider.value = currentStamina;
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            return true;
        }
        Debug.Log("Không đủ thể lực!");
        return false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Người chơi đã chết!");
        // Bạn có thể chạy animation chết của Starter Asset tại đây
    }
}