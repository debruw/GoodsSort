using UnityEngine;

namespace GameTemplate.Utils
{
    /// <summary>
    /// Singleton class which saves/loads local settings.
    /// (This is just a wrapper around the PlayerPrefs system,
    /// so that all the calls are in the same place.)
    /// </summary>
    public static class UserPrefs
    {
        const string k_SoundStateKey = "SoundState";
        const string k_MusicStateKey = "MusicState";
        const string k_LevelIdKey = "LevelId";
        const string k_CurrencyKey = "Currency";

        public static bool GetSoundState()
        {
            return PlayerPrefs.GetInt(k_SoundStateKey, 1) == 1;
        }

        public static void SetSoundState(bool state)
        {
            PlayerPrefs.SetInt(k_SoundStateKey, state ? 1 : 0);
        }

        public static bool GetMusicState()
        {
            return PlayerPrefs.GetInt(k_MusicStateKey, 1) == 1;
        }

        public static void SetMusicState(bool state)
        {
            PlayerPrefs.SetInt(k_MusicStateKey, state ? 1 : 0);
        }

        public static int GetLevelId()
        {
            return PlayerPrefs.GetInt(k_LevelIdKey, 0);
        }
        
        public static void SetLevelId(int newLevelId)
        {
            PlayerPrefs.SetInt(k_LevelIdKey, newLevelId);
        }
        
        public static int GetCurrency(int currencyId, int currencyAmount)
        {
            return PlayerPrefs.GetInt(k_CurrencyKey + currencyId, currencyAmount);
        }

        public static void SetCurrency(int currencyId, int newCurrencyAmount)
        {
            PlayerPrefs.SetInt(k_CurrencyKey + currencyId, newCurrencyAmount);
        }
    }
}