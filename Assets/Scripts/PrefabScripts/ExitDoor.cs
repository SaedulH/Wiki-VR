using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitDoor : MonoBehaviour
{

    [SerializeField]
    private Material blueGlass;

    [SerializeField]
    private Material blueGlassHovered;

    [SerializeField]
    private GameObject GlassHex;

    [SerializeField]
    private TextMeshProUGUI catName;

    public StringSO SO;
    public Animator Fade;


    void Start()
    {
        catName.text = SO.Cat;
    }

    public void OnHover()
    {
        GlassHex.GetComponent<MeshRenderer>().material = blueGlassHovered;
    }

    public void OffHover()
    {
         GlassHex.GetComponent<MeshRenderer>().material = blueGlass; 
    }

    public void Exited()
    {
        Debug.Log("exiting");
        StartCoroutine(exitPressed());
    }


    IEnumerator exitPressed()
    {
        Fade.SetTrigger("Start");
            
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FDG");
    }
}
