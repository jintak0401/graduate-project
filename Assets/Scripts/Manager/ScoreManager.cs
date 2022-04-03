using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        private const string ScoreUpTrigger = "ScoreUp";
        private const string ComboUpTrigger = "ComboUp";
        private static readonly int ScoreUp = Animator.StringToHash(ScoreUpTrigger);
        private static readonly int ComboUp = Animator.StringToHash(ComboUpTrigger);
        [SerializeField] private Animator scoreAnimator;
        [SerializeField] private Animator comboAnimator;
        [SerializeField] private Text scoreText;
        [SerializeField] private Image comboImage;
        [SerializeField] private Text comboText;
        [SerializeField] private int defaultIncreasingScore = 10;
        [SerializeField] private int defaultComboBunusScore = 10;


        [SerializeField] private float[] weight;
        private int _currentCombo;
        private int _currentScore;

        private void Awake()
        {
            _currentScore = 0;
            _currentCombo = 0;
            scoreText.text = "0";
            comboText.gameObject.SetActive(false);
            comboImage.gameObject.SetActive(false);
        }

        public void IncreaseCombo(int num = 1)
        {
            _currentCombo += num;
            comboText.text = $"{_currentCombo:#,##0}";

            if (_currentCombo <= 2) return;

            // 콤보가 3이상일 때부터 보인다
            comboText.gameObject.SetActive(true);
            comboImage.gameObject.SetActive(true);
            comboAnimator.SetTrigger(ComboUp);
        }

        public void ResetCombo()
        {
            _currentCombo = 0;
            comboText.text = "0";
            comboText.gameObject.SetActive(false);
            comboImage.gameObject.SetActive(false);
        }

        public void IncreaseScore(int judgementState)
        {
            // 콤보 증가
            IncreaseCombo();

            // 콤보 보너스 점수 계산
            var bonusComboScore = _currentCombo / 10 * defaultComboBunusScore;

            // 판정 가중치 계산
            var increasingScore = (int) ((defaultIncreasingScore + bonusComboScore) * weight[judgementState]);

            // 점수 반영
            _currentScore += increasingScore;
            scoreText.text = $"{_currentScore:#,##0}";

            // 애니메이션 실행
            scoreAnimator.SetTrigger(ScoreUp);
        }
    }
}