using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PadPool 
{
	[SerializeField] private int _PoolSize = 0;
	[SerializeField] private GameObject _PadPrefab = null;
	[SerializeField] private List<BouncePad> _ActivePads = new List<BouncePad> ();
 	[SerializeField] private List<BouncePad> _InactivePads = new List <BouncePad> ();

	/// Faux constructor for class.
	public void Initialise ()
	{
		GeneratePool ();
	}

	private void GeneratePool ()
	{
		for(int i = 0; i < _PoolSize; i++)
			_InactivePads.Add (CreatePad (i));
	}

	private BouncePad CreatePad (int index)
	{
		var go = (GameObject)Object.Instantiate (_PadPrefab.gameObject, Vector3.zero, Quaternion.identity);
		go.name = index + " Bounce Pad";
		go.transform.SetParent (GetParent ());
		go.SetActive (false);

		// Add pool reference to pad.
		var pad = go.GetComponentInChildren<BouncePad> ();
		pad.Initialise (this);

		return pad;
	}

    private Transform GetParent ()
    {
        if(GameObject.Find("Pool"))
            return GameObject.Find ("Pool").transform;

        var parent = new GameObject ("Pool");
        parent.transform.SetParent (GameObject.FindGameObjectWithTag ("Generator").transform);

        return parent.transform;
    }

	/// Get's and returns a fully defaulted bounce pad from the pool.
	public BouncePad RetrieveFromPool ()
	{
		if(_InactivePads.Count > 0)
		{
			var pad = _InactivePads[0];
			_InactivePads.Remove (pad);
			_ActivePads.Add (pad);

            pad.gameObject.SetActive (true);
            pad.transform.position = Vector3.zero;

            return pad;
		}

		return null;
	}

	/// Returns a bounce pad back to the pool and resets it.
	public void ReturnToPool (BouncePad pad)
	{
		_ActivePads.Remove (pad);
		_InactivePads.Add (pad);

        pad.gameObject.SetActive (false);
        pad.transform.position = Vector3.zero;
    }

    // Recursively loop and reset every active object in game.
    public void ResetPool ()
    {
        for(int i = 0; i < _ActivePads.Count; i++)
        {
            var pad = _ActivePads[i];
            pad.Cull ();

            // Recursively loop through the list until every active object is removed.
            CheckPool ();
        }
    }

    private void CheckPool ()
    {
        if (_ActivePads.Count > 0)
            ResetPool ();
    }
}
