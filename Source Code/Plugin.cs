using BepInEx;
using System;
using UnityEngine;
using Utilla;
using System.Reflection;
using System.IO;
using System.ComponentModel;

namespace KabobOrKebob
{
	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[Description("HauntedModMenu")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[ModdedGamemode]
	public class Plugin : BaseUnityPlugin
	{
		/*Original code and model by Husky#9424*/
		/*Mod under the MIT license, if you reproduce please credit*/

		/*Assetloading*/
		public static readonly string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		static GameObject Kabob; // the kabob item
		static GameObject Hand; // the player's left hand

		void OnEnable()
		{
			Utilla.Events.GameInitialized += OnGameInitialized;
			enabled = true;
			Kabob.SetActive(enabled);
		}

		void OnDisable()
		{
			Utilla.Events.GameInitialized -= OnGameInitialized;
			Kabob.SetActive(false);
			enabled = false;
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			Hand = GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/");

			Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("KabobOrKebob.Assets.kabob knife");
			AssetBundle bundle = AssetBundle.LoadFromStream(str);
			GameObject knife = bundle.LoadAsset<GameObject>("KABOB KNIFE");

			Kabob = Instantiate(knife);
			Kabob.transform.SetParent(Hand.transform, false);
			Kabob.transform.localPosition = new Vector3(-0.018f, 0.017f, 0.011f);
			Kabob.transform.localRotation = Quaternion.Euler(7.1491f, 278.8881f, 2.063f);
			Kabob.transform.localScale = new Vector3(0.09188f, 0.05f, 0.05f);
			Kabob.SetActive(enabled);
		}
	}
}
