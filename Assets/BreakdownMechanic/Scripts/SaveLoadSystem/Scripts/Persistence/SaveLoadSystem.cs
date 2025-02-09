using System;
using System.Collections.Generic;
using System.Linq;
using BreakdownMechanic.Scripts.Malfunctions.Vents.Fan;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.Persistence {
    [Serializable] public class GameData { 
        public string Name;
        public string CurrentLevelName;

        public FanOverpoweredMalfunction.FanData FanData;
        public FilterCloggingMalfunction.FilterData FilterData;
    }
        
    public interface ISaveable  {
        SerializableGuid Id { get; set; }
    }
    
    public interface IBind<TData> where TData : ISaveable {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }
    
    public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem> {
        [SerializeField] public GameData gameData;
        
        IDataService dataService;

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }
        
        void Start() => NewGame();

        void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            BindScriptableObject<FilterCloggingMalfunction, FilterCloggingMalfunction.FilterData>(gameData.FilterData);
            BindScriptableObject<FanOverpoweredMalfunction, FanOverpoweredMalfunction.FanData>(gameData.FanData);
            
            
            Bind<DiagnosticTempWidget, FilterCloggingMalfunction.FilterData>(gameData.FilterData);
            Bind<DiagnosticTempWidget, FanOverpoweredMalfunction.FanData>(gameData.FanData);
            Bind<VentsMalfunctionController, FanOverpoweredMalfunction.FanData>(gameData.FanData);
            Bind<VentsMalfunctionController, FilterCloggingMalfunction.FilterData>(gameData.FilterData);
            Bind<FanController, FanOverpoweredMalfunction.FanData>(gameData.FanData);
            Bind<FilterController, FilterCloggingMalfunction.FilterData>(gameData.FilterData);
        }
        
        void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new() {
            var entity = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null) {
                if (data == null) {
                    data = new TData { Id = entity.Id };
                }
                entity.Bind(data);
            }
        }

        void Bind<T, TData>(List<TData> datas) where T: MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach(var entity in entities) {
                var data = datas.FirstOrDefault(d=> d.Id == entity.Id);
                if (data == null) {
                    data = new TData { Id = entity.Id };
                    datas.Add(data); 
                }
                entity.Bind(data);
            }
        }

        void BindScriptableObject<T, TData>(TData data) where T: ScriptableObject, IBind<TData> where TData : ISaveable, new() {
            var entity = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            if (entity != null) {
                if (data == null) {
                    data = new TData { Id = entity.Id };
                }
                entity.Bind(data);
            }
            else
            {
                Debug.Log("Can't find");
            }
        }
        
        void BindScriptableObject<T, TData>(List<TData> datas) where T: ScriptableObject, IBind<TData> where TData : ISaveable, new() {
            var entities = Resources.FindObjectsOfTypeAll<T>();

            foreach(var entity in entities) {
                var data = datas.FirstOrDefault(d=> d.Id == entity.Id);
                if (data == null) {
                    data = new TData { Id = entity.Id };
                    datas.Add(data); 
                }
                entity.Bind(data);
            }
        }

        public void NewGame() {
            gameData = new GameData {
                Name = "Game",
                CurrentLevelName = "SampleScene 1"
            };
            SceneManager.LoadScene(gameData.CurrentLevelName);
        }
        
        public void SaveGame() => dataService.Save(gameData);

        public void LoadGame(string gameName) {
            gameData = dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.CurrentLevelName)) {
                gameData.CurrentLevelName = "SampleScene 1";
            }

            SceneManager.LoadScene(gameData.CurrentLevelName);
        }
        
        public void ReloadGame() => LoadGame(gameData.Name);

        public void DeleteGame(string gameName) => dataService.Delete(gameName);
    }
}