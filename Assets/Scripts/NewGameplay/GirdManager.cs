using UnityEngine;
using System.Collections.Generic;

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
        int totalTiles = rows * cols; // Tổng số lượng tile
        int tileTypes = tileSprites.Length; // Số loại tile (7 loại)

        // Kiểm tra nếu tổng số lượng tile không thể chia hết cho số loại
        if (totalTiles % 3 != 0)
        {
            Debug.LogError("Tổng số tile phải chia hết cho 3 để đảm bảo các loại tile đủ số lượng.");
            return;
        }

        // Tính số lượng tile cần sinh ra cho mỗi loại
        int baseCount = totalTiles / tileTypes; // Số lượng cơ bản cho mỗi loại

        // Danh sách số lượng tile cho từng loại (đều bằng baseCount)
        List<int> tileCounts = new List<int>();
        for (int i = 0; i < tileTypes; i++)
        {
            tileCounts.Add(baseCount);
        }

        // Trộn danh sách loại tile
        List<int> tileTypesList = new List<int>();
        for (int i = 0; i < tileCounts.Count; i++)
        {
            for (int j = 0; j < tileCounts[i]; j++)
            {
                tileTypesList.Add(i);
            }
        }

        ShuffleList(tileTypesList); // Trộn danh sách

        // Sinh tile vào grid
        int index = 0;
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                // Lấy loại tile từ danh sách đã trộn
                int typeIndex = tileTypesList[index];
                index++;

                // Tạo tile tại vị trí (x, y)
                var tile = Instantiate(tilePrefab, new Vector3(x * tileSpacing, y * tileSpacing, 0), Quaternion.identity);

                // Khởi tạo tile với loại và hình ảnh
                tile.GetComponent<Tile>().Initialize("Type" + typeIndex, tileSprites[typeIndex]);
            }
        }
    }

    // Hàm trộn danh sách ngẫu nhiên
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
