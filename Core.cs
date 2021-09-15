﻿using Kingmaker.PubSubSystem;
using ModMaker;
using System;
using static TutorialCanvas.Main;
using static TutorialCanvas.Utilities.SettingsWrapper;

namespace TutorialCanvas
{
    internal class Core : IModEventHandler
    {
        public UI.UIController UI { get; internal set; }
        public int Priority => 200;

        public void ResetSettings()
        {
            Mod.ResetSettings();
            Mod.Settings.lastModVersion = Mod.Version.ToString();
            LocalizationFileName = Local.FileName;
        }

        public void HandleModEnable()
        {
            if (!string.IsNullOrEmpty(LocalizationFileName))
            {
                Local.Import(LocalizationFileName, e => Mod.Error(e));
                LocalizationFileName = Local.FileName;
            }
            if (!Version.TryParse(Mod.Settings.lastModVersion, out Version version) || version < new Version(0, 0, 0))
                ResetSettings();
            else
            {
                Mod.Settings.lastModVersion = Mod.Version.ToString();
            }
            Utilities.BundleManger.AddBundle("tutorialcanvas");
            //EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            UI = null;
            //EventBus.Unsubscribe(this);
        }
    }
}