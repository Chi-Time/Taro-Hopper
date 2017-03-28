using UnityEngine;
using System.Collections.Generic;

public class PadPool : MonoBehaviour 
{
	[SerializeField] private int _PoolSize = 0;
	[SerializeField] private BouncePad _PadPrefab = null;

	private List<BouncePad> _ActivePads = new List<BouncePad> ();
	private List<BouncePad> _InactivePads = new List <BouncePad> ();

	private void Initialise ()
	{
		GeneratePool();
	}

	private void GeneratePool ()
	{
		var parent = new GameObject("Pool");

		for(int i = 0; i < _PoolSize; i++)
			_InactivePads.Add (CreatePad (parent.transform), i);
	}

	private BouncePad CreatePad (Transform parent, int index)
	{
		var go = (GameObject)Instantiate(_PadPrefab.gameObject, Vector3.zero, Quaternion.identity);
		go.name = index + " Bounce Pad";
		go.transform.SetParent(parent);
		go.SetActive(false);

		// Add pool reference to pad.
		var pad = go.GetComponent<BouncePad> ();

		return pad;
	}

	public BouncePad RetrieveFromPool ()
	{
		return null;
	}

	public void ReturnToPool (BouncePad pad)
	{

	}
}
