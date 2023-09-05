using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace StumpMirror
{
    // Constants for easier maintenance
    public static class GameObjectPaths
    {
        public const string Mirror = "Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/mirror (1)";
    }

    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        // Called when the mod is enabled
        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        // Called when the mod is disabled
        void OnDisable()
        {
            SetupMirror(true);
            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        // Called when the game is initialized
        void OnGameInitialized(object sender, EventArgs e)
        {
            SetupMirror(true);
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            // Activate your mod here
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            // Deactivate your mod here
        }

        // Helper method to set up or tear down the mirror
        private void SetupMirror(bool isActive)
        {
            GameObject mirror = GameObject.Find(GameObjectPaths.Mirror);
            if (mirror != null)
            {
                mirror.SetActive(isActive);
                Transform[] allChildren = mirror.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    GameObject.Destroy(child.GetComponent<Collider>());
                }
            }
        }
    }
}
