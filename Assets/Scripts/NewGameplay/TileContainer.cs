using UnityEngine;

public class TileContainer : MonoBehaviour
{
    public static TileContainer Instance; // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Bạn có thể thêm các chức năng quản lý hiển thị tile ở đây nếu cần
}
