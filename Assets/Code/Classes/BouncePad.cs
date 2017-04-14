using UnityEngine;
using System.Collections;

//TODO: Add in support for box break particle effect.
public class BouncePad : MonoBehaviour
{
	[SerializeField] private float _Speed = 2.0f;
	[SerializeField] private float _FadeTime = 1.0f;
	[SerializeField] private float _CullBoundary = 10.0f;

    private PadPool _Pool = null;
	private Transform _Player = null;
	private Transform _Transform = null;
	private Rigidbody2D _Rigidbody2D = null;
	private GameController _GameController = null;
	private SpriteRenderer _SpriteRenderer = null;

	private void Awake ()
	{
		SetReferences ();
	}

	private void SetReferences ()
	{
		_Transform = GetComponent<Transform> ();
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
		_SpriteRenderer = GetComponent<SpriteRenderer> ();
        _Player = GameObject.FindGameObjectWithTag ("Player").transform;
		_GameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	public void Initialise (PadPool pool)
	{
		_Pool = pool;
	}

	private void Start ()
	{
		SetDefaults ();
	}

	private void SetDefaults ()
	{
		_Rigidbody2D.gravityScale = 0.0f;
		_Rigidbody2D.isKinematic = true;
		_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	private void OnEnable ()
	{
		StartCoroutine (FadeTo (1.0f, _FadeTime));
	}

	private IEnumerator FadeTo (float aValue, float aTime)
	{
		float alpha = _SpriteRenderer.material.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newAlpha = new Color (1, 1, 1, Mathf.Lerp (alpha, aValue, t));
			_SpriteRenderer.material.color = newAlpha;

			yield return null;
		}
	}

	private void OnDisable ()
	{
		ResetOpacity ();
	}

	private void ResetOpacity ()
	{
		_SpriteRenderer.material.color = new Color (
			_SpriteRenderer.material.color.r,
			_SpriteRenderer.material.color.g,
			_SpriteRenderer.material.color.b,
			0.0f
		);
	}

	private void Update ()
	{
		Move ();
		CheckPosition ();
	}

	private void Move ()
	{
		_Rigidbody2D.MovePosition (_Transform.position + -_Transform.up * _Speed * Time.deltaTime);
	}

	private void CheckPosition ()
	{
		if (_Transform.position.y < (_Player.position.y - _CullBoundary))
			Cull ();
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
            Cull ();
            _GameController.Score++;
		}
	}

	public void Cull ()
	{
		_Pool.ReturnToPool (this);
	}
}
