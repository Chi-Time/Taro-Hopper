using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _Speed = 0.0f;
	[SerializeField] private float _JumpHeight = 0.0f;
	[SerializeField] private LayerMask _Ground;

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
		GetInput();
	}

	private void GetInput ()
	{
		float x = Input.GetAxis("Horizontal");

		Move (new Vector2 (x, _Rigidbody2D.velocity.y));
	}

	private void Move (Vector2 dir)
	{
		_Rigidbody2D.velocity = dir * _Speed;
	}

	private bool IsGrounded ()
	{
		var endPos = new Vector3 (_Transform.position.x, _Transform.position.y - 0.5f, 0);

		if(Physics2D.Linecast (_Transform.position, endPos, _Ground))
			return true;

		return false;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Respawn"))
			Bounce();
	}

	private void Bounce ()
	{
		_Rigidbody2D.AddForce(Vector2.up * _JumpHeight, ForceMode2D.Impulse);
	}
}
