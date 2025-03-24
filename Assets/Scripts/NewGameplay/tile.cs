using UnityEngine;

public class Tile : MonoBehaviour
{
    public string type; // Loại của tile (ví dụ: "banana", "strawberry", ...)
    private SpriteRenderer spriteRenderer;

    private Vector3 targetPosition; // Vị trí mục tiêu
    private bool isMoving = false; // Để kiểm tra xem tile có đang di chuyển không
    private float moveSpeed = 30f; // Tốc độ di chuyển
    public bool isSelected = false; // Cờ kiểm tra tile đã được chọn hay chưa

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(string tileType, Sprite tileSprite)
    {
        type = tileType;
        spriteRenderer.sprite = tileSprite; // Gán hình ảnh của tile
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
        isSelected = true; // Đánh dấu rằng tile này đã được chọn
    }

    private void Update()
    {
        if (isMoving)
        {
            // Di chuyển dần đến vị trí mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Nếu đã đến vị trí mục tiêu, dừng di chuyển
            if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void OnMouseDown()
    {
        // Nếu tile đã được chọn, không làm gì cả
        if (isSelected)
        {
            Debug.Log($"Tile {type} đã được chọn, không thể nhấn lại.");
            return;
        }

        // Khi người chơi nhấn vào tile, báo cho TileManager
        TileManager.Instance.SelectTile(this);
    }
}
