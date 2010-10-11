// <copyright file="SharpenEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.SharpenEffect</c>.
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
	/// Effet qui rend la texture cible plus nette.
	/// </summary>
	public class SharpenEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Amount" />.
		/// </summary>
		public static readonly DependencyProperty AmountProperty=DependencyProperty.Register
		(
			"Amount",
			typeof(double),
			typeof(SharpenEffect),
			new UIPropertyMetadata(15.0, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(SharpenEffect),
			0
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Width" />.
		/// </summary>
		public static readonly DependencyProperty WidthProperty=DependencyProperty.Register
		(
			"Width",
			typeof(double),
			typeof(SharpenEffect),
			new UIPropertyMetadata(0.0001, PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/Sharpen.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="SharpenEffect" />.
		/// </summary>
		public SharpenEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(AmountProperty);
			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(WidthProperty);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit une valeur indiquant l'intensité de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur indiquant l'intensité de l'effet. La valeur par défaut est 15.</value>
		public double Amount
		{
			get { return (double) this.GetValue(AmountProperty); }
			set { this.SetValue(AmountProperty, value); }
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
		/// Obtient ou définit la largeur de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Largeur de l'effet. La valeur par défaut est 0,0001.</value>
		public double Width
		{
			get { return (double) this.GetValue(WidthProperty); }
			set { this.SetValue(WidthProperty, value); }
		}
	}
}