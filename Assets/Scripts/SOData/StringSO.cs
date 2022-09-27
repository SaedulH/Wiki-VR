using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringSO : ScriptableObject
{
    /// Holds data for values that are to be retained when switching scenes or exiting application (indefinitely)

    [SerializeField]
    private string _cat;

    public string Cat
    {
        get { return _cat; }
        set { _cat = value; }
    }
    
    [SerializeField]
    private string _lastcat;

    public string LastCat
    {
        get { return _lastcat; }
        set { _lastcat = value; }
    }

    [SerializeField]
    private string _2ndlastCat;

    public string SecondLastCat
    {
        get { return _2ndlastCat; }
        set { _2ndlastCat = value; }
    }

    [SerializeField]
    private string _Pagename;

    public string PageName
    {
        get { return _Pagename; }
        set { _Pagename = value; }
    }

    [SerializeField]
    private string _Pagename2;

    public string PageName2
    {
        get { return _Pagename2; }
        set { _Pagename2 = value; }
    }

    [SerializeField]
    private string _Pagename3;

    public string PageName3
    {
        get { return _Pagename3; }
        set { _Pagename3 = value; }
    }

    [SerializeField]
    private int _Num;
    public int initialNum
    {
        get { return _Num; }
        set { _Num = value; }
    }

    [SerializeField]
    private float _Limiter;
    public float Limiter
    {
        get { return _Limiter; }
        set { _Limiter = value; }
    }

    [SerializeField]
    private List<string> _Saved;
    public List<string> Saved
    {
        get { return _Saved; }
        set { _Saved = value; }
    }

    [SerializeField]
    public void AddtoSaved(string page)
    {
        Saved.Add(page);
    }

    [SerializeField]
    public void RemovefromSaved(string page)
    {
        Saved.Remove(page);
    }

    [SerializeField]
    private bool _Snapturn;
    public bool Snapturn
    {
        get { return _Snapturn; }
        set { _Snapturn = value; }
    }

    [SerializeField]
    private float _Volume;
    public float Volume
    {
        get { return _Volume; }
        set { _Volume = value; }
    }  

}
