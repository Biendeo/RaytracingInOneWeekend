using System;

namespace RaytracingInOneWeekend {
	struct Ray3 {
		public Vec3 Origin { get; set; }
		public Vec3 Direction { get; set; }

		public Ray3(Vec3 origin, Vec3 direction) {
			Origin = origin;
			Direction = direction;
		}

		public Vec3 At(double t) {
			return Origin + Direction * t;
		}

		public override string ToString() => $"R({Origin}, {Direction})";
	}
}