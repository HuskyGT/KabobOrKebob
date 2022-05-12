using BepInEx;
using System;
using UnityEngine;
using Utilla;
using System.Reflection;
using System.IO;
using UnityEngine.XR;
using System.ComponentModel;

namespace KabobOrKebob
{
	[Description("HauntedModMenu")]
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		public static readonly string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		float hideshowcooldown = 0.3f;
		float showhide;
		bool ishiden = false;
		GameObject kabobl;
		GameObject handl;
		

		void OnEnable()
		{
			HarmonyPatches.ApplyHarmonyPatches();
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnDisable()
		{
			HarmonyPatches.RemoveHarmonyPatches();
			Utilla.Events.GameInitialized -= OnGameInitialized;
		}


		void OnGameInitialized(object sender, EventArgs e)
		{
			Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("KabobOrKebob.Assets.kabob knife");
			AssetBundle bundle = AssetBundle.LoadFromStream(str);
			GameObject knife = bundle.LoadAsset<GameObject>("KABOB KNIFE");
			Instantiate(knife);

			knife.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			kabobl = GameObject.Find("KABOB KNIFE(Clone)");
			handl = GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/");
			kabobl.transform.SetParent(handl.transform, true);
			kabobl.transform.localPosition = new Vector3(-0.1f, 0f, 0.08f);
			kabobl.transform.localRotation = Quaternion.Euler(0f, -60f, 0f);
			kabobl.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		}
		void Update()
		{
			bool hidecosmetic;
			InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.secondaryButton, out hidecosmetic);
			if (ishiden)
			{
				kabobl.transform.localPosition = new Vector3(-1000f, 0f, 0.08f);
			}
			if (Time.time > showhide)
			{
				if (hidecosmetic)
				{
					if (!ishiden)
					{
						ishiden = true;
					}
					else
					{
						ishiden = false;
						kabobl.transform.localPosition = new Vector3(-0.1f, 0f, 0.08f);
					}
					showhide = Time.time + hideshowcooldown;
				}
			}
		}
	}
}
