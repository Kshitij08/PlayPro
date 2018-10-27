using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

    public MyLook abc;

	Vector3 velocity;
	Rigidbody myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(abc.Horizontal() * 200 , transform.position.y, abc.Vertical() * 200);
        transform.LookAt(heightCorrectedPoint);
    }

    void FixedUpdate() {
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);

	}
}
