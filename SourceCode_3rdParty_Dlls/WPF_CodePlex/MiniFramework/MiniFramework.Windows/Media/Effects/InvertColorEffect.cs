// <copyright file="InvertColorEffect.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Media.Effects.InvertColorEffect</c>.
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
	/// Effet qui inverse les couleurs de la texture cible.
	/// </summary>
	public class InvertColorEffect: ShaderEffect
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Input" />.
		/// </summary>
		public static readonly DependencyProperty InputProperty=ShaderEffect.RegisterPixelShaderSamplerProperty
		(
			"Input",
			typeof(InvertColorEffect),
			0
		);

		/// <summary>
		/// Nuanceur de pixels utilisé par cet effet.
		/// </summary>
		private static PixelShader pixelShader=new PixelShader { UriSource=new Uri("Resources/Effects/InvertColor.ps", UriKind.Relative).MakePack() };

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="InvertColorEffect" />.
		/// </summary>
		public InvertColorEffect()
		{
			this.PixelShader=pixelShader;
			this.UpdateShaderValue(InputProperty);
		}

		//// =====================================================================================

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