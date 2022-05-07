using BepInEx;
using System;
using UnityEngine;
using Utilla;
using System.Reflection;
using System.IO;
using UnityEngine.XR;

namespace KabobOrKebob
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;
		public static readonly string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		GameObject kabobl;
		GameObject handl;

		void OnEnable()
		{
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/
			HarmonyPatches.ApplyHarmonyPatches();
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
			HarmonyPatches.RemoveHarmonyPatches();
			Utilla.Events.GameInitialized -= OnGameInitialized;
		}
		

		void OnGameInitialized(object sender, EventArgs e)
		{
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
			Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("KabobOrKebob.Assets.kabob knife");
			AssetBundle bundle = AssetBundle.LoadFromStream(str);
			GameObject knife = bundle.LoadAsset<GameObject>("KABOB KNIFE");
			Instantiate(knife);

			knife.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			// left hand kabob
			kabobl = GameObject.Find("KABOB KNIFE(Clone)");
			handl = GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/");
			kabobl.transform.parent = handl.transform;
			kabobl.transform.localPosition = new Vector3(-0.1f, 0f, 0.08f);
			kabobl.transform.rotation = Quaternion.Euler(0f, 80f, 0f);
			kabobl.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
		}
		void Update()
		{
			// future stuff bool hidecosmetic;
			// future stuff InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.secondaryButton, out hidecosmetic);

		}

		/* This attribute tells Utilla to call this method when a modded room is joined */
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			/* Activate your mod here */
			/* This code will run regardless of if the mod is enabled*/

			inRoom = true;
		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			/* Deactivate your mod here */
			/* This code will run regardless of if the mod is enabled*/

			inRoom = false;
		}
	}
}
