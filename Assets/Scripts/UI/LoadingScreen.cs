using System.Collections;
using UnityEngine;

namespace GameTemplate
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup m_CanvasGroup;
        
        [SerializeField]
        float m_DelayBeforeFadeOut = 0.5f;

        [SerializeField]
        float m_FadeOutDuration = 0.1f;
        
        bool m_LoadingScreenRunning;
        
        Coroutine m_FadeOutCoroutine;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SetCanvasVisibility(false);
        }
        
        public void StartLoadingScreen(string sceneName)
        {
            SetCanvasVisibility(true);
            m_LoadingScreenRunning = true;
            if (m_LoadingScreenRunning)
            {
                if (m_FadeOutCoroutine != null)
                {
                    //Debug.Log("start loading screen");
                    StopCoroutine(m_FadeOutCoroutine);
                }
            }
        }
        
        public void StopLoadingScreen()
        {
            if (m_LoadingScreenRunning)
            {
                if (m_FadeOutCoroutine != null)
                {
                    //Debug.Log("stop loading screen");
                    StopCoroutine(m_FadeOutCoroutine);
                }
                m_FadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
            }
        }
        void SetCanvasVisibility(bool visible)
        {
            m_CanvasGroup.alpha = visible ? 1 : 0;
            m_CanvasGroup.blocksRaycasts = visible;
        }
        
        IEnumerator FadeOutCoroutine()
        {
            yield return new WaitForSeconds(m_DelayBeforeFadeOut);
            m_LoadingScreenRunning = false;

            float currentTime = 0;
            while (currentTime < m_FadeOutDuration)
            {
                m_CanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime / m_FadeOutDuration);
                yield return null;
                currentTime += Time.deltaTime;
            }

            SetCanvasVisibility(false);
        }
    }
}