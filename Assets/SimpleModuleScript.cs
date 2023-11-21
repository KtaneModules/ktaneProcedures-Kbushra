using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using Rnd = UnityEngine.Random;

public class SimpleModuleScript : MonoBehaviour {

	public KMAudio audio;
	public KMBombInfo info;
	public KMBombModule module;
	public KMSelectable[] keypadButs;
	static int ModuleIdCounter = 1;
	int ModuleId;

	bool _isSolved = false;
	bool incorrect = false;

	public TextMesh X;
	public TextMesh Method;
	public TextMesh Repeats;
	public TextMesh Radius;

	public int XInt;
	private int MethodInt;
	public int RepeatsInt;
	private int RadiusInt;

	public int methodResults1Int;
	public int methodResults2Int;

	void Awake() 
	{
		ModuleId = ModuleIdCounter++;

		foreach (KMSelectable button in keypadButs)
		{
			KMSelectable pressedButton = button;
			button.OnInteract += delegate () { buttonPresses(pressedButton); return false; };
		}
	}

	void Start ()
	{
		XInt = Rnd.Range (1, 40);
		MethodInt = Rnd.Range (1, 8);
		RepeatsInt = Rnd.Range (4, 13);
		RadiusInt = Rnd.Range (2, 8);

		X.text = XInt.ToString ();
		Method.text = MethodInt.ToString ();
		Repeats.text = RepeatsInt.ToString ();
		Radius.text = RadiusInt.ToString ();

		sequenceCreator ();
	}

	void sequenceCreator()
	{
		int[] methodResults1 = new int[RepeatsInt];
		int[] methodResults2 = new int[RepeatsInt];

		for (int i = 0; i < RepeatsInt; i++) 
		{
			if (MethodInt == 1) 
			{
				XInt = (XInt * RadiusInt + info.GetPortCount ()) % 4000;
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 2) 
			{
				XInt = (XInt * (info.GetBatteryCount() / 2) + 1) % 50;
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 3) 
			{
				XInt = Mod(XInt - info.GetOffIndicators().ToList().Count, 65);
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 4) 
			{
				XInt = ((XInt + 5 / RadiusInt) + (info.GetTwoFactorCounts() * info.GetBatteryHolderCount())) % 14;
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 5) 
			{
				XInt = Mod(XInt - (150 * RadiusInt), 200);
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 6) 
			{
				XInt = (XInt * XInt) % 39;
				methodResults1 [i] = XInt;
			}
			if (MethodInt == 7) 
			{
				XInt = (XInt + (RadiusInt % 5) * 12) % 24;
				methodResults1 [i] = XInt;
			}
		}
		XInt = methodResults1 [RepeatsInt - 1];
		Debug.LogFormat("[Procedure #{0}] Result no.1 is {1} with {2} as the method and {3} as the repeats", ModuleId, XInt, MethodInt, RepeatsInt);


		MethodInt = ((MethodInt + XInt) % 7) + 1;

		for (int i = 0; i < RepeatsInt; i++) 
		{
			if (MethodInt == 1) 
			{
				XInt = ((XInt * RadiusInt) + info.GetPortCount ()) % 4000;
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 2) 
			{
				XInt = ((XInt * (info.GetBatteryCount() / 2)) + 1) % 50;
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 3) 
			{
				XInt = Mod(XInt - info.GetOffIndicators().ToList().Count, 65);
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 4) 
			{
				XInt = (((XInt + 5) / RadiusInt) + (info.GetTwoFactorCounts() * info.GetBatteryHolderCount())) % 14;
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 5) 
			{
				XInt = Mod(XInt - (150 * RadiusInt), 200);
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 6) 
			{
				XInt = (XInt * XInt) % 39;
				methodResults2 [i] = XInt;
			}
			if (MethodInt == 7) 
			{
				XInt = (XInt + ((RadiusInt % 5) * 12)) % 24;
				methodResults2 [i] = XInt;
			}
		}
		XInt = methodResults2 [RepeatsInt - 1];
		Debug.LogFormat("[Procedure #{0}] Result no.2 is {1} with {2} as the method and {3} as the repeats", ModuleId, XInt, MethodInt, RepeatsInt);

		XInt = XInt % 14;
		Debug.LogFormat("[Procedure #{0}] Result no.3 is {1}", ModuleId, XInt);
	}

	void buttonPresses(KMSelectable pressedButton)
	{
		int buttonPosition = Array.IndexOf(keypadButs, pressedButton);

		if (_isSolved == false) 
		{
			audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, keypadButs[buttonPosition].transform);
			keypadButs [buttonPosition].AddInteractionPunch ();
			switch (buttonPosition) 
			{
			case 0:
				if (XInt != 0) 
				{
					incorrect = true;
				}
				break;
			case 1:
				if (XInt != 1) 
				{
					incorrect = true;
				}
				break;
			case 2:
				if (XInt != 2) 
				{
					incorrect = true;
				}
				break;
			case 3:
				if (XInt != 3) 
				{
					incorrect = true;
				}
				break;
			case 4:
				if (XInt != 4) 
				{
					incorrect = true;
				}
				break;
			case 5:
				if (XInt != 5) 
				{
					incorrect = true;
				}
				break;
			case 6:
				if (XInt != 6) 
				{
					incorrect = true;
				}
				break;
			case 7:
				if (XInt != 7) 
				{
					incorrect = true;
				}
				break;
			case 8:
				if (XInt != 8) 
				{
					incorrect = true;
				}
				break;
			case 9:
				if (XInt != 9) 
				{
					incorrect = true;
				}
				break;
			case 10:
				if (XInt != 10) 
				{
					incorrect = true;
				}
				break;
			case 11:
				if (XInt != 11) 
				{
					incorrect = true;
				}
				break;
			case 12:
				if (XInt != 12) 
				{
					incorrect = true;
				}
				break;
			case 13:
				if (XInt != 13) 
				{
					incorrect = true;
				}
				break;
			}
			if (incorrect) 
			{
				module.HandleStrike ();
				Log ("Wrong button");
				incorrect = false;
			}
			else
			{
				module.HandlePass ();
				Log ("Solved!");
				_isSolved = true;
			}
		}

	}

	void Log(string message)
	{
		Debug.LogFormat("[Procedure #{0}] {1}", ModuleId, message);
	}
	private int Mod(int x, int m)
	{
		int r = x % m;
		return r < 0 ? r + m : r;
	}

	#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press <0-13> [Presses the specified button]";
	#pragma warning restore 414

	IEnumerator ProcessTwitchCommand(string command)
	{
		string[] parameters = command.Split(' ');
		if (Regex.IsMatch(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			if (parameters.Length > 2)
				yield return "sendtochaterror Too many parameters!";
			else if (parameters.Length == 2)
			{
				int temp = -1;
				if (!int.TryParse(parameters[1], out temp))
				{
					yield return "sendtochaterror!f The specified button '" + parameters[1] + "' is invalid!";
					yield break;
				}
				if (temp < 0 || temp > 13)
				{
					yield return "sendtochaterror The specified button '" + parameters[1] + "' is invalid!";
					yield break;
				}
				yield return null;
				keypadButs[temp].OnInteract();
			}
			else if (parameters.Length == 1)
				yield return "sendtochaterror Please specify which button to press!";
		}
	}

	IEnumerator TwitchHandleForcedSolve()
	{
		keypadButs[XInt].OnInteract();
		yield return new WaitForSeconds(.1f);
	}
}
