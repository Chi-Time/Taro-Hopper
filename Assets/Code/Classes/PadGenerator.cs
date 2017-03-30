using UnityEngine;
using System.Collections;

public class PadGenerator : MonoBehaviour
{
    [SerializeField] private float _MinDelay = 0f, _MaxDelay = 0f;
    [SerializeField] private float _MinRange = 0f, _MaxRange = 0f;
    [SerializeField] private float _MinHeight = 0f, _MaxHeight = 0f;
    [SerializeField] private float _MinWidth = 0f, _MaxWidth = 0f;
    [SerializeField] private PadPool _Pool = new PadPool ();

    private Transform _Player = null;
    private Transform _LastPad = null;

    private void Awake ()
    {
        Initialise ();
    }

    private void Initialise ()
    {
        _Pool.Initialise ();
        _Player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        float delay = Random.Range (_MinDelay, _MaxDelay);
        StartCoroutine (SpawnPad (delay));
    }

    private IEnumerator SpawnPad (float delay)
    {
        yield return new WaitForSeconds (delay);

        var pad = _Pool.RetrieveFromPool ();

        if(pad != null)
        {
            float pos = Random.Range (_MinRange, _MaxRange);
            float rndH = Random.Range (_MinHeight, _MaxHeight);

            if(_LastPad != null)
            else
                pad.transform.position = new Vector3 (pos, 10f, 0f);

            _LastPad = pad.transform;
        }

        float newDelay = Random.Range (_MinDelay, _MaxDelay);

        StopCoroutine (SpawnPad (newDelay));
        StartCoroutine (SpawnPad (newDelay));
    }
}
