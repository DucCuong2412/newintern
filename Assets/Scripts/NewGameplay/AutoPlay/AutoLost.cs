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
            // find tile
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

           
            if (tiles.Length == 0)
            {
                Debug.Log("Auto Win: Tất cả các tile đã được chọn và xóa. Bạn đã thắng!");
                yield break;
            }

            bool foundTileToSelect = false;

          
            foreach (GameObject tileObj in tiles)
            {
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile != null && !tile.isSelected) 
                {
                  
                    TileManager.Instance.SelectTile(tile);
                    Debug.Log($"Auto chọn tile: {tile.type}");

                    foundTileToSelect = true;

                   
                    yield return new WaitForSeconds(0.5f);

                    break; 
                }
            }

            
            if (!foundTileToSelect)
            {
                Debug.Log("Không còn tile nào có thể chọn.");
                yield break;
            }
        }
    }
}
