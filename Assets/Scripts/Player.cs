using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity {

	public float moveSpeed = 5;

    public VirtualJoystick joystick;

	public Crosshairs crosshairs;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;
	
	protected override void Start () {
		base.Start ();
	}

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    void OnNewWave(int waveNumber)
    {
        health = startingHealth;
        gunController.EquipGun (waveNumber - 1);
    }

	void Update () {
		// Movement input
		Vector3 moveInput = new Vector3 (joystick.Horizontal() / 10, 0, joystick.Vertical() / 10);
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move (moveVelocity);

		// Look input
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.up * gunController.GunHeight);
		float rayDistance;

		if (groundPlane.Raycast(ray,out rayDistance)) {
			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.DrawLine(ray.origin,point,Color.red);
			controller.LookAt(point);  //Done by me --------------------------------------------
			crosshairs.transform.position = point;
			crosshairs.DetectTargets(ray);
		}

		// Weapon input
		if (Input.GetMouseButton(0)) {
			gunController.OnTriggerHold();
		}
		if (Input.GetMouseButtonUp(0)) {
			gunController.OnTriggerRelease();
		}
        if (transform.position.y < -10)
        {
            TakeDamage(health);
        }
	}
    public override void Die()
    {
        AudioManager.instance.PlaySound("Player Death", transform.position);
        base.Die();
    }

}
