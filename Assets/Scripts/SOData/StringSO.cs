using UnityEngine;

[CreateAssetMenu]
public class StringSO : ScriptableObject
{
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
    private int _Num;
    public int Num
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
}
