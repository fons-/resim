﻿// Material.cs
//
// Copyright 2013 Fons van der Plas
// Fons van der Plas, fonsvdplas@gmail.com

using System;
using System.Collections.Generic;
using OpenTK.Graphics;

namespace GraphicsLibrary.Core
{
	/* Met deze class wordt het kleuren van particles, text en meshes makkelijker
	 * ook wordt de texture hiering opgeslagen
	 * 
	 * Een Material kan ook verschillende kleuren hebben die in elkaar over gaan
	 * bijvoorbeeld bij vuur, waar de kleur van geel naar rood naar transparant gaat
	 * 
	 * Er kunnen ook verschillende textures gebruikt worden, maar deze gaan (nog) niet vloeiend in elkaar over
	 */
	public class Material
	{
		public string textureName = "default";
		public Color4 baseColor = Color4.White;


		public bool enableTextureTransitions = false;
		public bool enableColorTransitions = false;
		public bool smoothTransitionColors = true;

		private List<TransitionColor> transitionColors = new List<TransitionColor>();
		private List<TransitionTexture> transitionTextures = new List<TransitionTexture>();

		public Material()
		{
			
		}

		public Material(string textureName, Color4 baseColor)
		{
			this.textureName = textureName;
			this.baseColor = baseColor;
		}

		public Material(string textureName, Color4 baseColor, bool enableTransitions)
		{
			this.textureName = textureName;
			this.baseColor = baseColor;
			enableColorTransitions = enableTransitions;
			enableTextureTransitions = enableTransitions;
		}

		public void ClearTransitionColors()
		{
			transitionColors = new List<TransitionColor>();
		}

		public void ClearTransitionTextures()
		{
			transitionTextures = new List<TransitionTexture>();
		}

		public void AddTransitionColor(Color4 color, float fraction)
		{
			transitionColors.Add(new TransitionColor(fraction, color));
		}

		public void AddTransitionTexture(string name, float fraction)
		{
			transitionTextures.Add(new TransitionTexture(fraction, name));
		}

		public string GetCurrentTexture(float fraction)
		{
			if (enableTextureTransitions)
			{
				string output = textureName;
				float highestFraction = 0f;

				foreach (TransitionTexture t in transitionTextures)
				{
					if (t.fraction > highestFraction && t.fraction <= fraction)
					{
						highestFraction = t.fraction;
						output = t.name;
					}
				}
				return output;
			}
			return textureName;
		}

		public string GetCurrentTexture()
		{
			return textureName;
		}

		public Color4 GetCurrentColor(float fraction)
		{
			if (enableColorTransitions)
			{
				TransitionColor low = new TransitionColor(0f, baseColor);
				TransitionColor high = new TransitionColor(1f, baseColor);

				foreach (TransitionColor t in transitionColors)
				{
					if (t.fraction >= low.fraction && t.fraction <= fraction)
					{
						low.fraction = t.fraction;
						low = t;
					}

					if (t.fraction <= high.fraction && t.fraction >= fraction)
					{
						high.fraction = t.fraction;
						high = t;
					}
				}
				float highDelta = (fraction - low.fraction)/(high.fraction - low.fraction);
				float lowDelta = (high.fraction - fraction)/(high.fraction - low.fraction);

				return new Color4(
					(low.color.R * lowDelta) + (high.color.R * (highDelta)), 
					(low.color.G * lowDelta) + (high.color.G * (highDelta)), 
					(low.color.B * lowDelta) + (high.color.B * (highDelta)), 
					(low.color.A * lowDelta) + (high.color.A * (highDelta)));
				
			}
			return baseColor;
		}

		public Color4 GetCurrentColor()
		{
			return baseColor;
		}

		struct TransitionColor
		{
			public float fraction;
			public Color4 color;

			public TransitionColor(float fraction, Color4 color)
			{
				this.color = color;
				this.fraction = fraction;
			}
		}

		struct TransitionTexture
		{
			public readonly float fraction;
			public readonly string name;

			public TransitionTexture(float fraction, string name)
			{
				this.name = name;
				this.fraction = fraction;
			}
		}
	}
}
