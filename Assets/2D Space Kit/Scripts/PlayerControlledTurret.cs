using UnityEngine;
using System.Collections;

public class PlayerControlledTurret : MonoBehaviour {

	public GameObject weapon_prefab;
	public GameObject[] barrel_hardpoints;
	public float shot_speed;
	int barrel_index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0) && barrel_hardpoints != null) {
			GameObject bullet = Instantiate(weapon_prefab, barrel_hardpoints[barrel_index].transform.position, transform.rotation);
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
			bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
			barrel_index++; //This will cycle sequentially through the barrels in the barrelHardpoints array
			
			if (barrel_index >= barrel_hardpoints.Length)
				barrel_index = 0;
			
		
		}
	
	}
}
