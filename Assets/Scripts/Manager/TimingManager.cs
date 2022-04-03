using System.Collections.Generic;
using System.Linq;
using Controller;
using UnityEngine;

namespace Manager
{
    public class TimingManager : MonoBehaviour
    {
        public List<GameObject> noteList = new List<GameObject>();

        [SerializeField] private Transform center;
        [SerializeField] private RectTransform[] timingRect;

        private EffectManager _effectManager;
        private PlayerController _playerController;
        private ScoreManager _scoreManager;
        private Vector2[] _timingBoxes;

        private void Awake()
        {
            _effectManager = FindObjectOfType<EffectManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            _timingBoxes = new Vector2[timingRect.Length];
            _playerController = FindObjectOfType<PlayerController>();

            for (var i = 0; i < timingRect.Length; i++)
            {
                var halfWidth = timingRect[i].rect.width / 2;
                _timingBoxes[i].Set(center.localPosition.x - halfWidth, center.localPosition.x + halfWidth);
            }
        }

        public void CheckTiming(KeyCode keyCode)
        {
            foreach (var (notePosX, idx) in noteList.Select((obj, idx) => (obj.transform.localPosition.x, idx)))
            {
                foreach (var (left, right, jdx) in _timingBoxes.Select((vec, jdx) => (vec.x, vec.y, jdx)))
                {
                    if (!(left <= notePosX) || !(notePosX <= right)) continue;

                    // 노트 제거
                    Note note;
                    (note = noteList[idx].GetComponent<Note>()).HideNote();

                    if (Note.KeyMapping[note.NoteType] != keyCode) continue;

                    _playerController.Motion(note.NoteType);

                    // 이펙트 연출
                    if (jdx < _timingBoxes.Length - 1)
                        _effectManager.NoteHitEffect();
                    noteList.RemoveAt(idx);
                    _effectManager.JudgementEffect(jdx);

                    // 점수 증가
                    _scoreManager.IncreaseScore(jdx);

                    if (note.NoteType != 4)
                        return;
                }

                _scoreManager.ResetCombo();
                _effectManager.JudgementEffect(4);
            }
        }
    }
}