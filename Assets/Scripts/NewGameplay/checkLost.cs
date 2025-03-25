using System.Collections;
using UnityEngine;

public class checkLost : MonoBehaviour
{
    public static checkLost Instance;
    public GameObject panelLost;


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

        yield return new WaitForSeconds(60f);
        Debug.Log("đã hết 1 phút)");
        panelLost.SetActive(true);
    }
}
