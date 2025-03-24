using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab; 
    public Sprite[] tileSprites; 
    public int rows = 5; 
    public int cols = 5; 
    public float tileSpacing = 1.1f;

    private void Start()
    {
        GenerateGrid();
    }

   
    void GenerateGrid()
    {
        int totalTiles = rows * cols; 
        int tileTypes = tileSprites.Length; 

      
        if (totalTiles % 3 != 0)
        {
            Debug.LogError("Tổng số tile phải chia hết cho 3 để đảm bảo các loại tile đủ số lượng.");
            return;
        }

      
        int baseCount = totalTiles / tileTypes; 

     
        List<int> tileCounts = new List<int>();
        for (int i = 0; i < tileTypes; i++)
        {
            tileCounts.Add(baseCount);
        }

      
        List<int> tileTypesList = new List<int>();
        for (int i = 0; i < tileCounts.Count; i++)
        {
            for (int j = 0; j < tileCounts[i]; j++)
            {
                tileTypesList.Add(i);
            }
        }

        ShuffleList(tileTypesList); 

  
        int index = 0;
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
             
                int typeIndex = tileTypesList[index];
                index++;

               
                var tile = Instantiate(tilePrefab, new Vector3(x * tileSpacing, y * tileSpacing, -5), Quaternion.identity);

               
                tile.GetComponent<Tile>().Initialize("Type" + typeIndex, tileSprites[typeIndex]);
            }
        }
    }

 
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
