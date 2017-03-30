using UnityEngine;
using System.Collections;

public class PadGenerator : MonoBehaviour
{
    [SerializeField] private float _MinDelay = 0.2f, _MaxDelay = 0.5f;
    [SerializeField] private float _MinRange = -4f, _MaxRange = 4f;
    [SerializeField] private float _MinHeight = 1f, _MaxHeight = 2f;
    [SerializeField] private float _MinWidth = -6f, _MaxWidth = 6f;
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
            float rndW = Random.Range (_MinWidth, _MaxWidth);

            if(_LastPad != null)
               pad.transform.position = new Vector3 (_LastPad.position.x + rndW, _LastPad.position.y + rndH, 0f);
            else
                pad.transform.position = new Vector3 (pos, 10f, 0f);

            _LastPad = pad.transform;
        }

        float newDelay = Random.Range (_MinDelay, _MaxDelay);

        StopCoroutine (SpawnPad (newDelay));
        StartCoroutine (SpawnPad (newDelay));
    }
}
