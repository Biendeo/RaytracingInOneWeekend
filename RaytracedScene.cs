using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeekend {
	class RaytracedScene {
		private double aspectRatio;
		private const double ViewportHeight = 2.0;
		private double ViewportWidth => ViewportHeight * aspectRatio;
		private const double FocalLength = 1.0;

		private HittableList world = new();

		public RaytracedScene() {
			world.Objects.Add(new Sphere(new Vec3(0.0, 0.0, -1.0), 0.5));
			world.Objects.Add(new Sphere(new Vec3(0.0, -100.5, -1.0), 100.0));
		}

		public Bitmap RenderScene(int width, int height) {
			Bitmap b = new Bitmap(width, height);
			aspectRatio = width * 1.0 / height;

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

		private Color RayToColor(Ray3 r) {
			if (world.Hit(r, 0.0, double.PositiveInfinity, out HitRecord hitRecord)) {
				return Color.FromArgb(ToColor256(0.5 * (hitRecord.Normal.X + 1.0)), ToColor256(0.5 * (hitRecord.Normal.Y + 1.0)), ToColor256(0.5 * (hitRecord.Normal.Z + 1.0)));
			}
			Vec3 normalisedDirection = r.Direction.Normalized;
			double t = 0.5 * (normalisedDirection.Y + 1.0);
			return Color.FromArgb(ToColor256((1.0 - t) * 1.0 + t * 0.5), ToColor256((1.0 - t) * 1.0 + t * 0.7), 255);
		}

		private bool DoesRayHitSphere(Vec3 sphereCentre, double sphereRadius, Ray3 ray) {
			Vec3 oc = ray.Origin - sphereCentre;
			double a = ray.Direction.SquaredMagnitude;
			double b = 2.0 * Vec3.Dot(oc, ray.Direction);
			double c = oc.SquaredMagnitude - sphereRadius * sphereRadius;
			double discriminant = b * b - 4 * a * c;
			return discriminant > 0.0;
		}

		private double HitSphere(Vec3 sphereCentre, double sphereRadius, Ray3 ray) {
			Vec3 oc = ray.Origin - sphereCentre;
			double a = ray.Direction.SquaredMagnitude;
			double halfB = Vec3.Dot(oc, ray.Direction);
			double c = oc.SquaredMagnitude - sphereRadius * sphereRadius;
			double discriminant = halfB * halfB - a * c;
			if (discriminant < 0.0) {
				return -1.0;
			} else {
				return (-halfB - Math.Sqrt(discriminant)) / a;
			}
		}

		private void DrawPixel(Bitmap  b, int x, int y, int width, int height) {
			double u = x * 1.0 / (width - 1);
			double v = (height - y - 1) * 1.0 / (height - 1);

			Vec3 origin = Vec3.Zero;
			Vec3 horizontal = new Vec3(ViewportWidth, 0.0, 0.0);
			Vec3 vertical = new Vec3(0.0, ViewportHeight, 0.0);
			Vec3 lowerLeftCorner = origin - (horizontal / 2.0) - (vertical / 2.0) - new Vec3(0.0, 0.0, FocalLength);

			Ray3 r = new Ray3(Vec3.Zero, lowerLeftCorner + u * horizontal + v * vertical - origin);
			b.SetPixel(x, y, RayToColor(r));
		}

		private int ToColor256(double d) {
			return Math.Max(0, Math.Min(255, (int)Math.Floor(d * 256.0)));
		}
	}
}
