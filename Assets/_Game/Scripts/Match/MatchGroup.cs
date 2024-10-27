using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class MatchGroup : MonoBehaviour
    {
        public List<SingleGroup> SingleGroups = new List<SingleGroup>();

        
        //TODO check matches
        public void SpawnRow()
        {
            foreach (var singleGroup in SingleGroups)
            {
                singleGroup.AddQueue();
            }
        }
    }
}