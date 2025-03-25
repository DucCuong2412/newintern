using UnityEngine;

public class Tile : MonoBehaviour
{
    public string type; 
    private SpriteRenderer spriteRenderer;

    private Vector3 targetPosition; 
    private bool isMoving = false; 
    private float moveSpeed = 30f; 
    public bool isSelected = false; 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(string tileType, Sprite tileSprite)
    {
        type = tileType;
        spriteRenderer.sprite = tileSprite; 
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
        isSelected = true; 
    }

    private void Update()
    {
        if (isMoving)
        {
          
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void OnMouseDown()
    {
       
        if (isSelected)
        {
            Debug.Log($"Tile {type} đã được chọn, không thể nhấn lại.");
            return;
        }

       
        TileManager.Instance.SelectTile(this);
    }
}
