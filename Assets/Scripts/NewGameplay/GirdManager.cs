using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab của tile
    public Sprite[] tileSprites; // Các sprite cho 7 loại tile
    public int rows = 5; // Số hàng
    public int cols = 5; // Số cột
    public float tileSpacing = 1.1f; // Khoảng cách giữa các tile

    private void Start()
    {
        GenerateGrid();
    }

    // Tạo lưới các tile
    void GenerateGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                // Tạo tile tại vị trí (x, y)
                var tile = Instantiate(tilePrefab, new Vector3(x * tileSpacing, y * tileSpacing, 0), Quaternion.identity);
                var typeIndex = Random.Range(0, tileSprites.Length);

                // Khởi tạo tile với loại và hình ảnh
                tile.GetComponent<Tile>().Initialize("Type" + typeIndex, tileSprites[typeIndex]);
            }
        }
    }
}
