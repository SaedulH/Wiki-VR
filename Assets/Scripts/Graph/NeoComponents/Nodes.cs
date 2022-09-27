using System;
using UnityEngine;

namespace Graph.DataStructure{

[Serializable]
public class Nodes
{

    #region Constructors

    // Default Constructor
    public Nodes(long id, string label, string title)
    {
        _ID = id;
        _Label = label;
        _Title = title;
    }

    // Clone Constructor
    public Nodes(Nodes node)
        : this(node.Id, node.Label, node.Title)
    {
    }    

    #endregion

    #region Properties

    [SerializeField]
    private long _ID;

    // Unique identifier used to link nodes together.
    public long Id { get { return _ID; } }

    [SerializeField]
    private string _Label;

    // The displayed label of the node.
    public string Label { get { return _Label; } }    

    [SerializeField]
    private string _Title;

    // The displayed title of the node.
    public string Title { get { return _Title; } }

    #endregion

    public override string ToString()
    {
        return Title;
    }

}

}