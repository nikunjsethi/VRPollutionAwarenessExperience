using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Transition : MonoBehaviour
{
    [Header("Trigger")]
    public int triggerIndex = 4;               // when Question_handler.currentIndex == this -> start transition
    [Tooltip("Delay (seconds) before fade starts (set ~3s as requested)")]
    public float initialDelaySeconds = 3f;     // ~3 seconds before fade

    [Header("Black Screen (assign your existing object)")]
    [Tooltip("Assign the GameObject you created named 'Black Screen' (should be under CenterEyeAnchor).")]
    public GameObject blackScreen;

    [Header("Fade settings")]
    public float fadeDuration = 2f;            // how long the fade takes

    [Header("Scene build indexes for scores 0..4")]

    bool transitionStarted = false;

    // detected components on blackScreen (one of these will be used)
    Image fadeImage;
    CanvasGroup fadeCanvasGroup;
    Renderer fadeRenderer;

    void Start()
    {
        if (blackScreen == null)
        {
            Debug.LogError("[STM-NoAudio] Assign your Black Screen GameObject in the inspector. Aborting fade setup.");
            return;
        }

        // detect common fade components on the assigned object
        fadeImage = blackScreen.GetComponentInChildren<Image>(true);
        fadeCanvasGroup = blackScreen.GetComponentInChildren<CanvasGroup>(true);

        // If image or canvas group not found, check for a Renderer (Quad)
        if (fadeImage == null && fadeCanvasGroup == null)
        {
            Renderer r = blackScreen.GetComponentInChildren<Renderer>(true);
            if (r != null) fadeRenderer = r;
        }

        // initialize alpha to transparent
        if (fadeImage != null) SetImageAlpha(0f);
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 0f;
        if (fadeRenderer != null) SetRendererAlpha(0f);
    }

    // call this from your Question_handler when index changes
    public void OnIndexUpdated(int currentIndex, int score)
    {
        if (transitionStarted) return;

        if (currentIndex == triggerIndex)
            StartTransition(score);
    }

    void StartTransition(int score)
    {
        if (transitionStarted) return;
        transitionStarted = true;

        StartCoroutine(TransitionSequence(score));
    }

    IEnumerator TransitionSequence(int score)
    {
        // 1) initial delay (about 3s by default)
        if (initialDelaySeconds > 0f)
            yield return new WaitForSeconds(initialDelaySeconds);

        // 2) start fade (no audio involved)
        if (fadeImage != null)
            StartCoroutine(FadeImageTo(1f, fadeDuration));
        else if (fadeCanvasGroup != null)
            StartCoroutine(FadeCanvasGroupTo(1f, fadeDuration));
        else if (fadeRenderer != null)
            StartCoroutine(FadeRendererTo(1f, fadeDuration));
        else
            Debug.LogWarning("[STM-NoAudio] No fade component found on assigned Black Screen. No visual fade will run.");

        // 3) wait for fade duration
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // ensure fully opaque
        if (fadeImage != null) SetImageAlpha(1f);
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 1f;
        if (fadeRenderer != null) SetRendererAlpha(1f);
        if (score == 4)
            score = 1;
        else if (score == 3)
            score = 2;
        else if (score == 2)
            score = 3;
        else if (score == 1)
            score = 4;
        else
            score = 5;
        // 4) load scene by build index
        SceneManager.LoadScene(score);
    }

    #region Fade helpers
    IEnumerator FadeImageTo(float target, float duration)
    {
        if (fadeImage == null) yield break;
        float start = fadeImage.color.a;
        float time = 0f;
        fadeImage.raycastTarget = true;
        while (time < duration)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(start, target, time / duration);
            SetImageAlpha(a);
            yield return null;
        }
        SetImageAlpha(target);
        fadeImage.raycastTarget = false;
    }

    void SetImageAlpha(float a)
    {
        if (fadeImage == null) return;
        Color c = fadeImage.color; c.a = Mathf.Clamp01(a); fadeImage.color = c;
    }

    IEnumerator FadeCanvasGroupTo(float target, float duration)
    {
        if (fadeCanvasGroup == null) yield break;
        float start = fadeCanvasGroup.alpha;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }
        fadeCanvasGroup.alpha = target;
    }

    IEnumerator FadeRendererTo(float target, float duration)
    {
        if (fadeRenderer == null) yield break;
        Material mat = fadeRenderer.material;
        Color startCol = GetMaterialColor(mat);
        float startA = startCol.a;
        float time = 0f;
        // Ensure material uses a Transparent rendering mode for alpha to show (user must set shader)
        while (time < duration)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(startA, target, time / duration);
            SetMaterialAlpha(mat, a);
            yield return null;
        }
        SetMaterialAlpha(mat, target);
    }

    Color GetMaterialColor(Material m)
    {
        if (m == null) return Color.black;
        if (m.HasProperty("_Color")) return m.color;
        if (m.HasProperty("color")) return m.GetColor("color");
        return Color.black;
    }

    void SetMaterialAlpha(Material m, float a)
    {
        if (m == null) return;
        a = Mathf.Clamp01(a);
        if (m.HasProperty("_Color"))
        {
            Color c = m.color; c.a = a; m.color = c;
        }
        else if (m.HasProperty("color"))
        {
            Color c = m.GetColor("color"); c.a = a; m.SetColor("color", c);
        }
    }

    void SetRendererAlpha(float a)
    {
        if (fadeRenderer == null) return;
        SetMaterialAlpha(fadeRenderer.material, a);
    }

    void SetRendererAlpha(GameObject go, float a)
    {
        Renderer r = go.GetComponent<Renderer>();
        if (r == null) return;
        SetMaterialAlpha(r.material, a);
    }
    #endregion
}