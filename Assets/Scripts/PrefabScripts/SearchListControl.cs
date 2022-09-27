using System.Collections;
using UnityEngine;
using Graph;
using TMPro;

namespace Search
{

    public class SearchListControl : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("References the parent holding all buttons.")]
        private GameObject ButtonsParent;

        [SerializeField]
        private GameObject buttonTemplate;

        [SerializeField]
        private TextMeshProUGUI statusText;
        
        void generateButtons(Graph.SearchResults results)
        {   
            
            if(results.results1.Count == 0)
            {
                Debug.Log("Nothing here");
                statusText.text = "No Results found";
            }
            else
            {
                statusText.text = "";
                foreach (string cat in results.results1)
                {
                    GameObject button = Instantiate(buttonTemplate, ButtonsParent.transform) as GameObject;
                    button.SetActive(true);

                    button.GetComponent<SearchListButton>().setText(cat);

                }
            }
        }

        private void clearButtons()
        {

            foreach (Transform button in ButtonsParent.transform)
                if(button.GetComponentInChildren<TextMeshProUGUI>().text != "placeholder")
                {
                    GameObject.Destroy(button.gameObject);
                }
                            
        }

        //House Category query results in scoll rect
        public IEnumerator SearchCat(string inputfieldtext)
        {   
            clearButtons();
            
            Graph.SearchResults results = new Graph.SearchResults();
            
            NeoQuery.searchCatQuery(inputfieldtext, results);
            statusText.text = "Searching...";
            yield return new WaitForSeconds(0.5f);
            generateButtons(results);
        }

        //House Page query results in scoll rect
        public IEnumerator SearchPage(string inputfieldtext)
        {   
            clearButtons();

            Graph.SearchResults results = new Graph.SearchResults();
            
            NeoQuery.searchPageQuery(inputfieldtext, results);
            statusText.text = "Searching...";
            yield return new WaitForSeconds(0.5f);
            generateButtons(results);
        }
        //House category query results in scoll rect specific for graph
        public IEnumerator GraphSearchCat(string inputfieldtext)
        {   
            clearButtons();
            
            Graph.SearchResults results = new Graph.SearchResults();
            statusText.text = "Searching...";

            foreach(GraphNode node in GraphRenderer.Current.GraphNodes.Values)
            {
                if(node.Node.Title.Contains(inputfieldtext) && node.Node.Label == "Category")
                {
                    results.results1.Add(node.Node.Title);
                }
            }
            
            yield return new WaitForSeconds(0.5f);
            generateButtons(results);

        }

        //House page query results in scoll rect specific for graph
        public IEnumerator GraphSearchPage(string inputfieldtext)
        {   
            clearButtons();

            Graph.SearchResults results = new Graph.SearchResults();
            statusText.text = "Searching...";   

            foreach(GraphNode node in GraphRenderer.Current.GraphNodes.Values)
            {
                if(node.Node.Title.Contains(inputfieldtext) && node.Node.Label == "Page")
                {
                    results.results1.Add(node.Node.Title);
                }
            }

            yield return new WaitForSeconds(0.5f);
            generateButtons(results);
        }
    }

}
