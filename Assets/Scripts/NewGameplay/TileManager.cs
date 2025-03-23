using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    private List<Tile> selectedTiles = new List<Tile>();
    public List<Transform> targetPositions = new List<Transform>();
    private List<bool> usedPositions = new List<bool>();

    public int score = 0;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < targetPositions.Count; i++)
        {
            usedPositions.Add(false);
        }
    }

    public void SelectTile(Tile tile)
    {
       
        int availableIndex = FindNextAvailablePosition();
        if (availableIndex == -1)
        {
            Debug.Log("Game Over! Không còn vị trí trống.");
            return;
        }

        tile.transform.position = targetPositions[availableIndex].position;
        usedPositions[availableIndex] = true; 

        selectedTiles.Add(tile);

        Debug.Log($"Tile {tile.type} đã được thêm vào vị trí {availableIndex}");

      
        CheckMatch();
    }

    private int FindNextAvailablePosition()
    {
        for (int i = 0; i < usedPositions.Count; i++)
        {
            if (!usedPositions[i])
                return i;
        }
        return -1; 
    }

    private void CheckMatch()
    {
      
        Dictionary<string, int> tileCount = new Dictionary<string, int>();

        foreach (var tile in selectedTiles)
        {
            if (!tileCount.ContainsKey(tile.type))
                tileCount[tile.type] = 0;

            tileCount[tile.type]++;
        }

        foreach (var pair in tileCount)
        {
            if (pair.Value >= 3)
            {
                Debug.Log($"Matched 3 tiles: {pair.Key}");
                score += 10;
                RemoveMatchedTiles(pair.Key); 
                break;
            }
        }
    }

    private void RemoveMatchedTiles(string type)
    {
      
        List<Tile> matchedTiles = selectedTiles.FindAll(tile => tile.type == type);

        foreach (var tile in matchedTiles)
        {
           
            int index = targetPositions.FindIndex(pos => pos.position == tile.transform.position);
            if (index != -1)
            {
                usedPositions[index] = false; 
            }

            Destroy(tile.gameObject);
        }

      
        selectedTiles.RemoveAll(tile => tile.type == type);

       
        ReorganizeTiles();

        Debug.Log("Matched tiles đã được xóa và vị trí được giải phóng.");
    }

    private void ReorganizeTiles()
    {
        List<Tile> remainingTiles = new List<Tile>(selectedTiles);
        selectedTiles.Clear();

        for (int i = 0; i < usedPositions.Count; i++)
        {
            usedPositions[i] = false;
        }

       
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
