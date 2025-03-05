// Copyright 2022 The Tilt Brush Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using UnityEngine;
namespace TiltBrush
{
    public class AppSettingsPanel : BasePanel
    {
        public ToggleButton m_ExperimentalModeToggle;

        public override void InitPanel()
        {
            base.InitPanel();
            // Force the toggle to always be on
            m_ExperimentalModeToggle.IsToggledOn = true;
            App.Config.SetIsExperimental(true);
        }

        public void HandleToggleHandedness()
        {
            SketchControlsScript.DoSwapControls();
        }

        public void HandleResetFirstUse()
        {
            App.Config.SetIsExperimental(true); // Keep it on
            PlayerPrefs.SetInt(PanelManager.kPlayerPrefAdvancedMode, 1); // Ensure advanced mode stays enabled
            PlayerPrefs.DeleteKey(App.kPlayerPrefHasPlayedBefore);
            RestartNotification();
        }

        public void HandleToggleExperimentalMode(ToggleButton btn)
        {
            // Force it to stay on
            btn.IsToggledOn = true;
            App.Config.SetIsExperimental(true);
        }

        private void RestartNotification()
        {
            OutputWindowScript.m_Instance.CreateInfoCardAtController(
                InputManager.ControllerName.Brush,
                $"Please restart Open Brush",
                fPopScalar: 0.5f, false);
        }
    }
} // namespace TiltBrush

