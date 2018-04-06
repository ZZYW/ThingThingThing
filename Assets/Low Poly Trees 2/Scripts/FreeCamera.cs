// Marmoset Skyshop
// Copyright 2013 Marmoset LLC
// http://marmoset.co

using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour {	
	
	public float thetaSpeed = 250.0f;
	public float phiSpeed = 120.0f;
	public float moveSpeed = 10.0f;
	public float zoomSpeed = 30.0f;
	
	public float phiBoundMin = -89f;
	public float phiBoundMax = 89f;
	public bool useMoveBounds = true;
	public float moveBounds = 100f;
	
	public float rotateSmoothing = 0.5f;
	public float moveSmoothing = 0.7f;
		
	public float distance = 2.0f;
	private Vector2 euler;
	
	private Quaternion targetRot;
	private Vector3 targetLookAt;
	private float targetDist;
	private Vector3 distanceVec = new Vector3(0,0,0);
	
	private Transform target;
	private Rect inputBounds;
	public Rect paramInputBounds = new Rect(0,0,1,1);
	
	public bool usePivotPoint = true;
	public Vector3 pivotPoint = new Vector3(0,2,0);

	public Transform pivotTransform = null;
	#if UNITY_IPHONE || UNITY_ANDROID
	private bool firstTouch = true;
	#endif	
	
	public void Start () {
	    Vector3 angles = transform.eulerAngles;
	    euler.x = angles.y;
	    euler.y = angles.x;
		//unity only returns positive euler angles but we need them in -90 to 90
		euler.y = Mathf.Repeat(euler.y+180f, 360f)-180f;
		
		GameObject go = new GameObject("_FreeCameraTarget");
		go.hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;
		target = go.transform;
		
		if( usePivotPoint ) {			
			target.position = pivotPoint;
			targetDist = (transform.position-target.position).magnitude;
		} else if(pivotTransform != null) {
			usePivotPoint = true;
			Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(pivotTransform.position);
			localPos.x = 0;
			localPos.y = 0;
			targetDist = localPos.z;
			target.position = transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
		} else {
			target.position = transform.position + transform.forward * distance;
			targetDist = distance;
		}
		targetRot = transform.rotation;
		targetLookAt = target.position;
		#if UNITY_IPHONE || UNITY_ANDROID
		firstTouch = true;
		#endif
	}
	
	public void Update () {
		//NOTE: mouse coordinates have a bottom-left origin, camera top-left
		inputBounds.x = GetComponent<Camera>().pixelWidth * paramInputBounds.x;
		inputBounds.y = GetComponent<Camera>().pixelHeight * paramInputBounds.y;
		inputBounds.width = GetComponent<Camera>().pixelWidth * paramInputBounds.width;
		inputBounds.height = GetComponent<Camera>().pixelHeight * paramInputBounds.height;
	
	    if(target && inputBounds.Contains(Input.mousePosition)) {
	    	float dx = Input.GetAxis("Mouse X");
			float dy = Input.GetAxis("Mouse Y");
			#if UNITY_IPHONE || UNITY_ANDROID
			if(Input.multiTouchEnabled) {
				if(Input.touchCount > 0) {
					//touch-down detection. kekeke.
					if(!firstTouch) {
						dx += Input.GetTouch(0).deltaPosition.x * 0.01f;
						dy += Input.GetTouch(0).deltaPosition.y * 0.01f;
					}
					firstTouch = false;
				} else {
					firstTouch = true;
				}
			}
			#endif
			bool click1 = Input.GetMouseButton(0) || Input.touchCount == 1;
    		bool click2 = Input.GetMouseButton(1) || Input.touchCount == 2;
    		bool click3 = Input.GetMouseButton(2) || Input.touchCount == 3;
    		bool click4 = Input.touchCount >= 4;
			bool rotInput = click1;
			bool skyInput = click4 || click1 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift));
			bool panInput = click3 || click1 && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
			bool zoomInput = click2;
			
			if( skyInput ) {
				
			}
			else if( panInput ) {
				dx = dx * moveSpeed * 0.005f * targetDist;
	        	dy = dy * moveSpeed * 0.005f * targetDist;
	 			targetLookAt -= transform.up*dy + transform.right*dx;
				if( useMoveBounds ) {
			 		targetLookAt.x = Mathf.Clamp(targetLookAt.x,-moveBounds,moveBounds);
					targetLookAt.y = Mathf.Clamp(targetLookAt.y,-moveBounds,moveBounds);
			 		targetLookAt.z = Mathf.Clamp(targetLookAt.z,-moveBounds,moveBounds);
				}
			}
			else if( zoomInput ) {
				dy = dy * zoomSpeed * 0.005f * targetDist;
		 		targetDist += dy;
		 		targetDist = Mathf.Max(0.1f,targetDist);
	 		}
			else if( rotInput ) {
				dx = dx * thetaSpeed * 0.02f;
				dy = dy * phiSpeed * 0.02f;
				euler.x += dx;
		        euler.y -= dy;				
		        euler.y = ClampAngle(euler.y, phiBoundMin, phiBoundMax);
		        targetRot = Quaternion.Euler(euler.y, euler.x, 0);
			}
	 		
			targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 0.5f;
	 		targetDist = Mathf.Max(0.1f,targetDist);
		}
	}
	
	public void FixedUpdate() {
		distance = moveSmoothing*targetDist + (1-moveSmoothing)*distance;
		
		transform.rotation = Quaternion.Slerp( transform.rotation, targetRot, rotateSmoothing );
	    target.position = Vector3.Lerp(target.position, targetLookAt, moveSmoothing);
		distanceVec.z = distance;
	    transform.position = target.position - transform.rotation * distanceVec;
	}
	
	static float ClampAngle(float angle, float min, float max) {
		if(angle < -360f) angle += 360f;
		if(angle > 360f)	angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}
}
