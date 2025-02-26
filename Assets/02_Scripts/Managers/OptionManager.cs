using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    [Header("사운드 설정")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI bgmValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;

    [Header("키 바인딩 설정")]
    [SerializeField] private Button keyBindingButtonW;
    [SerializeField] private Button keyBindingButtonS;
    [SerializeField] private Button keyBindingButtonA;
    [SerializeField] private Button keyBindingButtonD;
    [SerializeField] private TextMeshProUGUI upBindingText;
    [SerializeField] private TextMeshProUGUI downBindingText;
    [SerializeField] private TextMeshProUGUI leftBindingText;
    [SerializeField] private TextMeshProUGUI rightBindingText;

    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    private string waitingForKey = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 사운드 슬라이더 변경 이벤트 등록
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // 키 바인딩 버튼 이벤트 등록
        keyBindingButtonW.onClick.AddListener(() => StartKeyBinding("Up"));
        keyBindingButtonA.onClick.AddListener(() => StartKeyBinding("Left"));
        keyBindingButtonS.onClick.AddListener(() => StartKeyBinding("Down"));
        keyBindingButtonD.onClick.AddListener(() => StartKeyBinding("Right"));

        // 초기값 설정
        bgmSlider.value = 1f;
        sfxSlider.value = 1f;
        keyBindings["Up"] = KeyCode.W;
        keyBindings["Left"] = KeyCode.A;
        keyBindings["Down"] = KeyCode.S;
        keyBindings["Right"] = KeyCode.D;

        UpdateVolumeUI();
        UpdateKeyBindingUI();
    }

    /// <summary>
    /// BGM 볼륨 설정
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.SetBGMVolume(volume);
        UpdateVolumeUI();
    }

    /// <summary>
    /// SFX 볼륨 설정
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        SoundManager.instance.SetSFXVolume(volume);
        UpdateVolumeUI();
    }

    /// <summary>
    /// 현재 볼륨 값을 UI에 표시
    /// </summary>
    private void UpdateVolumeUI()
    {
        bgmValueText.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
        sfxValueText.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
    }

    /// <summary>
    /// 키 바인딩 설정 시작
    /// </summary>
    private void StartKeyBinding(string key)
    {
        waitingForKey = key;
    }

    private void Update()
    {
        if (waitingForKey != null && Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    keyBindings[waitingForKey] = key;
                    waitingForKey = null;
                    Debug.Log($"키 바인딩 변경: {key}");

                    // UI 업데이트
                    UpdateKeyBindingUI();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 현재 키 바인딩을 UI에 표시
    /// </summary>
    private void UpdateKeyBindingUI()
    {
        upBindingText.text = keyBindings["Up"].ToString();
        downBindingText.text = keyBindings["Down"].ToString();
        leftBindingText.text = keyBindings["Left"].ToString();
        rightBindingText.text = keyBindings["Right"].ToString();
    }

    /// <summary>
    /// 바인딩된 키 반환
    /// </summary>
    public KeyCode GetKey(string action)
    {
        return keyBindings.ContainsKey(action) ? keyBindings[action] : KeyCode.None;
    }
}