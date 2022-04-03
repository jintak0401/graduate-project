using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class EffectManager : MonoBehaviour
    {
        public enum Judgement
        {
            Perfect,
            Cool,
            Good,
            Bad,
            Miss
        }

        private const string HitTrigger = "Hit";
        private static readonly int Hit = Animator.StringToHash(HitTrigger);
        [SerializeField] private Animator noteHitAnimator;

        [SerializeField] private Animator judgementAnimator;
        [SerializeField] private Image judgementImage;
        [SerializeField] private Sprite[] judgementSprite;

        public void JudgementEffect(int judgement)
        {
            judgementImage.sprite = judgementSprite[judgement];
            judgementAnimator.SetTrigger(Hit);
        }

        public void NoteHitEffect()
        {
            noteHitAnimator.SetTrigger(Hit);
        }
    }
}