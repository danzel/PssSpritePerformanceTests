using System;
using Sce.Pss.Core;
using Sce.Pss.Core.Graphics;
using Microsoft.Xna.Framework;

namespace PssSpritePerformanceTests
{
	public interface ISpriteBatch
	{
		void Begin(Matrix4 matrix);
		
		void Draw(Texture2D texture, Vector2 position, Color color);
		
		void End();
	}
}

