using System;
using System.Drawing;

namespace RaytracingInOneWeekend {
	static class ColorUtil {
		public static Color FromDoubles(double r, double g, double b) => Color.FromArgb(ToColor256(r), ToColor256(g), ToColor256(b));
		private static int ToColor256(double d) => Math.Clamp((int)Math.Floor(d * 256.0), 0, 255);
	}
}