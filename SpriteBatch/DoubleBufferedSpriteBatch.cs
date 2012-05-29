using System;

namespace PssSpritePerformanceTests
{
	public class DoubleBufferedSpriteBatch : ISpriteBatch
	{
		int _index = 0;
		ISpriteBatch[] _batches = new ISpriteBatch[2];
		
		public DoubleBufferedSpriteBatch (ISpriteBatch a, ISpriteBatch b)
		{
			_batches[0] = a;
			_batches[1] = b;
		}
		
		
		#region ISpriteBatch implementation
		public void Begin (Sce.Pss.Core.Matrix4 matrix)
		{
			_batches[_index].Begin(matrix);
		}

		public void Draw (Sce.Pss.Core.Graphics.Texture2D texture, Sce.Pss.Core.Vector2 position, Microsoft.Xna.Framework.Color color)
		{
			_batches[_index].Draw(texture, position, color);
		}

		public void End ()
		{
			_batches[_index].End();
			
			_index = (_index + 1) % 2;
		}
		#endregion
	}
}

