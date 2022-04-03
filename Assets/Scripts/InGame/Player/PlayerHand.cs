using UnityEngine;

namespace InGame.Player
{
    public class PlayerHand : MonoBehaviour
    {
        private const float StraightSpeed = 20;
        private const float GuardSpeed = 5;
        private Vector3 _counterpartInitPosition;
        private Vector3 _guardLocalPosition;
        private Vector3 _initLocalPosition;
        private Vector3 _initPosition;
        private float _speed = GuardSpeed;
        private HandState _state;

        private void Awake()
        {
            var tmp = GameObject.FindGameObjectsWithTag("Player");
            foreach (var obj in tmp)
                if (obj != transform.parent.gameObject)
                {
                    _counterpartInitPosition = obj.transform.position;
                    break;
                }

            _initPosition = transform.position;
            _initLocalPosition = transform.localPosition;
            _guardLocalPosition = new Vector3(_initLocalPosition.x > 0 ? 0.5f : -0.5f, 1, 1);
            _state = HandState.Stop;
        }

        private void Update()
        {
            switch (_state)
            {
                case HandState.Going:
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, _counterpartInitPosition,
                            _speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, _counterpartInitPosition) < 0.1f) _state = HandState.Back;
                    break;
                }
                case HandState.Back:
                case HandState.GuardBack:
                {
                    transform.localPosition =
                        Vector3.MoveTowards(transform.localPosition, _initLocalPosition, _speed * Time.deltaTime);
                    if (Vector3.Distance(transform.localPosition, _initLocalPosition) < 0.1f)
                    {
                        transform.localPosition = _initLocalPosition;
                        _state = HandState.Stop;
                    }

                    break;
                }
                case HandState.Guard:
                {
                    transform.localPosition =
                        Vector3.MoveTowards(transform.localPosition, _guardLocalPosition, _speed * Time.deltaTime);
                    if (Vector3.Distance(transform.localPosition, _guardLocalPosition) < 0.001f)
                    {
                        transform.localPosition = _guardLocalPosition;
                        _state = HandState.GuardBack;
                    }

                    break;
                }

                case HandState.Stop:
                {
                    if (Vector3.Distance(transform.localPosition, _initLocalPosition) > 0.1f)
                        transform.localPosition =
                            Vector3.MoveTowards(transform.localPosition, _initLocalPosition,
                                _speed * Time.deltaTime);
                    break;
                }
            }
        }

        public void Straight()
        {
            _speed = StraightSpeed;
            _state = HandState.Going;
        }

        public void Guard()
        {
            if (_state == HandState.Going || _state == HandState.Back)
                _speed = StraightSpeed;
            else
                _speed = GuardSpeed;

            _state = HandState.Guard;
        }

        private enum HandState
        {
            Stop,
            Going,
            Back,
            Guard,
            GuardBack
        }
    }
}