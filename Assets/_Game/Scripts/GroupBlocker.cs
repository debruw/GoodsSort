using System;
using GameTemplate._Game.Scripts.Match;
using TMPro;
using UnityEngine;

namespace _Game.Scripts
{
    public class GroupBlocker : MonoBehaviour
    {
        #region Variables

        // Public Variables
        public TextMeshProUGUI _countText;
        public int StartCount = 5;

        // Private Variables
        private int _count;
        
        #endregion Variables

        private void Awake()
        {
            _count = StartCount;
            _countText.text = _count.ToString();
            
            MatchGroup.OnMatched += OnMatched;
        }

        private void OnDestroy()
        {
            MatchGroup.OnMatched -= OnMatched;
        }

        public void OnMatched(Vector3 position)
        {
            _count--;
            _countText.text = _count.ToString();

            if (_count == 0)
            {
                GetComponentInParent<MatchGroup>().BlockerDeactivated();
                gameObject.SetActive(false);
            }
        }

        public void Initialize(int blockCount)
        {
            StartCount = blockCount;
        }
    }
}