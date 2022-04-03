using InGame.Player;
using UnityEngine;

namespace Controller
{
    public class CounterpartController : MonoBehaviour
    {
        private PlayerHand _leftHand;
        private PlayerHand _rightHand;
        private PlayerWeaving _weaving;

        private void Awake()
        {
            _rightHand = transform.Find("RightHand").gameObject.GetComponent<PlayerHand>();
            _leftHand = transform.Find("LeftHand").gameObject.GetComponent<PlayerHand>();
            _weaving = gameObject.GetComponent<PlayerWeaving>();
        }

        public void Motion(int motion)
        {
            switch (motion)
            {
                case 0:
                    _weaving.Weaving(1);
                    break;
                case 1:
                    _leftHand.Straight();
                    break;
                case 2:
                    _weaving.Weaving(2);
                    break;
                case 3:
                    _rightHand.Straight();
                    break;
                case 4:
                case 5:
                    _rightHand.Straight();
                    break;
            }
        }
    }
}