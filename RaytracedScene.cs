using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeekend {
	class RaytracedScene {
		public Bitmap RenderScene(int width, int height) {
			Bitmap b = new Bitmap(width, height);

			// TODO: Figure out how to reliably parallelise setting pixels on all platforms.
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
				// No idea why Mac's okay with this for now.
				GetAllPixels(width, height).AsParallel().ForAll(t => DrawPixel(b, t.x, t.y, width, height));
			} else {
				foreach (var t in GetAllPixels(width, height)) {
					DrawPixel(b, t.x, t.y, width, height);
				}
			}

			return b;
		}

		private IEnumerable<(int x, int y)> GetAllPixels(int width, int height) {
			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					yield return (x, y);
				}
			}
		}

		private void DrawPixel(Bitmap  b, int x, int y, int width, int height) {
			b.SetPixel(x, y, Color.FromArgb((y * y * 256 / height) % 256, (x * y * 256 / width) % 256, (x + y) % 256));
		}
	}
}
