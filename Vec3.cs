using System;

namespace RaytracingInOneWeekend {
	struct Vec3 {
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Vec3(double x, double y, double z) {
			X = x;
			Y = y;
			Z = z;
		}

		public static Vec3 Zero => new Vec3(0.0, 0.0, 0.0);

		public static Vec3 operator-(Vec3 vec3) => new Vec3(-vec3.X, -vec3.Y, -vec3.Z);

		public static Vec3 operator+(Vec3 a, Vec3 b) => new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

		public static Vec3 operator-(Vec3 a, Vec3 b) => new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

		public static Vec3 operator*(double m, Vec3 a) => new Vec3(a.X * m, a.Y * m, a.Z * m);
		public static Vec3 operator*(Vec3 a, double m) => new Vec3(a.X * m, a.Y * m, a.Z * m);

		public static Vec3 operator/(Vec3 a, double m) => new Vec3(a.X / m, a.Y / m, a.Z / m);

		public static Vec3 operator*(Vec3 a, Vec3 b) => new Vec3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

		public static double Dot(Vec3 a, Vec3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

		public Vec3 Normalized => this / Magnitude;

		public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z);

		public double SquaredMagnitude => X * X + Y * Y + Z * Z;

		public override string ToString() => $"V({X}, {Y}, {Z})";
	}
}