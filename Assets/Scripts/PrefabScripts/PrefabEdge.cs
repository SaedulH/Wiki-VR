using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefabScripts{

public class PrefabEdge : MonoBehaviour
{
        public LineRenderer LR;
        public string EdgeType;
        public GameObject EdgeTypeObject;
        public PrefabNode Node1;
        public PrefabEdge Node2;
    }

}
