using static TutorialCanvas.Main;

namespace TutorialCanvas.Utilities
{
    public static class SettingsWrapper
    {
        public static string LocalizationFileName
        {
            get => Mod.Settings.localizationFileName;
            set => Mod.Settings.localizationFileName = value;
        }

        public static string ModPath
        {
            get => Mod.Settings.modPath;
            set => Mod.Settings.modPath = value;
        }

        public static bool Reuse
        {
            get => Mod.Settings.reuse;
            set => Mod.Settings.reuse = value;
        }
    }
}