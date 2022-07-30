using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteraction : MonoBehaviour
{

    public void enlargeNode()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

        public void enlargeMenuNode()
    {
        transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
    }

    public void normalsizeNode()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

        public void normalMenuNode()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
