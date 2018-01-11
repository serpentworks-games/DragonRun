using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		target = FindObjectOfType<Player> ().transform;
		offset = transform.position - target.position;
	}

	void Update(){
		
		if (target != null) {
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
			if (target.GetComponent<Player> ().isDead) {
				target = null;
			}
		} else if(target == null){
			//Do absolutely nothing just chill man
			return;
		}
	}
}
