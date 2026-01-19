using UnityEngine;

public class SimpleGate : MonoBehaviour
{
    [Header("CÀI ĐẶT CỔNG")]
    public GameObject congCanDuong; // Kéo bức tường chặn đường vào đây
    public int tongSoThap = 2;      // Số tháp cần kích hoạt đồng thời

    // Biến đếm số tháp đang sáng
    private int soThapDangSang = 0;

    // Hàm này nhận tín hiệu từ các tháp (true = vừa bật, false = vừa tắt)
    public void CapNhatTrangThaiThap(bool denVuaBat)
    {
        if (denVuaBat)
        {
            soThapDangSang++; // Cộng thêm 1
        }
        else
        {
            soThapDangSang--; // Trừ đi 1
        }

        Debug.Log(">>> Tiến độ: " + soThapDangSang + "/" + tongSoThap + " tháp đang sáng.");

        KiemTraCua();
    }

    void KiemTraCua()
    {
        // Nếu đủ số lượng tháp sáng -> Mở cửa
        if (soThapDangSang >= tongSoThap)
        {
            Debug.Log(">>> ĐỦ ĐIỀU KIỆN: MỞ CỔNG!");
            if (congCanDuong != null) congCanDuong.SetActive(false); // Tắt tường
        }
        else
        {
            // Nếu không đủ (do vừa tắt bớt đèn) -> Đóng cửa lại
            Debug.Log(">>> CHƯA ĐỦ ĐÈN: ĐÓNG CỔNG!");
            if (congCanDuong != null) congCanDuong.SetActive(true); // Bật tường lại
        }
    }
}