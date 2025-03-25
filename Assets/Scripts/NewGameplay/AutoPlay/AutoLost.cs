using System.Collections;
using UnityEngine;

public class AutoLost : MonoBehaviour
{
   

    private void Start()
    {
        StartCoroutine(AutoSelectTiles());
    }

   IEnumerator AutoSelectTiles()
    {
        while (true)
        {
            // Tìm tất cả các Tile trên màn hình
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

            // Nếu không còn tile nào, dừng và báo thắng
            if (tiles.Length == 0)
            {
                Debug.Log("Auto Win: Tất cả các tile đã được chọn và xóa. Bạn đã thắng!");
                yield break;
            }

            bool foundTileToSelect = false;

            // Duyệt qua tất cả các tile
            foreach (GameObject tileObj in tiles)
            {
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile != null && !tile.isSelected) // Chỉ chọn tile chưa được chọn
                {
                    // Gọi hàm SelectTile từ TileManager
                    TileManager.Instance.SelectTile(tile);
                    Debug.Log($"Auto chọn tile: {tile.type}");

                    foundTileToSelect = true;

                    // Chờ một khoảng thời gian trước khi tiếp tục
                    yield return new WaitForSeconds(0.5f);

                    break; // Thoát khỏi vòng lặp sau khi chọn 1 tile
                }
            }

            // Nếu không tìm được tile nào hợp lệ để chọn, thoát vòng lặp
            if (!foundTileToSelect)
            {
                Debug.Log("Không còn tile nào có thể chọn.");
                yield break;
            }
        }
    }
}
