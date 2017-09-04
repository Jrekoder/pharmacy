﻿using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;

namespace Pharmacy.Droid
{
	public static class ColorExtensions
	{
		/// <summary>
		/// Blend <paramref name="color" /> and <paramref name="withColor" /> using the given ratio.
		/// </summary>
		/// <returns>A blended color based on the colors and ratio passed in.</returns>
		/// <param name="color">The color to be used as a base for our blended color.</param>
		/// <param name="withColor">The color to blend with.</param>
		/// <param name="ratio">The ratio of which to blend. 0.0 will return <paramref name="color" />, 0.5 will give 
		/// an even blend, 1.0 will return <paramref name="withColor"/></param>
		public static int BlendWith (this int color, int withColor, float ratio)
		{
			float inverseRatio = 1f - ratio;

			float a = (Color.GetAlphaComponent (color) * inverseRatio) + (Color.GetAlphaComponent (withColor) * ratio);
			float r = (Color.GetRedComponent (color) * inverseRatio) + (Color.GetRedComponent (withColor) * ratio);
			float g = (Color.GetGreenComponent (color) * inverseRatio) + (Color.GetGreenComponent (withColor) * ratio);
			float b = (Color.GetBlueComponent (color) * inverseRatio) + (Color.GetBlueComponent (withColor) * ratio);

			return Color.Argb ((int) a, (int) r, (int) g, (int) b);
		}


		/// <summary>
		/// Blend <paramref name="color" /> and <paramref name="withColor" /> using the given ratio.
		/// </summary>
		/// <returns>A blended color based on the colors and ratio passed in.</returns>
		/// <param name="color">The color to be used as a base for our blended color.</param>
		/// <param name="withColor">The color to blend with.</param>
		/// <param name="ratio">The ratio of which to blend. 0.0 will return <paramref name="color" />, 0.5 will give 
		/// an even blend, 1.0 will return <paramref name="withColor" /></param>
		public static Color BlendWith (this Color color, Color withColor, float ratio)
		{
#pragma warning disable IDE0004 // Remove Unnecessary Cast
			return new Color (BlendWith ((int) color, (int) withColor, ratio));
#pragma warning restore IDE0004 // Remove Unnecessary Cast
		}


		/// <summary>
		/// Gets a <see cref="Color"/> instance from the given resource Id.
		/// </summary>
		/// <returns>The color from resource.</returns>
		/// <param name="context">Context.</param>
		/// <param name="resource">Resource.</param>
		public static Color GetColorFromResource (this Context context, int resource)
		{
			return new Color (ContextCompat.GetColor (context, resource));
		}
	}
}