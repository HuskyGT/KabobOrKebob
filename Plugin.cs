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
		public static string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		GameObject kabobr;
		GameObject kabobl;
		GameObject handr;
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
			GameObject knife = UnityEngine.Object.Instantiate<GameObject>(AssetBundle.LoadFromFile(Plugin.fileLocation + "\\Assets\\kabob knife").LoadAsset<GameObject>("KABOB KNIFE"));
			knife.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			// right hand kabob used but keeping it here so left hand doesnt break ill remove this at a later date still got some ideas for this mod
			kabobr = GameObject.Find("KABOB KNIFE(Clone)");
			handr = GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/");
			kabobr.transform.parent = handr.transform;
			kabobr.transform.localPosition = new Vector3(-0.1f, 0f, 0f);
			kabobr.transform.rotation = Quaternion.Euler(0f, 70f, 0f);
			kabobr.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			// left hand kabob
			Instantiate(kabobr);
			kabobl = GameObject.Find("KABOB KNIFE(Clone)(Clone)");
			handl = GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/");
			kabobl.transform.parent = handl.transform;
			kabobl.transform.localPosition = new Vector3(-0.1f, 0f, 0.08f);
			kabobl.transform.rotation = Quaternion.Euler(0f, 70f, 0f);
			kabobl.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			kabobr.SetActive(false);
			// 1st is upside down
			// last is perfect... maybe
			// nope
			// left hand good right removed bc it kinda meh still need the code though because of poor decisions when writing it



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
