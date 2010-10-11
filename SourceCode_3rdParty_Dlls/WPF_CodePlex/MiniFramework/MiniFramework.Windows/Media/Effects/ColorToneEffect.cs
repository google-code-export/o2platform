// <copyright file="ColorToneEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.ColorToneEffect</c>.
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
	/// Effet qui modifie la tonalité des couleurs de la texture cible.
	/// </summary>
	public class ColorToneEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Amount" />.
		/// </summary>
		public static readonly DependencyProperty AmountProperty=DependencyProperty.Register
		(
			"Amount",
			typeof(double),
			typeof(ColorToneEffect),
			new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="DarkColor" />.
		/// </summary>
		public static readonly DependencyProperty DarkColorProperty=DependencyProperty.Register
		(
			"DarkColor",
			typeof(Color),
			typeof(ColorToneEffect),
			new UIPropertyMetadata(Colors.SaddleBrown, PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Desaturation" />.
		/// </summary>
		public static readonly DependencyProperty DesaturationProperty=DependencyProperty.Register
		(
			"Desaturation",
			typeof(double),
			typeof(ColorToneEffect),
			new UIPropertyMetadata(0.5, PixelShaderConstantCallback(2))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(ColorToneEffect),
			0
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="LightColor" />.
		/// </summary>
		public static readonly DependencyProperty LightColorProperty=DependencyProperty.Register
		(
			"LightColor",
			typeof(Color),
			typeof(ColorToneEffect),
			new UIPropertyMetadata(Colors.GhostWhite, PixelShaderConstantCallback(3))
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/ColorTone.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="ColorToneEffect" />.
		/// </summary>
		public ColorToneEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(AmountProperty);
			this.UpdateShaderValue(DarkColorProperty);
			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(LightColorProperty);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit une valeur indiquant l'intensité de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur indiquant l'intensité de l'effet. La valeur par défaut est 0,5.</value>
		public double Amount
		{
			get { return (double) this.GetValue(AmountProperty); }
			set { this.SetValue(AmountProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la couleur foncée.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Couleur foncée. La valeur par défaut est <see cref="Colors.SaddleBrown" />.</value>
		public Color DarkColor
		{
			get { return (Color) this.GetValue(DarkColorProperty); }
			set { this.SetValue(DarkColorProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le niveau de désaturation.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Niveau de désaturation. La valeur par défaut est 0,5.</value>
		public double Desaturation
		{
			get { return (double) this.GetValue(DesaturationProperty); }
			set { this.SetValue(DesaturationProperty, value); }
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

		/// <summary>
		/// Obtient ou définit la couleur claire.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Couleur claire. La valeur par défaut est <see cref="Colors.GhostWhite" />.</value>
		public Color LightColor
		{
			get { return (Color) this.GetValue(LightColorProperty); }
			set { this.SetValue(LightColorProperty, value); }
		}
	}
}