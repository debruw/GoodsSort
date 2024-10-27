using GameTemplate.Managers.SceneManagers;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate.UI
{
    public class LevelTextSetter : MonoBehaviour
    {
        public void SetLevelText(int levelID)
        {
            GetComponent<TextMeshProUGUI>().text = "Level " + levelID;
        }
    }
}