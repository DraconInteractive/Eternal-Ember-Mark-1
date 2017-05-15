using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCamera : MonoBehaviour {
	public Dragon dragon;
	public Vector3 cameraOffset, lookOffset;
	Vector3 cameraVelocity;
	public GameObject d_head;
	// Use this for initialization
	void Start () {
		print ("dragon start");
		d_head = dragon.dragonHead;

		PossessionController.pc_controller.dragonCamera = this.gameObject;
	}

//	void OnEnable () {
//		PossessionController.pc_controller.dragonCamera = this.gameObject;
//	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp (transform.position, dragon.transform.position + dragon.transform.forward * cameraOffset.z + dragon.transform.right * cameraOffset.x + dragon.transform.up * cameraOffset.y, ref cameraVelocity, 0.25f);
//		transform.LookAt (d_head.transform.position);
		Quaternion targetRotation = Quaternion.LookRotation ((d_head.transform.position + (dragon.transform.forward * lookOffset.z) - transform.position), Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 10 * Time.deltaTime);
	}
}
