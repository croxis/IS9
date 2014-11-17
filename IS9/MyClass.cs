//
//  Author:
//    croxis 
//
//  Copyright (c) 2014, croxis
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace IS9
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class IS9Mod : MonoBehaviour
	{
		private ApplicationLauncherButton launcherButton = null;
		private Texture2D buttonTexture = new Texture2D(38, 38, TextureFormat.ARGB32, false);

		private bool debugging = true;
		private bool hideAllWindows = true;

		public void debug(string line)
		{
			if (debugging)
				print ("[IS9] " + line);
		}

		public void launcherButtonRemove()
		{
			if (launcherButton != null)
			{
				ApplicationLauncher.Instance.RemoveModApplication(launcherButton);
				debug("launcherButtonRemove");
			}
			else debug("launcherButtonRemove (useless attempt)");
		}

		public void UIToggle()
		{
			hideAllWindows = !hideAllWindows;
			print ("IS9 toggerling windows");
		}

		internal void OnDestroy()
		{
			debug("OnDestroy() START");

			// Un-register the callbacks
			GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
			GameEvents.onGameSceneLoadRequested.Remove(OnSceneChangeRequest);
			// Remove the button from the KSP AppLauncher
			launcherButtonRemove();
			debug("OnDestroy() END");
		}

		private void OnGUIApplicationLauncherReady()
		{
			print ("Adding button for IS9");
			ApplicationLauncher.Instance.AddModApplication (UIToggle, UIToggle, null, null, null, null, ApplicationLauncher.AppScenes.FLIGHT, buttonTexture);
		}

		public void OnSceneChangeRequest(GameScenes _scene)
		{
			launcherButtonRemove();
		}

	}
}

