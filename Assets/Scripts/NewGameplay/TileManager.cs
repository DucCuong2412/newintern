using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance; // Singleton

    private List<Tile> selectedTiles = new List<Tile>(); // Danh sách các tile đã chọn
    public List<Transform> targetPositions = new List<Transform>(); // Các vị trí đích
    private List<bool> usedPositions = new List<bool>(); // Theo dõi các vị trí đã sử dụng

    public int score = 0;

    private void Awake()
    {
        Instance = this;

        // Khởi tạo danh sách các vị trí trống ban đầu
        for (int i = 0; i < targetPositions.Count; i++)
        {
            usedPositions.Add(false); // Tất cả vị trí ban đầu đều trống
        }
    }

    public void SelectTile(Tile tile)
    {
        // Tìm vị trí trống đầu tiên
        int availableIndex = FindNextAvailablePosition();
        if (availableIndex == -1)
        {
            Debug.Log("Game Over! Không còn vị trí trống.");
            return;
        }

        // Đặt tile vào vị trí trống
        tile.transform.position = targetPositions[availableIndex].position;
        usedPositions[availableIndex] = true; // Đánh dấu vị trí này là đã được sử dụng

        // Thêm tile vào danh sách đã chọn
        selectedTiles.Add(tile);

        Debug.Log($"Tile {tile.type} đã được thêm vào vị trí {availableIndex}");

        // Kiểm tra xem có đủ 3 tile giống nhau để xóa hay không
        CheckMatch();
    }

    private int FindNextAvailablePosition()
    {
        // Tìm index đầu tiên chưa được sử dụng
        for (int i = 0; i < usedPositions.Count; i++)
        {
            if (!usedPositions[i])
                return i;
        }
        return -1; // Không tìm thấy vị trí trống
    }

    private void CheckMatch()
    {
        // Đếm số lượng từng loại tile
        Dictionary<string, int> tileCount = new Dictionary<string, int>();

        foreach (var tile in selectedTiles)
        {
            if (!tileCount.ContainsKey(tile.type))
                tileCount[tile.type] = 0;

            tileCount[tile.type]++;
        }

        // Kiểm tra các loại tile đủ 3 cái
        foreach (var pair in tileCount)
        {
            if (pair.Value >= 3)
            {
                Debug.Log($"Matched 3 tiles: {pair.Key}");
                score += 10;
                RemoveMatchedTiles(pair.Key); // Xóa các tile đã match
                break;
            }
        }
    }

    private void RemoveMatchedTiles(string type)
    {
        // Lấy danh sách các tile đã match
        List<Tile> matchedTiles = selectedTiles.FindAll(tile => tile.type == type);

        foreach (var tile in matchedTiles)
        {
            // Tìm vị trí tile này trên lưới
            int index = targetPositions.FindIndex(pos => pos.position == tile.transform.position);
            if (index != -1)
            {
                usedPositions[index] = false; // Đánh dấu vị trí này là trống
            }

            // Xóa tile khỏi scene
            Destroy(tile.gameObject);
        }

        // Xóa khỏi danh sách đã chọn
        selectedTiles.RemoveAll(tile => tile.type == type);

        // Sắp xếp lại các tile còn lại
        ReorganizeTiles();

        Debug.Log("Matched tiles đã được xóa và vị trí được giải phóng.");
    }

    private void ReorganizeTiles()
    {
        List<Tile> remainingTiles = new List<Tile>(selectedTiles);
        selectedTiles.Clear();

        // Xóa tất cả các vị trí đã sử dụng
        for (int i = 0; i < usedPositions.Count; i++)
        {
            usedPositions[i] = false;
        }

        // Sắp xếp lại các tile từ đầu
        for (int i = 0; i < remainingTiles.Count; i++)
        {
            Tile tile = remainingTiles[i];
            tile.transform.position = targetPositions[i].position;
            usedPositions[i] = true;
            selectedTiles.Add(tile);
        }

        Debug.Log("Đã sắp xếp lại các tile còn lại.");
    }
}
