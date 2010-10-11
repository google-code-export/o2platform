// <copyright file="ContrastAdjustEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.ContrastAdjustEffect</c>.
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
	/// Effet qui ajuste le contraste et la luminosité de la texture cible.
	/// </summary>
	public class ContrastAdjustEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Brightness" />.
		/// </summary>
		public static readonly DependencyProperty BrightnessProperty=DependencyProperty.Register
		(
			"Brightness",
			typeof(double),
			typeof(ContrastAdjustEffect),
			new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Contrast" />.
		/// </summary>
		public static readonly DependencyProperty ContrastProperty=DependencyProperty.Register
		(
			"Contrast",
			typeof(double),
			typeof(ContrastAdjustEffect),
			new UIPropertyMetadata(1.0, PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(ContrastAdjustEffect),
			0
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/ContrastAdjust.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="ContrastAdjustEffect" />.
		/// </summary>
		public ContrastAdjustEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(BrightnessProperty);
			this.UpdateShaderValue(ContrastProperty);
			this.UpdateShaderValue(InputProperty);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit la luminosité de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Luminosité de l'effet. La valeur par défaut est 0 (zéro).</value>
		public double Brightness
		{
			get { return (double) this.GetValue(BrightnessProperty); }
			set { this.SetValue(BrightnessProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le constraste de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Constraste de l'effet. La valeur par défaut est 1 (un).</value>
		public double Contrast
		{
			get { return (double) this.GetValue(ContrastProperty); }
			set { this.SetValue(ContrastProperty, value); }
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