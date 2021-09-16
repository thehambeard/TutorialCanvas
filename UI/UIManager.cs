using DG.Tweening;
using Kingmaker;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TutorialCanvas.Utilities;
using static TutorialCanvas.Main;
using Kingmaker.UI.Selection;

namespace TutorialCanvas.UI
{
    internal class UIManager : MonoBehaviour
    {
        public const string Source = "TutorialCanvas";
        private Text _text;
        
        public static UIManager CreateObject()
        {
            //This is the method that get's called when it is time to create the UI.  This happens every time a scene is loaded.

            try
            {
                if (!Game.Instance.UI.Canvas) return null;
                if (!BundleManger.IsLoaded(Source)) throw new NullReferenceException();

                //
                //Attempt to get the wrath objects needed to build the UI
                //
                var staticCanvas = Game.Instance.UI.Canvas.RectTransform;
                var background = staticCanvas.Find("HUDLayout/CombatLog_New/Background/Background_Image").GetComponent<Image>(); //Using the path we found earlier we get the sprite component 

                //
                //Attempt to get the objects loaded from the AssetBundles and build the window.
                //
                var window = Instantiate(BundleManger.LoadedPrefabs[Source].transform.Find("WeaponWindow")); //We ditch the TutorialCanvas as talked about in the Wiki, we will attach it to a different parent
                window.SetParent(staticCanvas, false); //Attaches our window to the static canvas
                window.SetAsFirstSibling(); //Our window will always be under other UI elements as not to interfere with the game. Top of the list has the lowest priority
                if(SettingsWrapper.Reuse) window.Find("Background").GetComponent<Image>().sprite = background.sprite; //Sets the background sprite to the one used in CombatLog_New
                
                return window.gameObject.AddComponent<UIManager>(); //This adds this class as a component so it can handle events, button clicks, awake, update, etc.
            }
            catch (Exception ex)
            {
                Mod.Error(ex.StackTrace);
            }
            return new UIManager();
        }

        private void Awake()
        {
            //This is a unity message that runs once when the script activates (Check Unity documenation for the differences between Start() and Awake()

            //
            // Setup the listeners when the script starts
            //

            var button = this.transform.Find("Foreground/Button").GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(new UnityAction(HandleButtonClick));
            button.gameObject.AddComponent<DraggableWindow>(); //Add draggable windows component allowing the window to be dragged when the button is pressed down

            _text = this.transform.Find("Foreground/Text").GetComponent<Text>(); //Find the text component so we can update later.
        }

        private void Update()
        {
            //This is a unity message that runs each frame.
        }

        private void HandleButtonClick()
        {
            //Display the equiped weapon wame of the selected character
            var selection = SelectionManager.Instance.SelectedUnits;
            var color = _text.color;
            _text.color = new Color(color.r, color.g, color.b, 0f);
            if ((selection != null) && (selection.Count == 1))
                _text.text = selection[0].Body.PrimaryHand.MaybeWeapon.Name;
            else if (selection.Count == 0)
                _text.text = "No one selected";
            else if (selection.Count > 1)
                _text.text = "Select only one";
            _text.DOFade(1f, 1.5f); //fade text alpha in using tweening
        }
    }
}