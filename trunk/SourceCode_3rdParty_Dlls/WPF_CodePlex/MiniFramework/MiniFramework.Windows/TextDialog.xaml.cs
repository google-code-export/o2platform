// <copyright file="TextDialog.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.TextDialog</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-01 18:48:42 +0200 (jeu. 01 oct. 2009) $</date>
// <version>$Revision: 1956 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Input;

	//// ========================================================================================

	/// <summary>
	/// Boîte de dialogue affichant du texte brut.
	/// </summary>
	public partial class TextDialog: Window
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Text" />.
		/// </summary>
		public static readonly DependencyProperty TextProperty=DependencyProperty.Register
		(
			"Text",
			typeof(string),
			typeof(TextDialog),
			new FrameworkPropertyMetadata(string.Empty, null, OnCoerceValue)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="WordWrap" />.
		/// </summary>
		public static readonly DependencyProperty WordWrapProperty=DependencyProperty.Register
		(
			"WordWrap",
			typeof(TextWrapping),
			typeof(TextDialog),
			new FrameworkPropertyMetadata(TextWrapping.Wrap)
		);

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="TextDialog" />.
		/// </summary>
		public TextDialog()
		{
			// Il s'agit d'une boîte de dialogue modale : le bouton de réduction doit être désactivé
			this.DisableMinimizeBox();

			// Initialisation de l'interface graphique
			this.InitializeComponent();
			this.TextBox.FontSize=Math.Max(SystemFonts.MessageFontSize, 12);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit le texte affiché par la boîte de dialogue.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte affiché par la boîte de dialogue. La valeur par défaut est une chaîne vide.</value>
		public string Text
		{
			get { return (string) this.GetValue(TextProperty); }
			set { this.SetValue(TextProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit une valeur indiquant si les mots du texte sont renvoyés au début de la ligne suivante lorsque cela est nécessaire.
		/// </summary>
		/// <value>Valeur énumérée qui indique si les mots du texte sont renvoyés au début de la ligne suivante. La valeur par défaut est <c>TextWrapping.Wrap</c>.</value>
		public TextWrapping WordWrap
		{
			get { return (TextWrapping) this.GetValue(WordWrapProperty); }
			set { this.SetValue(WordWrapProperty, value); }
		}

		//// =====================================================================================

		/// <summary>
		/// Ferme la fenêtre.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.Close();
			e.Handled=true;
		}

		//// =====================================================================================

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Text" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceValue(DependencyObject sender, object baseValue)
		{
			return baseValue ?? TextProperty.DefaultMetadata.DefaultValue;
		}
	}
}