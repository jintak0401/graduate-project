using UnityEngine;

namespace InGame.Player
{
    public class PlayerWeaving : MonoBehaviour
    {
        private const float RotDegree = 15;
        private const float RotSpeed = 60;
        private const float WeavingDistance = 3;
        private const float WeavingSpeed = 10;
        [SerializeField] private bool isCounterpart;
        private Vector3 _initLocalPosition;
        private Vector3 _initPosition;
        private Quaternion _initQuaternion;
        private Vector3 _leftWeavingLocalPosition;
        private Vector3 _leftWeavingPosition;
        private Vector3 _rightWeavingLocalPosition;
        private Vector3 _rightWeavingPosition;
        private WeavingState _state;


        private void Awake()
        {
            _initPosition = transform.position;
            _initLocalPosition = transform.localPosition;
            _leftWeavingPosition = new Vector3(_initPosition.x + WeavingDistance, _initPosition.y, _initPosition.z);
            // _leftWeavingLocalPosition = transform.localPosition + new Vector3(-WeavingDistance, 0, 0);
            _leftWeavingLocalPosition = new Vector3(transform.localPosition.x - WeavingDistance,
                transform.localPosition.y, transform.localPosition.z);
            _rightWeavingPosition = new Vector3(_initPosition.x - WeavingDistance, _initPosition.y, _initPosition.z);
            // _rightWeavingLocalPosition = transform.localPosition + new Vector3(WeavingDistance, 0, 0);
            _rightWeavingLocalPosition = new Vector3(transform.localPosition.x + WeavingDistance,
                transform.localPosition.y, transform.localPosition.z);
            _initQuaternion = transform.rotation;
            _state = WeavingState.Stop;
        }

        private void Update()
        {
            switch (_state)
            {
                case WeavingState.Left:
                case WeavingState.Right:
                {
                    var toLocalPosition = _state == WeavingState.Left
                        ? _leftWeavingLocalPosition
                        : _rightWeavingLocalPosition;
                    // var toRotation = Quaternion.Euler(0, 0, _state == WeavingState.Left ? RotDegree : -RotDegree);
                    var toRotation = (_state == WeavingState.Left ? transform.forward : -transform.forward) * RotSpeed *
                                     Time.deltaTime;
                    // if (isCounterpart) toRotation *= -1;
                    // transform.rotation =
                    //     Quaternion.RotateTowards(transform.rotation, toRotation,
                    //         RotSpeed * Time.deltaTime);
                    transform.Rotate(toRotation);
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, toLocalPosition,
                        WeavingSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.localPosition, toLocalPosition) < 0.1f)
                        _state = _state == WeavingState.Left ? WeavingState.LeftBack : WeavingState.RightBack;

                    break;
                }
                case WeavingState.LeftBack:
                case WeavingState.RightBack:
                {
                    var toRotation = (_state == WeavingState.RightBack ? transform.forward : -transform.forward) *
                                     RotSpeed *
                                     Time.deltaTime;
                    // transform.rotation =
                    //     Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0),
                    // RotSpeed * Time.deltaTime);
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, _initLocalPosition,
                        WeavingSpeed * Time.deltaTime);
                    transform.Rotate(toRotation);
                    if (Vector3.Distance(transform.localPosition, _initLocalPosition) < 0.1f)
                    {
                        transform.rotation = _initQuaternion;
                        transform.localPosition = _initLocalPosition;
                        _state = WeavingState.Stop;
                    }

                    break;
                }
                case WeavingState.Stop:
                default:
                    break;
            }
        }

        // 1: 왼쪽, 2: 오른쪽
        public void Weaving(int direction)
        {
            Debug.Assert(direction == 1 || direction == 2);

            _state = (WeavingState) direction;
        }

        private enum WeavingState
        {
            Stop,
            Left,
            Right,
            LeftBack,
            RightBack
        }
    }
}