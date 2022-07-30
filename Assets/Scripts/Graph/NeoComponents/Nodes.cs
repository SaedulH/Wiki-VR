using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graph.DataStructure{

[Serializable]
public class Nodes
{

    #region Constructors

    /// Default Constructor
    public Nodes(long id, string label, string title)
    {
        _ID = id;
        _Label = label;
        _Title = title;
    }

    /// Clone Constructor
    public Nodes(Nodes node)
        : this(node.Id, node.Label, node.Title)
    {
    }    

    //public long ID;
    //public string Label;
    //public string Title;

    #endregion

    #region Properties

    [SerializeField]
    [Tooltip("Unique identifier used to link nodes together")]
    private long _ID;

    /// <summary>
    /// Unique identifier used to link nodes together.
    /// </summary>
    public long Id { get { return _ID; } }

    [SerializeField]
    [Tooltip("The displayed label of the node.")]
    private string _Label;

    /// <summary>
    /// The displayed label of the node.
    /// </summary>
    public string Label { get { return _Label; } }    

    [SerializeField]
    [Tooltip("The displayed title of the node.")]
    private string _Title;

    /// <summary>
    /// The displayed title of the node.
    /// </summary>
    public string Title { get { return _Title; } }

    #endregion

    public override string ToString()
    {
        return Title;
    }

}

}