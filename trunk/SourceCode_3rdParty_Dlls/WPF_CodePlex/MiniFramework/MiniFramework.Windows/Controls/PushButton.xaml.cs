// <copyright file="PushButton.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Controls.PushButton</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Windows.Controls
{
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Markup;
	using System.Windows.Media;

	//// ========================================================================================

	/// <summary>
	/// Représente un bouton de commande.
	/// </summary>
	[ContentProperty("Text")]
	public partial class PushButton: Button
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Icon" />.
		/// </summary>
		public static readonly DependencyProperty IconProperty=DependencyProperty.Register("Icon", typeof(ImageSource), typeof(PushButton));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Text" />.
		/// </summary>
		public static readonly DependencyProperty TextProperty=DependencyProperty.Register("Text", typeof(string), typeof(PushButton));

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="PushButton" />.
		/// </summary>
		public PushButton()
		{
			this.InitializeComponent();
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit l'icône affichée par le bouton.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Icône affichée par le bouton.</value>
		public ImageSource Icon
		{
			get { return (ImageSource) this.GetValue(IconProperty); }
			set { this.SetValue(IconProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le texte affiché par le bouton.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte affiché par le bouton.</value>
		public string Text
		{
			get { return (string) this.GetValue(TextProperty); }
			set { this.SetValue(TextProperty, value); }
		}
	}
}