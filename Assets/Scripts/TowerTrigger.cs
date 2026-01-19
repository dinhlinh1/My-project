using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [Header("KẾT NỐI")]
    public GameObject denPhatSang;   // Đèn của tháp này
    public SimpleGate congChuong2;   // Cổng quản lý chung

    private bool playerOTrong = false; // Player có ở gần không?
    private bool dangBatDen = false;   // Trạng thái hiện tại của đèn (Mặc định là tắt)

    void Update()
    {
        // Nếu ở trong vùng + Nhấn E
        if (playerOTrong && Input.GetKeyDown(KeyCode.E))
        {
            DaoNguocCongTac();
        }
    }

    void DaoNguocCongTac()
    {
        // Đảo ngược trạng thái: Đang tắt thành bật, đang bật thành tắt
        dangBatDen = !dangBatDen;

        // 1. Xử lý cái đèn (Bật hoặc Tắt theo biến dangBatDen)
        if (denPhatSang != null)
        {
            denPhatSang.SetActive(dangBatDen);
        }

        // 2. Báo cáo về Cổng (Gửi trạng thái mới đi)
        if (congChuong2 != null)
        {
            congChuong2.CapNhatTrangThaiThap(dangBatDen);
        }

        // In ra console để kiểm tra
        if (dangBatDen) Debug.Log(">>> Đã BẬT đèn!");
        else Debug.Log(">>> Đã TẮT đèn!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            playerOTrong = true;
            Debug.Log("Nhấn E để Bật/Tắt đèn");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            playerOTrong = false;
        }
    }
}