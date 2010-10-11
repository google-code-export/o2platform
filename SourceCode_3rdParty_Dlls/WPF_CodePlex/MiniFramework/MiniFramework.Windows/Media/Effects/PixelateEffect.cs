// <copyright file="PixelateEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.PixelateEffect</c>.
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
	/// Effet qui pixellise la texture cible.
	/// </summary>
	public class PixelateEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="BlockHeight" />.
		/// </summary>
		public static readonly DependencyProperty BlockHeightProperty=DependencyProperty.Register
		(
			"BlockHeight",
			typeof(double),
			typeof(PixelateEffect),
			new UIPropertyMetadata(80.0, PixelShaderConstantCallback(0))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="BlockWidth" />.
		/// </summary>
		public static readonly DependencyProperty BlockWidthProperty=DependencyProperty.Register
		(
			"BlockWidth",
			typeof(double),
			typeof(PixelateEffect),
			new UIPropertyMetadata(80.0, PixelShaderConstantCallback(1))
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(PixelateEffect),
			0
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/Pixelate.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="PixelateEffect" />.
		/// </summary>
		public PixelateEffect()
		{
			this.PixelShader=pixelShader;

			this.UpdateShaderValue(BlockHeightProperty);
			this.UpdateShaderValue(BlockWidthProperty);
			this.UpdateShaderValue(InputProperty);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit la hauteur d'un bloc.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Hauteur d'un bloc. La valeur par défaut est 80.</value>
		public double BlockHeight
		{
			get { return (double) this.GetValue(BlockHeightProperty); }
			set { this.SetValue(BlockHeightProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la largeur d'un bloc.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Largeur d'un bloc. La valeur par défaut est 80.</value>
		public double BlockWidth
		{
			get { return (double) this.GetValue(BlockWidthProperty); }
			set { this.SetValue(BlockWidthProperty, value); }
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