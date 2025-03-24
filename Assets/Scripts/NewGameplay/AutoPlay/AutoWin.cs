using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWin : MonoBehaviour
{
    public float delayBetweenSelections = 0.5f; // Thời gian trễ giữa mỗi lần chọn tile

    private void Start()
    {
        StartCoroutine(AutoSelectTiles());
    }

    IEnumerator AutoSelectTiles()
    {
        while (true)
        {
            // Tìm tất cả các Tile còn lại trên màn hình
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

            // Nếu không còn tile nào, dừng và báo thắng
            if (tiles.Length == 0)
            {
                Debug.Log("Auto Win: Tất cả các tile đã được chọn và xóa. Bạn đã thắng!");
                yield break;
            }

            // Tạo danh sách các loại (type) tile còn lại
            HashSet<string> tileTypes = new HashSet<string>();
            foreach (GameObject tileObj in tiles)
            {
                if (tileObj != null) // Kiểm tra tileObj còn tồn tại
                {
                    Tile tile = tileObj.GetComponent<Tile>();
                    if (tile != null && !tile.isSelected)
                    {
                        tileTypes.Add(tile.type); // Thêm loại tile vào danh sách
                    }
                }
            }

            // Lặp qua từng loại tile
            foreach (string tileType in tileTypes)
            {
                Debug.Log($"Đang chọn tất cả tile loại: {tileType}");

                // Lấy danh sách tất cả các tile cùng loại
                foreach (GameObject tileObj in tiles)
                {
                    if (tileObj != null) // Kiểm tra tileObj còn tồn tại
                    {
                        Tile tile = tileObj.GetComponent<Tile>();
                        if (tile != null && tile.type == tileType && !tile.isSelected)
                        {
                            // Chọn tile
                            TileManager.Instance.SelectTile(tile);
                            Debug.Log($"Auto chọn tile: {tile.type}");

                            // Chờ một khoảng thời gian trước khi chọn tile tiếp theo
                            yield return new WaitForSeconds(delayBetweenSelections);
                        }
                    }
                }
            }
        }
    }
}
