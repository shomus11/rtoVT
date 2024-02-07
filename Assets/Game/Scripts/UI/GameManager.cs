using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum GameStates
{
    MainMenu,
    Gameplay,
    Pause,
    Resumed,
    Victory,
    Defeat
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Attribute")]
    public RectTransform mainMenuUIContainer;
    public RectTransform pauseUIContainer;
    public RectTransform endGameUIContainer;
    bool pauseOpened = false;
    float baseAnimationDuration = 0.25f;
    [Space(10)]

    [Header("EndGameAttribute")]
    public RectTransform victoryTittle;
    public RectTransform defeatTittle;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killText;


    [Header("Fade UI Attribute")]
    public RectTransform transitionImage;
    public RectTransform loadingImage;

    [Header("Optional (set false if not using scenes)")]
    public GameStates gameState;
    public bool usingScenes = true;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameStates.MainMenu || gameState != GameStates.Victory || gameState != GameStates.Defeat)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseOrResumeGame();

            if (Input.GetKeyDown(KeyCode.P))
            {
                FadeOut("Victory");
                SetScoreOrKillData(50000, scoreText, true);
                SetScoreOrKillData(500, killText, false);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                FadeOut("Defeat");
                SetScoreOrKillData(1000, scoreText, true);
                SetScoreOrKillData(10, killText, false);
            }
        }
        if (gameState == GameStates.MainMenu)
            if (Input.GetKeyDown(KeyCode.Escape))
                QuitGame();

    }

    public void ClickButton()
    {
        SoundManager.instance.ButtonClicked();
    }

    public void SetScoreOrKillData(int targetData, TextMeshProUGUI textTarget, bool score = true)
    {
        if (score)
            textTarget.text = $"scores : {targetData}";
        if (!score)
        {
            textTarget.text = $"kills : {targetData}";
        }
    }

    public void PauseOrResumeGame()
    {
        if (!pauseOpened)
            OpenUI(pauseUIContainer);
        else
            CloseUI(pauseUIContainer);
    }

    void Setup()
    {
        SwitchGameStates(GameStates.MainMenu);
        pauseOpened = false;

        if (mainMenuUIContainer.gameObject)
            OpenUI(mainMenuUIContainer, 0);

        if (pauseUIContainer.gameObject)
            CloseUI(pauseUIContainer, 0);

        if (endGameUIContainer)
            CloseUI(endGameUIContainer, 0);

        if (defeatTittle.gameObject)
            defeatTittle.gameObject.SetActive(false);

        if (defeatTittle.gameObject)
            victoryTittle.gameObject.SetActive(false);

        SoundManager.instance.SwitchOrPlayBGM("mainmenu_sound");
        FadeIn();
    }

    public void OpenUI(RectTransform target, float durationMultipliyed = 1)
    {
        if (target == pauseUIContainer)
        {
            DOTween.PauseAll();
            SwitchGameStates(GameStates.Pause);
        }


        if (target == pauseUIContainer)
            pauseOpened = true;

        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        if (target.gameObject)
            target.gameObject.SetActive(true);

        sequence.Insert(totalAnimationDuration, target.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;

    }

    public void CloseUI(RectTransform target, float durationMultipliyed = 1)
    {
        if (target == pauseUIContainer)
        {
            DOTween.PlayAll();
            SwitchGameStates(GameStates.Gameplay);
        }
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, target.DOScale(Vector3.zero, baseAnimationDuration * durationMultipliyed).From(Vector3.one).SetEase(Ease.OutBack)).OnComplete(() =>
          {
              if (target == pauseUIContainer)
                  pauseOpened = false;

              if (target.gameObject)
                  target.gameObject.SetActive(false);
          });
        totalAnimationDuration += baseAnimationDuration;
    }

    public void QuitGame()
    {
        FadeOut(true);
    }

    public void SwitchGameStates(GameStates states)
    {
        Debug.Log("Test state : " + states);
        gameState = states;
    }


    public void FadeIn()
    {
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(0, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.zero, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
    }

    public void FadeOutFadeIn()
    {
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(0, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.zero, baseAnimationDuration).From(Vector3.one).SetEase(Ease.OutBack));
    }
    public void FadeOut()
    {
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, loadingImage.DOScale(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack));
    }

    public void FadeOut(string sceneName)
    {
        Debug.Log(sceneName);
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack)).OnComplete(() =>
        {
            if (sceneName != "MainMenu")
            {
                CloseUI(mainMenuUIContainer, 0);
                if (sceneName == "Gameplay")
                {
                    gameState = GameStates.Gameplay;
                    CloseUI(pauseUIContainer, 0);
                    CloseUI(endGameUIContainer, 0);
                    //testing sound changed
                    ResetScore();
                    SoundManager.instance.SwitchOrPlayBGM("gameplay_sound");
                    StageManager.Instance.CurrentStage = -1;
                    StageManager.Instance.InitNPC();
                    PlayerController.instance.gameObject.SetActive(true);
                    PlayerController.instance.InitPlayer();

                    //SceneManager.LoadScene(sceneName);
                }
                else if (sceneName == "Defeat")
                {
                    defeatTittle.gameObject.SetActive(true);
                    victoryTittle.gameObject.SetActive(false);
                    SwitchGameStates(GameStates.Defeat);
                    //StageManager.Instance.ResetNPC();
                    SetScoreOrKillData((int)score, scoreText, true);
                    SetScoreOrKillData(kill, killText, false);
                    CloseUI(pauseUIContainer, 0);
                    OpenUI(endGameUIContainer, 1);
                }
                else if (sceneName == "Victory")
                {
                    defeatTittle.gameObject.SetActive(false);
                    victoryTittle.gameObject.SetActive(true);
                    SwitchGameStates(GameStates.Victory);
                    SetScoreOrKillData((int)score, scoreText, true);
                    SetScoreOrKillData(kill, killText, false);
                    CloseUI(pauseUIContainer, 0);
                    OpenUI(endGameUIContainer, 1);
                }

                FadeIn();
            }
            else
            {
                Setup();
            }

            if (usingScenes)
                SceneManager.LoadScene(sceneName);
        });
    }
    public void FadeOut(bool quit)
    {
        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack)).OnComplete(() =>
        {
            Application.Quit();
        });

    }


    int kill = 0;
    float score = 0;
    void ResetScore()
    {
        kill = 0;
        score = 0;
    }
    public void AddKillAndScore(float scoreMultiplier)
    {
        kill++;
        score += scoreMultiplier * 100;
    }


}
