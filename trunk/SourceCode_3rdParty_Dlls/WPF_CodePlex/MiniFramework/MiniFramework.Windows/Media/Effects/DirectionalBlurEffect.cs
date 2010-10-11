// <copyright file="DirectionalBlurEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.DirectionalBlurEffect</c>.
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
	/// Effet qui rend la texture cible floue selon une direction donnée.
	/// </summary>
	public class DirectionalBlurEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Amount" />.
		/// </summary>
		public static readonly DependencyProperty AmountProperty=DependencyProperty.Register
		(
			"Amount",
			typeof(double),
			typeof(DirectionalBlurEffect),
			new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Direction" />.
		/// </summary>
		public static readonly DependencyProperty DirectionProperty=DependencyProperty.Register
		(
			"Direction",
			typeof(double),
			typeof(DirectionalBlurEffect),
			new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(DirectionalBlurEffect),
			0
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/DirectionalBlur.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="DirectionalBlurEffect" />.
		/// </summary>
		public DirectionalBlurEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(AmountProperty);
			this.UpdateShaderValue(DirectionProperty);
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
		/// Obtient ou définit la direction de l'effet.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Direction de l'effet en degrés. La valeur par défaut est 0 (zéro).</value>
		public double Direction
		{
			get { return (double) this.GetValue(DirectionProperty); }
			set { this.SetValue(DirectionProperty, ((value%360)+360)%360); }
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