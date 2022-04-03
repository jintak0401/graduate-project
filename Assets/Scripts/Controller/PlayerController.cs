using InGame.Player;
using Manager;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public enum MotionState
        {
            LeftWeaving,
            LeftStraight,
            RightWeaving,
            RightStraight,
            Guard
        }

        private PlayerHand _leftHand;
        private PlayerHand _rightHand;
        private TimingManager _timingManager;
        private PlayerWeaving _weaving;

        private void Awake()
        {
            _timingManager = FindObjectOfType<TimingManager>();
            _rightHand = transform.Find("RightHand").gameObject.GetComponent<PlayerHand>();
            _leftHand = transform.Find("LeftHand").gameObject.GetComponent<PlayerHand>();
            _weaving = gameObject.GetComponent<PlayerWeaving>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                _timingManager.CheckTiming(KeyCode.A);
            // _leftHand.Straight();
            else if (Input.GetKeyDown(KeyCode.D))
                _timingManager.CheckTiming(KeyCode.D);
            // _rightHand.Straight();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                _timingManager.CheckTiming(KeyCode.LeftArrow);
            // _weaving.Weaving(1);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                _timingManager.CheckTiming(KeyCode.RightArrow);
            // _weaving.Weaving(2);
            else if (Input.GetKeyDown(KeyCode.W))
                _timingManager.CheckTiming(KeyCode.W);
            // _rightHand.Guard();
            // _leftHand.Guard();
        }

        public void Motion(int motion)
        {
            switch (motion)
            {
                case 0:
                    _leftHand.Straight();
                    break;
                case 1:
                    _weaving.Weaving(1);
                    break;
                case 2:
                    _rightHand.Straight();
                    break;
                case 3:
                    _weaving.Weaving(2);
                    break;
                case 4:
                    _leftHand.Guard();
                    break;
                case 5:
                    _rightHand.Guard();
                    break;
            }
        }
    }
}