using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace DailyRewards_V1.Scripts.Core
{
    public class WindowEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Template/Save/Delete")]
        private static void DeleteSave()
        {
            if (File.Exists(GetSavePath()))
            {
                File.Delete(GetSavePath());
                Debug.Log("Save data deleted!");
            }
            else
            {
                Debug.Log("No Save data to delete found");
            }

            string GetSavePath()
            {
                return Application.persistentDataPath + "/saveData.json";
            }
        }
#endif
    }
}