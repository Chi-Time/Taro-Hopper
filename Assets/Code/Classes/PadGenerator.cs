using UnityEngine;
using System.Collections;

public class PadGenerator : MonoBehaviour
{
    [SerializeField] private float _MinDelay = 0f, _MaxDelay = 0f;
    [SerializeField] private float _MinRange = 0f, _MaxRange = 0f;
    [SerializeField] private PadPool _Pool = new PadPool ();

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
            pad.transform.position = new Vector3 (pos, 15f, 0f);
        }

        float newDelay = Random.Range (_MinDelay, _MaxDelay);

        StopCoroutine (SpawnPad (newDelay));
        StartCoroutine (SpawnPad (newDelay));
    }
}
