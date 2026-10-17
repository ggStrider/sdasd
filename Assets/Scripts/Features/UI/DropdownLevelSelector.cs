using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Features.UI
{
    public class DropdownLevelSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private LevelSettings[] _levels;
        
        public string CurrentLevel { get; private set; }

        private void Awake()
        {
            _dropdown.ClearOptions();

            _dropdown.AddOptions(GetOptionsDisplayStrings());
            _dropdown.onValueChanged.AddListener(OnDropdownLevelChanged);

            CurrentLevel = _levels[0].SceneName;
        }

        private void OnDropdownLevelChanged(int index)
        {
            CurrentLevel = _levels[index].SceneName;
        }

        private List<string> GetOptionsDisplayStrings()
        {
            List<string> f = new List<string>();

            for (int i = 0; i < _levels.Length; i++)
            {
                f.Add(_levels[i].DisplayName);
            }

            return f;
        }
    }

    [Serializable]
    public class LevelSettings
    {
        public string DisplayName;
        public string SceneName;
    }
}