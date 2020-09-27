//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class ThirdPersonCamera : MonoBehaviour
	{
        public GameObject PlayerPos;
        public float CameraDistance;
        public float CameraHeight;

		public float smooth = 3f;		// カメラモーションのスムーズ化用変数
        public float camera_x_MaxLimit = 0.5f;
        public float camera_x_MinLimit = -0.5f;

        Transform standardPos;			// the usual position for the camera, specified by a transform in the game
		Transform frontPos;			// Front Camera locater
		Transform jumpPos;			// Jump Camera locater
	
		// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
		bool bQuickSwitch = false;  //Change Camera Position Quickly

        [SerializeField]
        private Quaternion vRotation;      // カメラの垂直回転(見下ろし回転)
        [SerializeField]
        public Quaternion hRotation;      // カメラの水平回転

        void Start ()
		{
			// 各参照の初期化
			standardPos = GameObject.Find ("CamPos").transform;
		
			if (GameObject.Find ("FrontPos"))
				frontPos = GameObject.Find ("FrontPos").transform;

			if (GameObject.Find ("JumpPos"))
				jumpPos = GameObject.Find ("JumpPos").transform;

			//カメラをスタートする
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;

            vRotation = Quaternion.identity;                // 垂直回転(X軸を軸とする回転)は、度見下ろす回転
            hRotation = Quaternion.identity;                // 水平回転(Y軸を軸とする回転)は、無回転

        }

        void FixedUpdate()  // このカメラ切り替えはFixedUpdate()内でないと正常に動かない
        {
            Debug.Log(transform.localEulerAngles.x);
            if (Input.GetKey(KeyCode.UpArrow)&& !(vRotation.x <= camera_x_MinLimit))
            {
                vRotation *= Quaternion.Euler(1 * smooth * -1, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow)&&!(vRotation.x >= camera_x_MaxLimit))
            {
                vRotation *= Quaternion.Euler(-1 * smooth * -1, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                hRotation *= Quaternion.Euler(0, -1 *smooth, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                hRotation *= Quaternion.Euler(0, 1 *smooth, 0);
            }

            hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * smooth, 0);

            transform.rotation = hRotation * vRotation;

            //transform.position = standardPos.position;
            transform.position = PlayerPos.transform.position - transform.rotation * Vector3.forward * CameraDistance;
            transform.position = new Vector3(transform.position.x, transform.position.y + CameraHeight, transform.position.z);
            //transform.forward = standardPos.forward;
            //transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            //transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
            //if (Input.GetButton("Fire1"))
            //{   // left Ctlr	
            //    // Change Front Camera
            //    setCameraPositionFrontView();
            //}
            ////else if (Input.GetButton("Fire2"))
            ////{   //Alt	
            ////    // Change Jump Camera
            ////    setCameraPositionJumpView();
            ////}
            //else
            //{
            //    // return the camera to standard position and direction
            //    setCameraPositionNormalView();
            //}
        }

        void setCameraPositionNormalView ()
		{
			if (bQuickSwitch == false) {
				// the camera to standard position and direction
				transform.position = Vector3.Lerp (transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
				transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
			} else {
				// the camera to standard position and direction / Quick Change
				transform.position = standardPos.position;	
				transform.forward = standardPos.forward;
				bQuickSwitch = false;
			}
		}
	
		void setCameraPositionFrontView ()
		{
			// Change Front Camera
			bQuickSwitch = true;
			transform.position = frontPos.position;	
			transform.forward = frontPos.forward;
		}

		void setCameraPositionJumpView ()
		{
			// Change Jump Camera
			bQuickSwitch = false;
			transform.position = Vector3.Lerp (transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp (transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);		
		}
	}
}