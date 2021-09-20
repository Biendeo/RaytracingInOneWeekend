using System;

namespace RaytracingInOneWeekend {
	interface IHittable {
		bool Hit(Ray3 ray3, double tMin, double tMax, out HitRecord record);
	}
}