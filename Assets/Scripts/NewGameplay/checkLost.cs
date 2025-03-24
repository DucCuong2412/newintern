using System.Collections;
using UnityEngine;

public class checkLost : MonoBehaviour
{
    public static checkLost Instance;

    private void Start()
    {
        StartCoroutine(waitforMinute());
    }
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

    IEnumerator waitforMinute()
    {
        Debug.Log("đã hết 1 phút)");

        yield return new WaitForSeconds(60);
    }
}
