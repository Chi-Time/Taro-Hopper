using UnityEngine;
using System.Collections;

public class PadGenerator : MonoBehaviour
{
    [SerializeField] private float _MinDelay = 0.2f, _MaxDelay = 0.5f;
    [SerializeField] private float _MinRange = -4f, _MaxRange = 4f;
    [SerializeField] private float _MinHeight = 1f, _MaxHeight = 3.5f;
    [SerializeField] private float _MinWidth = -7f, _MaxWidth = 7f;
    [SerializeField] private PadPool _Pool = new PadPool ();

    private Transform _LastPad = null;

    private void Awake ()
    {
        Initialise ();
    }

    private void Initialise ()
    {
        _Pool.Initialise ();
    }

    private void Start ()
    {
        GenerateField ();
        StartSpawner ();
    }

    private void GenerateField ()
    {
        for (int i = 0; i < 15; i++)
            SpawnPad ();
    }

    private void StartSpawner ()
    {
        float delay = Random.Range (_MinDelay, _MaxDelay);
        StartCoroutine (Spawn (delay));
    }

    private void SpawnPad ()
    {
        var pad = _Pool.RetrieveFromPool ();

        if (pad != null)
        {
            float pos = Random.Range (_MinRange, _MaxRange);
            float rndH = Random.Range (_MinHeight, _MaxHeight);
            float rndW = Random.Range (_MinWidth, _MaxWidth);

            if (_LastPad != null)
                pad.transform.position = new Vector3 (_LastPad.position.x + rndW, _LastPad.position.y + rndH, 0f);
            else
                pad.transform.position = new Vector3 (pos, 10f, 0f);

            _LastPad = pad.transform;
        }
    }

    private IEnumerator Spawn (float delay)
    {
        yield return new WaitForSeconds (delay);

        SpawnPad ();

        float newDelay = Random.Range (_MinDelay, _MaxDelay);

        StopCoroutine (Spawn (newDelay));
        StartCoroutine (Spawn (newDelay));
    }

    public void Restart ()
    {
        StopCoroutine (Spawn (0f));
        _LastPad = null;

        //TODO: Re-call generate field every time restart happens.
        _Pool.ResetPool ();
    } 
}
