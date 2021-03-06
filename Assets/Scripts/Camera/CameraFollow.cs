﻿using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
		public PhaseManager phaseManager;

        Vector3 offset;                     // The initial offset from the target.
        new Camera camera;

        void Start () {
            // Calculate the initial offset.
            offset = transform.position - target.position;

			phaseManager = GameObject.Find ("HUDCanvas").GetComponent<PhaseManager> ();
			camera = GetComponent <Camera>();
        }

		void FixedUpdate ()
		{
			// since there is no target during buildphase (player is disabled) we need this extra camera code to move around while building.

			if (phaseManager.IsBuildPhase) {
				RaycastHit hitter;
				Ray ray = camera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
				// this is saying that if the raycast doesnt hit something ?below? layer 9?
				// layer mask ignores colliders ie: ignore scene objects to check for floor mask
				// 8 is floor layer
				if (Physics.Raycast (ray, out hitter, Mathf.Infinity, 1 << 9)) {
					Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal") * 10.5f * Time.deltaTime, Input.GetAxis ("Vertical") * 10.5f * Time.deltaTime, 0.0f);
					transform.Translate (direction);

					ray = camera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
					if (!Physics.Raycast (ray, out hitter, Mathf.Infinity, 1 << 9)) { 
						direction = -direction;
						transform.Translate (direction);
					}
				}
				return;
			}

            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}