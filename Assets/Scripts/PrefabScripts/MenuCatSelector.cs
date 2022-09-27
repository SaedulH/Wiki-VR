using UnityEngine;
using TMPro;

public class MenuCatSelector : MonoBehaviour
{

    [SerializeField]
    private StringSO SO;

    public void getCategory()
    {
        SO.Cat = GetComponentInChildren<TextMeshProUGUI>().text; 
        Debug.Log(SO.Cat);
    }

}
