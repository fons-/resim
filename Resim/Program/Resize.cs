﻿using System.Drawing;
using OpenTK;

namespace Resim.Program
{
	public partial class Game
	{
		public override void Resize(Rectangle newDimensions)
		{
			//crossHair.position = new Vector2(newDimensions.Width / 2 - 16, newDimensions.Height / 2 - 16);
			hudConsole.position = new Vector2(0, newDimensions.Height - hudConsole.height);
			speedometerBase.position = new Vector2((newDimensions.Width - speedometerBase.width), newDimensions.Height - speedometerBase.height + 32);
			speedometerPointer.position = new Vector2(newDimensions.Width / 2, newDimensions.Height - speedometerPointer.height + 32);
			ActionTrigger.textField.position = new Vector2((newDimensions.Width - ActionTrigger.textField.sizeX * ActionTrigger.textField.text.Length) / 2, (newDimensions.Height - ActionTrigger.textField.sizeY) * 3 / 4);
		}
	}
}