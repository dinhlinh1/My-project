using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    // Tên vật phẩm sẽ hiện lên màn hình
    public string tenVatPham = "Pha Lê";

    // Hàm này sẽ được gọi khi nhân vật bấm E
    public void OnInteract()
    {
        // Tạo hiệu ứng âm thanh hoặc nổ ở đây (nếu có sau này)

        // Hủy vật phẩm (Biến mất)
        Destroy(gameObject);
    }
}