using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Text LoadingPercentage;
    public Image LoadingProgressBar;
    
    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false; 
    
    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        
        instance.gameObject.SetActive(true);
        instance.componentAnimator.SetTrigger("ChoseStart");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        // Чтобы сцена не начала переключаться пока играет анимация closing:
        instance.loadingSceneOperation.allowSceneActivation = false;
        instance.LoadingProgressBar.fillAmount = 0;
    }
    
    private void Start()
    {
        instance = this;       
        instance.gameObject.SetActive(false);
        componentAnimator = GetComponent<Animator>();
        Debug.Log(shouldPlayOpeningAnimation);
        if (shouldPlayOpeningAnimation) 
        {
            componentAnimator.SetTrigger("ChoseEnd");
            instance.LoadingProgressBar.fillAmount = 1;
            
            // Чтобы если следующий переход будет обычным SceneManager.LoadScene, не проигрывать анимацию opening:
            shouldPlayOpeningAnimation = false; 
        }
    }

    private void Update()
    {
            Debug.Log(instance.loadingSceneOperation != null);
        if (instance.loadingSceneOperation != null)
        {
            Debug.Log(instance.loadingSceneOperation.progress * 100);
            instance.LoadingPercentage.text = Mathf.RoundToInt(instance.loadingSceneOperation.progress * 100) + "%";
            
            // Просто присвоить прогресс:
            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 
            
            // Присвоить прогресс с быстрой анимацией, чтобы ощущалось плавнее:
            instance.LoadingProgressBar.fillAmount = Mathf.Lerp(instance.LoadingProgressBar.fillAmount, instance.loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }

    public void OnAnimationOver()
    {
        // Чтобы при открытии сцены, куда мы переключаемся, проигралась анимация opening:
        shouldPlayOpeningAnimation = true;
        
        instance.loadingSceneOperation.allowSceneActivation = true;
    }
}