using Studio.Models;
using Studio.Settings;
using Studio.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace Studio.ProjectSystems
{
    public class DataSystem : MonoBehaviour
    {
        private Dictionary<CacheType, string> _cacheDataPathes;

        public CachedUserData CachedUserData { get; private set; }
        public AppSettingsData AppSettingsData { get; private set; }
        public PurchaseData PurchaseData { get; private set; }

        [Inject]
        public void Construct()
        {
            Utilities.Logger.Log("DataSystem Construct", LogTypes.Info);

            EventBus.OnSystemsBindedEvent += OnSystemsBindedEventHandler;
        }

        public void Initialize()
        {
            FillCacheDataPathes();

            if (!Directory.Exists(AppConstants.PATH_TO_GAMES_CACHE))
            {
                Directory.CreateDirectory(AppConstants.PATH_TO_GAMES_CACHE);
            }

            StartLoadCache();
        }

        private void StartLoadCache()
        {
            for (int i = 0; i < Enum.GetNames(typeof(CacheType)).Length; i++)
            {
                LoadCachedData((CacheType)i);
            }

            EventBus.OnCacheLoadedEvent?.Invoke();
        }

        public void SaveAllCache()
        {
            int count = Enum.GetNames(typeof(CacheType)).Length;
            for (int i = 0; i < count; i++)
            {
                SaveCache((CacheType)i);
            }
        }

        public void SaveCache(CacheType type)
        {
            if (!File.Exists(_cacheDataPathes[type]))
            {
                File.Create(_cacheDataPathes[type]).Close();
            }

            switch (type)
            {
                case CacheType.UserLocalData:
                    File.WriteAllText(_cacheDataPathes[type], InternalTools.SerializeData(CachedUserData));
                    break;

                case CacheType.AppSettingsData:
                    File.WriteAllText(_cacheDataPathes[type], InternalTools.SerializeData(AppSettingsData));
                    break;

                case CacheType.PurchaseData:
                    File.WriteAllText(_cacheDataPathes[type], InternalTools.SerializeData(PurchaseData));
                    break;

                default:
                    Utilities.Logger.Log($"[{type}] is not implemented", LogTypes.Warning);
                    break;
            }
        }

        private void LoadCachedData(CacheType type)
        {
            switch (type)
            {
                case CacheType.UserLocalData:
                    if (!File.Exists(_cacheDataPathes[type]))
                    {
                        CachedUserData = new CachedUserData()
                        {
                            //crystals = 0,
                            //deathCount = 0,
                            //killedEnemy = 0,
                            //money = 0,
                            //isTutorialComplete = false,
                        };

                        SaveCache(type);
                    }
                    else
                    {
                        CachedUserData = InternalTools.DeserializeData<CachedUserData>(File.ReadAllText(_cacheDataPathes[type]));
                    }
                    break;

                case CacheType.AppSettingsData:
                    if (!File.Exists(_cacheDataPathes[type]))
                    {
                        AppSettingsData = new AppSettingsData()
                        {
                            isFirstRun = true,
                            appLanguage = Languages.English,
                            musicVolume = 1,
                            soundVolume = 1,
                        };

                        SaveCache(type);
                    }
                    else
                    {
                        AppSettingsData = InternalTools.DeserializeData<AppSettingsData>(File.ReadAllText(_cacheDataPathes[type]));
                    }
                    break;

                case CacheType.PurchaseData:
                    if (!File.Exists(_cacheDataPathes[type]))
                    {
                        PurchaseData = new PurchaseData()
                        {
                            isRemovedAds = false,
                        };

                        SaveCache(type);
                    }
                    else
                    {
                        PurchaseData = InternalTools.DeserializeData<PurchaseData>(File.ReadAllText(_cacheDataPathes[type]));
                    }
                    break;

                default:
                    {
                        Utilities.Logger.Log($"[{type}] is not implemented", LogTypes.Warning);
                        return;
                    }
            }
        }

        private void FillCacheDataPathes()
        {
            _cacheDataPathes = new Dictionary<CacheType, string>
            {
                { CacheType.UserLocalData, Application.persistentDataPath + AppConstants.LOCAL_USER_DATA_FILE_PATH },
                { CacheType.AppSettingsData, Application.persistentDataPath + AppConstants.LOCAL_APP_DATA_FILE_PATH },
                { CacheType.PurchaseData, Application.persistentDataPath + AppConstants.LOCAL_PURCHASE_DATA_FILE_PATH },
            };
        }

        public void ResetData(CacheType type)
        {
            switch (type)
            {
                case CacheType.UserLocalData:

                    CachedUserData = new CachedUserData()
                    {
                        //crystals = 0,
                        //deathCount = 0,
                        //killedEnemy = 0,
                        //money = 0,
                        //isTutorialComplete = false,
                    };

                    break;

                case CacheType.AppSettingsData:

                    AppSettingsData = new AppSettingsData()
                    {
                        isFirstRun = true,
                        appLanguage = Languages.English,
                        musicVolume = 1,
                        soundVolume = 1,
                    };

                    break;

                case CacheType.PurchaseData:

                    PurchaseData = new PurchaseData()
                    {
                        isRemovedAds = false,
                    };

                    break;

                default:
                    {
                        Utilities.Logger.Log($"[{type}] is not implemented", LogTypes.Warning);
                        return;
                    }
            }

            SaveCache(type);

            EventBus.OnCacheResetEvent?.Invoke(type);
        }

        private void OnSystemsBindedEventHandler()
        {
            Initialize();
        }
    }
}