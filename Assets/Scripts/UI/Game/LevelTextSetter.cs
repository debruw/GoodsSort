using TMPro;
using UnityEngine;

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