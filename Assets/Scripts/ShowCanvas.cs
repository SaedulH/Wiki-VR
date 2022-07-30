using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowCanvas : MonoBehaviour
{
    [SerializeField]
    [Tooltip("prefab used for the cat canvas.")]
    private GameObject LoadCat;

    [SerializeField]
    [Tooltip("prefab used for the page canvas.")] 
    private GameObject LoadPage;  

    [SerializeField]
    private StringSO SO;

    public void loadnewCat()
    {
        SceneManager.LoadScene("FDG");
    }

    public void back()
    {
        Destroy(LoadCat);
    }

}
