using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace  GameFrame.Multilingual
{
    public enum ELanguageType : byte
    {
        Zh,   // Simplified Chinese
        En    // English
    }
    
    /// <summary>
    /// 当切换语言时,进行广播
    /// </summary>
    public struct SChangeMultingualEvent
    {
    }
    
    public class MultilingualManager : AbstractSystem
    {
        public TextAsset LanguageFile;
        
        public ELanguageType willChangeLanguageType = ELanguageType.Zh;
        
        private Dictionary<string, Dictionary<ELanguageType, string>> translations = new Dictionary<string, Dictionary<ELanguageType, string>>();

        protected override void OnInit()
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitLanguage()
        {
            if (LanguageFile)
            {
                string jsonText = LanguageFile.text;
                LanguageData[] languageDataArray = JsonUtility.FromJson<LanguageDataWrapper>(jsonText).languageData;
                
                foreach (var entry in languageDataArray)
                {
                    var languageDictionary = new Dictionary<ELanguageType, string>
                    {
                        {
                            ELanguageType.Zh, entry.SimplifiedChinese
                        },
                        {
                            ELanguageType.En, entry.English
                        }
                    };
                    translations[entry.Key] = languageDictionary;
                }
            }
        }
        
        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="newLanguageType"></param>
        public void ChangeLanguage(ELanguageType newLanguageType)
        {
            willChangeLanguageType = newLanguageType;
            TypeEventSystem.Global.Send(new SChangeMultingualEvent());
        }

        /// <summary>
        /// 获取当前多语言文本
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetMultilingual_Text(string key)
        {
            if (translations.Count == 0)
                return "";
            
            if (translations.ContainsKey(key) && translations[key].ContainsKey(willChangeLanguageType))
            {
                return translations[key][willChangeLanguageType];
            }

            return "";
        }
        
        /// <summary>
        /// 获取当前多语言图片
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Sprite GetMultilingual_Sprite(string key)
        {
            string folderName = willChangeLanguageType == ELanguageType.Zh ? "Zh" : "En";
            string path = $"Multilingual/{folderName}/{key}";
            Sprite sprite = Resources.Load<Sprite>(path);
            if (sprite == null) 
            {
                Debug.LogWarning($"Sprite not found at path: {path}");
            }
            return sprite;
        }
        
        [System.Serializable]
        private struct LanguageDataWrapper
        {
            public LanguageData[] languageData;
        }
        
        [System.Serializable]
        private struct LanguageData
        {
            public string Key;
            
            public string SimplifiedChinese;
            
            public string English;
        }
    }
}


