﻿using System;
using System.Globalization;
using System.Threading;
using OpenTK;
using GraphicsLibrary;
using GraphicsLibrary.Hud;
using OpenTK.Graphics;

namespace Resim.Program
{
	public partial class Game
	{
		private void ConsoleInputReceived(object sender, HudConsoleInputEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			if(e.InputArray.Length > 0 && !String.IsNullOrEmpty(e.InputArray[0]))
			{
				switch(e.InputArray[0])
				{
					case "exit":
					case "stop":
					case "close":
						RenderWindow.Instance.Exit();
						break;
					case "set":
						if(e.InputArray.Length > 1 && !String.IsNullOrEmpty(e.InputArray[1]))
						{
							switch(e.InputArray[1])
							{
								case "timeMult":
									if(e.InputArray.Length > 2 && !String.IsNullOrEmpty(e.InputArray[2]))
									{
										RenderWindow.Instance.timeMultiplier = Convert.ToDouble(e.InputArray[2]);
										hudConsole.AddText("timeMult was set to " + RenderWindow.Instance.timeMultiplier);
									}
									break;
								case "walkSpeed":
									if(e.InputArray.Length > 2 && !String.IsNullOrEmpty(e.InputArray[2]))
									{
										walkSpeed = (int)Convert.ToDouble(e.InputArray[2]);
										hudConsole.AddText("walkSpeed was set to " + walkSpeed);
									}
									break;
								case "VSync":
									if(e.InputArray.Length > 2 && !String.IsNullOrEmpty(e.InputArray[2]))
									{
										try
										{
											RenderWindow.Instance.VSync = (VSyncMode)Enum.Parse(typeof(VSyncMode), e.InputArray[2], true);
										}
										catch(Exception exception)
										{
										}
										hudConsole.AddText("VSync was set to " + RenderWindow.Instance.VSync);
									}
									break;
								default:
									hudConsole.AddText("Usage: set [timeMult|walkSpeed|VSync] [value]", Color4.LightBlue);
									break;
							}
						}
						else
						{
							hudConsole.AddText("Usage: set [timeMult|walkSpeed|VSync] [value]", Color4.LightBlue);
						}
						break;
					case "reset":
						if(e.InputArray.Length > 1 && !String.IsNullOrEmpty(e.InputArray[1]))
						{
							switch(e.InputArray[1])
							{
								case "timeMult":
									RenderWindow.Instance.timeMultiplier = 1;
									hudConsole.AddText("timeMult was reset to " + RenderWindow.Instance.timeMultiplier);
									break;
								case "walkSpeed":
									walkSpeed = 400;
									hudConsole.AddText("walkSpeed was reset to " + walkSpeed);
									break;
								case "VSync":
									RenderWindow.Instance.VSync = VSyncMode.On;
									hudConsole.AddText("walkSpeed was reset to " + RenderWindow.Instance.VSync);
									break;
								default:
									hudConsole.AddText("Usage: reset [timeMult|walkSpeed|VSync]", Color4.LightBlue);
									break;
							}
						}
						else
						{
							hudConsole.AddText("Usage: reset [timeMult|walkSpeed|VSync]", Color4.LightBlue);
						}
						break;
					case "get":
						if(e.InputArray.Length > 1 && !String.IsNullOrEmpty(e.InputArray[1]))
						{
							switch(e.InputArray[1])
							{
								case "timeMult":
									hudConsole.AddText("timeMult = " + RenderWindow.Instance.timeMultiplier);
									break;
								case "walkSpeed":
									hudConsole.AddText("walkSpeed = " + walkSpeed);
									break;
								case "VSync":
									hudConsole.AddText("VSync = " + RenderWindow.Instance.VSync);
									break;
								default:
									hudConsole.AddText("Usage: get [timeMult|walkSpeed|VSync]", Color4.LightBlue);
									break;
							}
						}
						else
						{
							hudConsole.AddText("Usage: get [timeMult|walkSpeed|VSync]", Color4.LightBlue);
						}
						break;
					case "clear":
						hudConsole.ClearScreen();
						break;
					case "reload":
						config.Reload();
						break;
					case "list":

						break;
					default:
						hudConsole.AddText("Invalid command. Type 'list' for a list of commands", Color4.Red);
						break;

				}
			}
		}

		private void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == '`' || e.KeyChar == '~' || e.KeyChar == '	' || e.KeyChar == '/') //Tab and '/' to support non-European keyboards
			{
				if(hudConsole.enabled)
				{
					hudConsole.enabled = false;
					hudConsole.isVisible = false;
				}
				else
				{
					hudConsole.enabled = true;
					hudConsole.isVisible = true;
					if(hudConsole.input.Length > 0)
					{
						hudConsole.input = hudConsole.input.Remove(hudConsole.input.Length - 1);
					}
				}
			}
		}
	}
}