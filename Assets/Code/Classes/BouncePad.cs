using UnityEngine;

public class BouncePad : MonoBehaviour 
{
	[SerializeField] private float _Speed = 0.0f;
	[SerializeField] private float _CullBoundary = 0.0f;
	
	private PadPool _Pool = null;
	private Transform _Transform = null;
	private Rigidbody2D _Rigidbody2D = null;

	public void Initialise (PadPool pool)
	{
		_Transform = GetComponent<Transform> ();
		_Rigidbody2D = GetComponent<Rigidbody2D> ();

		_Pool = pool;
	}

	private void Start ()
	{
		SetDefaults();
	}

	private void SetDefaults ()
	{
		_Rigidbody2D.gravityScale = 0.0f;
		_Rigidbody2D.isKinematic = true;
		_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	private void Update ()
	{
		Move();
		CheckPosition();
	}

	private void Move ()
	{
		_Rigidbody2D.MovePosition (_Transform.position + -_Transform.up * _Speed * Time.deltaTime);
	}

	private void CheckPosition ()
	{
		if(_Transform.position.y < _CullBoundary)
			Cull();
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
			Cull();
	}

	private void Cull ()
	{
		_Pool.ReturnToPool (this);
	}
}
