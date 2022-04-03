using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] private static float _speed = 700;

    public static readonly KeyCode[] KeyMapping =
        {KeyCode.A, KeyCode.LeftArrow, KeyCode.D, KeyCode.RightArrow, KeyCode.W, KeyCode.W};

    [SerializeField] private Sprite[] noteSprites;

    private Image _noteImage;

    // 0: 왼쪽 주먹, 1:왼쪽 가드, 2: 왼쪽 위빙, 3: 오른쪽 주먹, 4:오른쪽 가드, 5: 오른쪽 위빙
    // 0: 왼쪽 주먹, 1: 왼쪽 위빙, 2: 오른쪽 주먹, 3: 오른쪽 위빙, 4: 왼쪽 가드, 5: 오른쪽 가드
    private int _noteType;
    private RectTransform _rectTran;

    public int NoteType
    {
        get => _noteType;
        set
        {
            Debug.Assert(value <= 5);
            _noteType = value;
        }
    }

    public bool NoteImageEnabled
    {
        get => _noteImage.enabled;
        set => _noteImage.enabled = value;
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value > 0) _speed = float.MaxValue;
            else Debug.Log("너무 느린 스피드");
        }
    }

    private void Awake()
    {
        _rectTran = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition +=
            (_noteType <= 1 || _noteType == 4 ? Vector3.right : Vector3.left) * _speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        if (_noteImage == null)
            _noteImage = GetComponent<Image>();

        _rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _noteType < 4 ? 170 : 92);
        _noteImage.sprite = noteSprites[_noteType];
        _noteImage.enabled = true;
    }

    public void HideNote()
    {
        _noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return _noteImage.enabled;
    }

    public void set_speed(float value)
    {
        _speed = value;
    }

    public void note_on()
    {
    }
}