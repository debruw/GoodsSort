using System;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using DG.Tweening;
using GameTemplate._Game.Scripts.Match;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Timer
{
    public class StarController : MonoBehaviour
    {
        #region Variables

        // Public Variables
        public TextMeshProUGUI _countText;
        public GameObject starPrefab;
        public Transform starIcon;

        public int StarCount
        {
            get { return _starCount; }
        }

        // Private Variables
        private int _starCount = 0;
        List<ParticleImage> particles = new List<ParticleImage>();

        #endregion

        private void Awake()
        {
            MatchGroup.OnMatched += SpawnParticle;
        }

        private void OnDestroy()
        {
            MatchGroup.OnMatched -= SpawnParticle;
        }

        private void SpawnParticle(Vector3 point)
        {
            //convert to screen position
            point = Camera.main.WorldToScreenPoint(point);

            int spawnCount = ComboController.Instance.ComboCount >= 3 ? 3 : ComboController.Instance.ComboCount + 1;

            //Create particle
            ParticleImage particle = Instantiate(starPrefab, transform)
                .GetComponent<ParticleImage>();
            particle.AddBurst(0, spawnCount);
            particle.rectTransform.position = point;
            particle.attractorTarget = starIcon;
            particle.onAnyParticleFinished.AddListener(EarnStar);
            particle.onParticleStop.AddListener(DestroyParticle);
            particle.Play();
            particles.Add(particle);
        }

        public void EarnStar()
        {
            _starCount++;
            _countText.text = _starCount.ToString();
        }

        private void DestroyParticle()
        {
            particles[0].onAnyParticleFinished.RemoveListener(EarnStar);
            particles[0].onParticleStop.RemoveListener(DestroyParticle);
            Destroy(particles[0].gameObject);
            particles.RemoveAt(0);
        }
    }
}