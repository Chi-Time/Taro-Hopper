using UnityEngine;
using System.Collections.Generic;

public class PadPool : MonoBehaviour 
{
	[SerializeField] private int _PoolSize = 0;
	[SerializeField] private BouncePad _PadPrefab = null;

	[SerializeField] private List<BouncePad> _ActivePads = new List<BouncePad> ();
 	[SerializeField] private List<BouncePad> _InactivePads = new List <BouncePad> ();

	private void Initialise ()
	{
		GeneratePool();
	}

	private void GeneratePool ()
	{
		var parent = new GameObject ("Pool");

		for(int i = 0; i < _PoolSize; i++)
			_InactivePads.Add (CreatePad (parent.transform, i));
	}

	private BouncePad CreatePad (Transform parent, int index)
	{
		var go = (GameObject)Instantiate (_PadPrefab.gameObject, Vector3.zero, Quaternion.identity);
		go.name = index + " Bounce Pad";
		go.transform.SetParent (parent);
		go.SetActive (false);

		// Add pool reference to pad.
		var pad = go.GetComponent<BouncePad> ();

		return pad;
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
}
