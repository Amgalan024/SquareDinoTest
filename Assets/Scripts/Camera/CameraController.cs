using System;
using UnityEngine;

namespace Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraModel _cameraModel;
        [SerializeField] private UnityEngine.Camera _camera;

        private void Update()
        {
            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            _camera.transform.position = _cameraModel.Anchor.position;

            Vector3 cameraEulerAngles = _camera.transform.rotation.eulerAngles;

            Vector3 newCameraAngle =
                new Vector3(cameraEulerAngles.x, _cameraModel.Anchor.rotation.eulerAngles.y, cameraEulerAngles.z);

            _camera.transform.rotation = Quaternion.Euler(newCameraAngle);
        }
    }
}