using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	Rigidbody rb;
	Rigidbody2D rb2D;
	Vector3 velocity;
	Vector2 velocity2D;
	public float moveSpeed = 10f;
	
	MeshGenerator meshGenerator;
	bool is2D;
	
	void Start () {
		meshGenerator = Object.FindFirstObjectByType<MeshGenerator>();
		
		if (meshGenerator != null) {
			is2D = meshGenerator.is2D;
		}
		
		if (is2D) {
			// Add 2D components
			rb2D = GetComponent<Rigidbody2D>();
			if (rb2D == null) {
				rb2D = gameObject.AddComponent<Rigidbody2D>();
			}
			rb2D.gravityScale = 0;
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
			
			BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
			if (boxCollider2D == null) {
				gameObject.AddComponent<BoxCollider2D>();
			}
		} else {
			// Add 3D components
			rb = GetComponent<Rigidbody>();
			if (rb == null) {
				rb = gameObject.AddComponent<Rigidbody>();
			}
			rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			
			BoxCollider boxCollider3D = GetComponent<BoxCollider>();
			if (boxCollider3D == null) {
				gameObject.AddComponent<BoxCollider>();
			}
		}
	}

	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		
		if (is2D) {
			// For 2D mode
			velocity2D = new Vector2(horizontal, vertical).normalized * moveSpeed;
		} else {
			// For 3D mode
			velocity = new Vector3(horizontal, 0, vertical).normalized * moveSpeed;
		}
	}

	void FixedUpdate() {
		if (is2D) {
			if (rb2D != null) {
				// Move in 2D space
				Vector2 newPosition = rb2D.position + velocity2D * Time.fixedDeltaTime;
				rb2D.MovePosition(newPosition);
			}
		} else {
			if (rb != null) {
				// Move in 3D space 
				rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
			}
		}
	}
}
