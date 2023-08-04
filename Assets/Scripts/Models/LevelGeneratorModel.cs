using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class LevelGeneratorModel
    {
        public List<GameObject> CurrentSegments = new();

        private int _numberOfLevelsForRepeating;
        

        public LevelGeneratorModel(int numberOfLevelsForRepeating)
        {
            _numberOfLevelsForRepeating = numberOfLevelsForRepeating;

        }
        
    }
}