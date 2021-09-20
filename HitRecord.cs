using System;

namespace RaytracingInOneWeekend {
	struct HitRecord {
		public Vec3 Point { get; set; }
		public Vec3 Normal { get; set; }
		public double T { get; set; }
		public bool IsFrontFace { get; }

		public HitRecord(Vec3 point, Vec3 normal, double t, bool isFrontFace) {
			Point = point;
			Normal = normal;
			T = t;
			IsFrontFace = isFrontFace;
		}

		public override string ToString() => $"HR({Point}, {Normal}, {T}, {IsFrontFace})";
	}
}