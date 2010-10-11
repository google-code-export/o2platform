// <copyright file="ZoomBlurEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.ZoomBlurEffect</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-28 09:06:09 +0200 (lun. 28 sept. 2009) $</date>
// <version>$Revision: 1910 $</version>

namespace MiniFramework.Windows.Media.Effects
{
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Media.Effects;
	
	//// ========================================================================================

	/// <summary>
	/// Effet qui rend la texture cible floue selon un centre donné.
	/// </summary>
	public class ZoomBlurEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Amount" />.
		/// </summary>
		public static readonly DependencyProperty AmountProperty=DependencyProperty.Register
		(
			"Amount",
			typeof(double),
			typeof(ZoomBlurEffect),
			new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Center" />.
		/// </summary>
		public static readonly DependencyProperty CenterProperty=DependencyProperty.Register
		(
			"Center",
			typeof(Point),
			typeof(ZoomBlurEffect),
			new UIPropertyMetadata(new Point(0.5, 0.5), PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(ZoomBlurEffect),
			0
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/ZoomBlur.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="ZoomBlurEffect" />.
		/// </summary>
		public ZoomBlurEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(AmountProperty);
			this.UpdateShaderValue(CenterProperty);
			this.UpdateShaderValue(InputProperty);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit une valeur indiquant l'intensité de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur, comprise entre 0 (zéro) et 1 (un), indiquant l'intensité de l'effet. La valeur par défaut est 0 (zéro).</value>
		public double Amount
		{
			get { return (double) this.GetValue(AmountProperty); }
			set { this.SetValue(AmountProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le point du centre de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Point du centre de l'effet.</value>
		public Point Center
		{
			get { return (Point) this.GetValue(CenterProperty); }
			set { this.SetValue(CenterProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit l'entrée de surface de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Entrée de surface de l'effet.</value>
		public Brush Input
		{
			get { return (Brush) this.GetValue(InputProperty); }
			set { this.SetValue(InputProperty, value); }
		}
	}
}