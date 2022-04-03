#nullable enable
using Controller;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

namespace Manager
{
    public class NoteManager : MonoBehaviour
    {
        private static readonly Random _random = new Random();
        [SerializeField] private Transform tfNoteAppear;
        [SerializeField] private Transform tfRightNoteAppear;
        [SerializeField] private Transform tfLeftNoteAppear;
        public int bpm;
        private CounterpartController _counterpartController;
        private double _currentTime;
        private EffectManager _effectManager;
        private ScoreManager _scoreManager;
        private TimingManager _timingManager;

        private void Awake()
        {
            _timingManager = GetComponent<TimingManager>();
            _effectManager = FindObjectOfType<EffectManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            _counterpartController = FindObjectOfType<CounterpartController>();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime < 60d / bpm) return;

            var randNum = _random.Next(5);
            var note1 = ObjectPool.Instance.NoteQueue.Dequeue().GetComponent<Note>();
            note1.NoteType = randNum;
            note1.transform.position = (randNum <= 1 || randNum == 4 ? tfLeftNoteAppear : tfRightNoteAppear).position;

            GameObject obj;
            (obj = note1.gameObject).SetActive(true);
            _timingManager.noteList.Add(obj);

            if (randNum == 4)
            {
                var note2 = ObjectPool.Instance.NoteQueue.Dequeue().GetComponent<Note>();
                note2.NoteType = 5;
                note2.transform.position = tfRightNoteAppear.position;

                GameObject obj2;
                (obj2 = note2.gameObject).SetActive(true);
                _timingManager.noteList.Add(obj2);
            }

            _currentTime -= 60d / bpm;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Note")) return;
            _counterpartController.Motion(other.GetComponent<Note>().NoteType);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Note")) return;

            if (other.GetComponent<Note>().NoteImageEnabled)
            {
                _scoreManager.ResetCombo();
                _effectManager.JudgementEffect(4);
            }

            var obj = other.gameObject;
            _timingManager.noteList.Remove(obj);
            ObjectPool.Instance.NoteQueue.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}