using GameTemplate._Game.Scripts.Match;
using UnityEngine;

namespace _Game.Scripts.Timer
{
    public class PopEffectController : MonoBehaviour
    {
        public GameObject triplePopEffect;

        private void Awake()
        {
            MatchGroup.OnMatched += MatchGroupOnOnMatched;
        }

        private void OnDestroy()
        {
            MatchGroup.OnMatched -= MatchGroupOnOnMatched;
        }

        private void MatchGroupOnOnMatched(Vector3 point)
        {
            //convert to screen position
            point = Camera.main.WorldToScreenPoint(point);
            Destroy(Instantiate(triplePopEffect, point, Quaternion.identity, transform), 2);
        }
    }
}