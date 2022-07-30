using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCatSelector : MonoBehaviour
{

    [SerializeField]
    private StringSO catNameSO;

    public void getCategory()
    {
        catNameSO.Cat = GetComponentInChildren<TextMeshProUGUI>().text; 
        Debug.Log(catNameSO.Cat);
    }

}
