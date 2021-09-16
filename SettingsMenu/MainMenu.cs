using ModMaker;
using UnityModManagerNet;
using TutorialCanvas.Utilities;
using static TutorialCanvas.Main;
using GL = UnityEngine.GUILayout;

namespace TutorialCanvas.SettingsMenu
{
    internal class MainMenu : IModEventHandler, IMenuSelectablePage
    {
        public int Priority => 200;

        public string Name => "MainMenu";

#if DEBUG
        public void OnGUI(UnityModManager.ModEntry modEntry)
        {

            if (GL.Button("Load bundle", GL.ExpandWidth(false)))
            {
                RefreshUI();
            }
            if (GL.Button("Toggle Reuse internal assets", GL.ExpandWidth(false)))
            {
                SettingsWrapper.Reuse = !SettingsWrapper.Reuse;
                RefreshUI();
            }
        }

        private void RefreshUI()
        {
            Mod.Core.UI.Clear();
            BundleManger.AddBundle("tutorialcanvas");
            Mod.Core.UI.Update();
        }
#endif

#if !DEBUG
        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
        }
#endif
        public void HandleModDisable()
        {
        }

        public void HandleModEnable()
        {
        }
    }
}