using UnityEngine;

public class BouncePad : MonoBehaviour 
{
	[SerializeField] private float _CullBoundary = 0.0f;
	
	private Transform _Transform = null;
	private Rigidbody2D _Rigidbody2D = null;

	private void Awake ()
	{
		Initialise();
	}

	private void Initialise ()
	{
		_Transform = GetComponent<Transform> ();
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	private void Start ()
	{
		SetDefaults();
	}

	private void SetDefaults ()
	{
		_Rigidbody2D.gravityScale = 1.0f;
		_Rigidbody2D.isKinematic = false;
		_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	private void Update ()
	{
		CheckPosition();
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
		Destroy(this.gameObject);
	}
}
