using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWin : MonoBehaviour
{
   

    private void Start()
    {
        StartCoroutine(AutoSelectTiles());
    }

    IEnumerator AutoSelectTiles()
    {
        while (true)
        {
           
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

           
            if (tiles.Length == 0)
            {
                Debug.Log("Auto Win: Tất cả các tile đã được chọn và xóa. Bạn đã thắng!");
                yield break;
            }

           
            HashSet<string> tileTypes = new HashSet<string>();
            foreach (GameObject tileObj in tiles)
            {
                if (tileObj != null) 
                {
                    Tile tile = tileObj.GetComponent<Tile>();
                    if (tile != null && !tile.isSelected)
                    {
                        tileTypes.Add(tile.type); 
                    }
                }
            }

            
            foreach (string tileType in tileTypes)
            {
                Debug.Log($"Đang chọn tất cả tile loại: {tileType}");

              
                foreach (GameObject tileObj in tiles)
                {
                    if (tileObj != null) 
                    {
                        Tile tile = tileObj.GetComponent<Tile>();
                        if (tile != null && tile.type == tileType && !tile.isSelected)
                        {
                            
                            TileManager.Instance.SelectTile(tile);
                            Debug.Log($"Auto chọn tile: {tile.type}");

                          
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                }
            }
        }
    }
}
