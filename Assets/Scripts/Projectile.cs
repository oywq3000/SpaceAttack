using UnityEngine;
using System.Collections;
using DefaultNamespace.Live;

public class Projectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;
	public float damage;

	public float speed = 10;
	
	// Use this for initialization
	void Start () {
		GameObject obj = (GameObject) Instantiate(shoot_effect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = firing_ship.transform;
		Destroy(gameObject, 5f); //Bullet will despawn after 5 seconds
	}
	
	// Update is called once per frame
	void Update () {
	
		//transform.Translate(Vector3.up*Time.deltaTime*speed);
	}
	
	
	void OnTriggerEnter2D(Collider2D col) {

		//Don't want to collide with the ship that's shooting this thing, nor another projectile.
		if (col.gameObject != firing_ship && col.gameObject.tag != "Projectile") {
			Instantiate(hit_effect, transform.position, Quaternion.identity);
			var canDamageable = col.gameObject.GetComponent<ICanDamageable>();
			if (canDamageable!=null)
			{
				canDamageable.Damage(damage);
			}
			Destroy(gameObject);
		}

		
	}
	
	
	
}
