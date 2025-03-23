using UnityEngine;

public class Tile : MonoBehaviour
{
    public string type; // Loại của tile (ví dụ: "banana", "strawberry", ...)
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(string tileType, Sprite tileSprite)
    {
        type = tileType;
        spriteRenderer.sprite = tileSprite; // Gán hình ảnh của tile
    }

    private void OnMouseDown()
    {
        // Khi người chơi nhấn vào tile, báo cho TileManager
        TileManager.Instance.SelectTile(this);
    }
}
