using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace WebData
{
    public class SavedListButton : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI buttonText;

        [SerializeField]
        private StringSO SO;

        public Animator Fade;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }

        public void OnButtonPress()
        {
            StartCoroutine(ToSavedArticle());
        }

        IEnumerator ToSavedArticle()
        {
            SO.PageName3 = SO.PageName2;
            SO.PageName2 = SO.PageName;
            SO.PageName = buttonText.text;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);

            SceneManager.LoadScene("WikiPage");
        }
    }
}