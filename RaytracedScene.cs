using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RaytracingInOneWeekend {
	class RaytracedScene {
		private Random random = new();

		private double aspectRatio;
		private const double ViewportHeight = 2.0;
		private double ViewportWidth => ViewportHeight * aspectRatio;
		private const double FocalLength = 1.0;
		public const int SamplesPerPixel = 16;

		private HittableList world = new();
		private Camera camera;

		public RaytracedScene() {
			world.Objects.Add(new Sphere(new Vec3(0.0, 0.0, -1.0), 0.5));
			world.Objects.Add(new Sphere(new Vec3(0.0, -100.5, -1.0), 100.0));
		}

		public Bitmap RenderScene(int width, int height) {
			Bitmap b = new(width, height);
			camera = new(width, height);
			aspectRatio = width * 1.0 / height;

			// No idea why Mac's okay with this for now.
			GetAllPixels(width, height).AsParallel().ForAll(t => DrawPixel(b, t.x, t.y, width, height));

			return b;
		}

		private IEnumerable<(int x, int y)> GetAllPixels(int width, int height) {
			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					yield return (x, y);
				}
			}
		}

		private Color RayToColor(Ray3 r) {
			if (world.Hit(r, 0.0, double.PositiveInfinity, out HitRecord hitRecord)) {
				return ColorUtil.FromDoubles(0.5 * (hitRecord.Normal.X + 1.0), 0.5 * (hitRecord.Normal.Y + 1.0), 0.5 * (hitRecord.Normal.Z + 1.0));
			}
			Vec3 normalisedDirection = r.Direction.Normalized;
			double t = 0.5 * (normalisedDirection.Y + 1.0);
			return ColorUtil.FromDoubles((1.0 - t) * 1.0 + t * 0.5, (1.0 - t) * 1.0 + t * 0.7, 255);
		}

		private void DrawPixel(Bitmap b, int x, int y, int width, int height) {
			int red = 0;
			int green = 0;
			int blue = 0;
			for (int i = 0; i < SamplesPerPixel; ++i) {
				double u;
				double v;
				lock (random) {
					u = (x + random.NextDouble()) / (width - 1);
					v = (height - y - 1 + random.NextDouble()) / (height - 1);
				}
				Ray3 r = camera.GetRay(u, v);

				Color color = RayToColor(r);
				red += color.R;
				green += color.G;
				blue += color.B;
			}
			Color consolidatedColor = Color.FromArgb(red / SamplesPerPixel, green / SamplesPerPixel, blue / SamplesPerPixel);

			// This lock isn't necessary on Mac for some reason.
			lock (this) {
				b.SetPixel(x, y, consolidatedColor);
			}

			IncrementAndDebugProgress(width, height);
		}

		private int completedPixels = 0;

		private void IncrementAndDebugProgress(int width, int height) {
			lock (this) {
				++completedPixels;
				if (completedPixels % (width * height / 1000) == 0) {
					int permilles = completedPixels / (width * height / 1000);
					Console.WriteLine($"Progress: {(permilles / 10):D3}.{permilles % 10}%");
				}
			}
		}
	}
}
