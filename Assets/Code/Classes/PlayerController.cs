using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _Speed = 250.0f;
	[SerializeField] private float _JumpHeight = 8.0f;
	[SerializeField] private LayerMask _Ground;

	private float _LastPadPos = 0.0f;
	private Transform _Transform = null;
	private Rigidbody2D _Rigidbody2D = null;

	private void Awake ()
	{
		Initialise ();
	}

	private void Initialise ()
	{
		_Transform = GetComponent<Transform> ();
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	private void Start ()
	{
		SetDefaults ();
	}

	private void SetDefaults ()
	{
		_Rigidbody2D.gravityScale = 1.0f;
		_Rigidbody2D.isKinematic = false;
		_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	private void Update ()
	{
		GetInput ();
		CheckPosition ();
	}

	private void GetInput ()
	{
		float x = Input.GetAxis ("Horizontal");

		Move (new Vector2 (x, 0f));

		if(Input.GetButtonDown ("Jump"))
			if(IsGrounded ())
				Bounce ();
	}

	private void Move (Vector2 dir)
	{
		_Rigidbody2D.velocity = new Vector2 (dir.x * _Speed * Time.deltaTime, _Rigidbody2D.velocity.y);

		if(dir.x > 0)
			_Transform.localScale = new Vector3 (1, 1, 1);
		else if (dir.x < 0)
			_Transform.localScale = new Vector3 (-1, 1, 1);
	}

	private void CheckPosition ()
	{
		if(_Transform.position.y < _LastPadPos - 20f)
			Time.timeScale = 0.0f;
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
		if(other.gameObject.CompareTag ("Respawn"))
		{
			_Rigidbody2D.velocity = new Vector2 (_Rigidbody2D.velocity.x, 0f);
			_LastPadPos = other.transform.position.y;
			Bounce ();
		}
	}

	private void Bounce ()
	{
		_Rigidbody2D.AddForce (Vector2.up * _JumpHeight, ForceMode2D.Impulse);
	}
}
