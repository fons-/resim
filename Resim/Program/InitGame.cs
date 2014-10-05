﻿using System;
using GraphicsLibrary;
using GraphicsLibrary.Core;
using GraphicsLibrary.Hud;
using GraphicsLibrary.Input;
using OpenTK;
using OpenTK.Graphics;
using GraphicsLibrary.Collision;
using System.Collections.Generic;

namespace Resim.Program
{
	public partial class Game
	{
		private Node player0 = new Node("player0");
		private Node map = new Node("map");
		private Node players = new Node("players");
		private Random random = new Random();

		private List<CollisionAABB> mapCollision = new List<CollisionAABB>();
		/* Deze functie wordt opgeroepen als het spel start, na LoadResources()
		 */
		public override void InitGame()
		{
			#region Entities
			skybox.mesh.material.textureName = "skybox";
			skybox.isLit = false;
			float size = Camera.Instance.ZFar / 1.8f;//Sqrt(3)
			skybox.scale = new Vector3(size, size, size);

			ground.scale = new Vector3(2, 2, 2);
			ground.mesh.material.textureName = "map0e";

			//map.scale = new Vector3(2, 2, 2);
			map0a.mesh.material.textureName = "map0a";
			map0b.mesh.material.textureName = "map0b";
			map0c.mesh.material.textureName = "map0c";
			map0d.mesh.material.textureName = "map0d";
			map0e.mesh.material.textureName = "map0e";

			hudDebug.enabled = false;
			hudDebug.isVisible = false;
			hudDebug.FontTextureName = "font0";
			hudDebug.NumberOfLines = 30;
			hudDebug.DebugInput += DebugInputReceived;

			crossHair.width = crossHair.height = 32;

			grainImage.width = 4372;
			grainImage.height = 2906;
			grainImage.color.A = .2f;

			Camera.Instance.friction = new Vector3((float)config.GetDouble("playerFriction"), 1, (float)config.GetDouble("playerFriction"));

			beam.isLit = false;
			beam.scale = new Vector3(10, 10, 10000);
			beam.renderPass = 1;
			beam.writeToDepthBuffer = false;
			beam.position = new Vector3(0, 5.5f, 50);
			beam.mesh.material = new Material("beam0", new Color4(1f, 1f, 0f, 1f), true);
			beam.mesh.material.AddTransitionColor(new Color4(1f, 1f, 0f, 0f), 1f);
			beam.materialAge = 1;
			beam.materialLifetime = 0.1f;

			flashA.isLit = false;
			flashA.renderPass = 0;
			flashA.writeToDepthBuffer = false;
			flashA.position = new Vector3(0, 5.5f, 50);
			flashA.mesh.material = new Material("flashA", new Color4(1f, 1f, 1f, 1f), true);
			flashA.mesh.material.AddTransitionColor(new Color4(1f, 1f, 1f, 0f), 1f);
			flashA.materialAge = 1;
			flashA.materialLifetime = .1f;

			flashB.isLit = false;
			flashB.renderPass = 0;
			flashB.writeToDepthBuffer = false;
			flashB.position = new Vector3(0, 5.5f, 50);
			flashB.mesh.material = new Material("flashB", new Color4(1f, 1f, 1f, 1f), true);
			flashB.mesh.material.AddTransitionColor(new Color4(1f, 1f, 1f, 0f), 1f);
			flashB.materialAge = flashA.materialAge;
			flashB.materialLifetime = flashA.materialLifetime;

			gunBase.mesh.material.textureName = "m16";
			gunBase.Yaw(3.14159f);
			gunBase.position = new Vector3(0, -10.1f, 10);

			gunBolt.mesh.material.textureName = "m16";

			gunMag.mesh.material.textureName = "m16";

			monster.mesh.material.textureName = "monsterTexture";
			monster.scale = new Vector3(1, 1, 1);

			playerMesh.material.textureName = "playerTexture";

			for(int i = 0; i < comboTextFields.Length; i++)
			{
				comboTextFields[i] = new TextField("comboField" + i);
				comboTextFields[i].textMaterial = new Material("font0", new Color4(1f, 1f, 1f, 1f));
				comboTextFields[i].text = " ";
				comboTextFields[i].size = 32;
				comboTextFields[i].age = 1;
				comboTextFields[i].lifeTime = 1;
				comboTextFields[i].textMaterial.AddTransitionColor(new Color4(1f, 1f, 1f, 0f), 1);
				comboTextFields[i].textMaterial.enableColorTransitions = true;
				HudBase.Instance.Add(comboTextFields[i]);
			}

			HudBase.Instance.Add(grainImage);
			HudBase.Instance.Add(hudDebug);
			HudBase.Instance.Add(crossHair);

			RootNode.Instance.Add(players);
			RootNode.Instance.Add(monster);
			RootNode.Instance.Add(skybox);
			//RootNode.Instance.Add(ground);
			RootNode.Instance.Add(map);
			map.Add(map0a);
			map.Add(map0b);
			map.Add(map0c);
			map.Add(map0d);
			map.Add(map0e);
			Camera.Instance.Add(player0);
			Camera.Instance.Add(gunBase);
			gunBase.Add(beam);
			gunBase.Add(flashB);
			gunBase.Add(flashA);
			gunBase.Add(gunMag);
			gunBase.Add(gunBolt);
			#endregion

			InitializeWeapons();

			Camera.Instance.position = new Vector3(-10, 1000, 10);

			RenderWindow.Instance.KeyPress += HandleKeyPress;
			RenderWindow.Instance.Title = "CSC";
			InputManager.CursorLockState = CursorLockState.Centered;
			InputManager.HideCursor();

			mapCollision.Add(new CollisionAABB(new Vector3(-50, 0, -50), new Vector3(50, 110, 50)));
		}
	}
}