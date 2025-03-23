using UnityEngine;

public class TileContainer : MonoBehaviour
{
    public static TileContainer Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
