// <copyright file="InputDialog.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.InputDialog</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-01 18:48:42 +0200 (jeu. 01 oct. 2009) $</date>
// <version>$Revision: 1956 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	//// ========================================================================================

	/// <summary>
	/// Invite l'utilisateur à saisir une valeur.
	/// </summary>
	public partial class InputDialog: Window
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="InstructionText" />.
		/// </summary>
		public static readonly DependencyProperty InstructionTextProperty=DependencyProperty.Register("InstructionText", typeof(string), typeof(InputDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Text" />.
		/// </summary>
		public static readonly DependencyProperty TextProperty=DependencyProperty.Register("Text", typeof(string), typeof(InputDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Value" />.
		/// </summary>
		public static readonly DependencyProperty ValueProperty=DependencyProperty.Register("Value", typeof(object), typeof(InputDialog));

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="InputDialog" />.
		/// </summary>
		public InputDialog()
		{
			this.InitializeComponent();
			this.InstructionTextBlock.FontSize=this.FontSize*1.5;
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit le texte de l'instruction principale.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte de l'instruction principale.</value>
		public string InstructionText
		{
			get { return (string) this.GetValue(InstructionTextProperty); }
			set { this.SetValue(InstructionTextProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit le texte du message.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte du message.</value>
		public string Text
		{
			get { return (string) this.GetValue(TextProperty); }
			set { this.SetValue(TextProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit la valeur du champ de saisie.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur saisie par l'utilisateur.</value>
		public object Value
		{
			get { return this.GetValue(ValueProperty); }
			set { this.SetValue(ValueProperty, value); }
		}
			
		//// =====================================================================================
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre réel saisi par l'utilisateur.
		/// </summary>
		/// <returns>Nombre réel saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public double? GetDouble()
		{
			return this.GetDouble(double.MinValue);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre réel saisi par l'utilisateur.
		/// </summary>
		/// <param name="minValue">Valeur minimale du contrôle de saisie.</param>
		/// <returns>Nombre réel saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public double? GetDouble(double minValue)
		{
			return this.GetDouble(minValue, double.MaxValue);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre réel saisi par l'utilisateur.
		/// </summary>
		/// <param name="minValue">Valeur minimale du contrôle de saisie.</param>
		/// <param name="maxValue">Valeur maximale du contrôle de saisie.</param>
		/// <returns>Nombre réel saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public double? GetDouble(double minValue, double maxValue)
		{
			return this.GetDouble(minValue, maxValue, 2);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre réel saisi par l'utilisateur.
		/// </summary>
		/// <param name="minValue">Valeur minimale du contrôle de saisie.</param>
		/// <param name="maxValue">Valeur maximale du contrôle de saisie.</param>
		/// <param name="decimals">Nombre de décimales affichées par le contrôle de saisie.</param>
		/// <returns>Nombre réel saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public double? GetDouble(double minValue, double maxValue, int decimals)
		{
			return this.ShowDialog()==true ? this.Value as double? : null;
		}
			
		//// =====================================================================================
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre entier saisi par l'utilisateur.
		/// </summary>
		/// <returns>Nombre entier saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public int? GetInteger()
		{
			return this.GetInteger(int.MinValue);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre entier saisi par l'utilisateur.
		/// </summary>
		/// <param name="minValue">Valeur minimale du contrôle de saisie.</param>
		/// <returns>Nombre entier saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public int? GetInteger(int minValue)
		{
			return this.GetInteger(minValue, int.MaxValue);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le nombre entier saisi par l'utilisateur.
		/// </summary>
		/// <param name="minValue">Valeur minimale du contrôle de saisie.</param>
		/// <param name="maxValue">Valeur maximale du contrôle de saisie.</param>
		/// <returns>Nombre entier saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public int? GetInteger(int minValue, int maxValue)
		{
			return this.ShowDialog()==true ? this.Value as int? : null;
		}
			
		//// =====================================================================================
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne l'élément de liste déroulante sélectionné par l'utilisateur.
		/// </summary>
		/// <param name="items">Eléments affichés dans le contrôle de saisie.</param>
		/// <returns>Elément de liste déroulante sélectionné par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public ComboBoxItem GetItem(IList<ComboBoxItem> items)
		{
			return this.GetItem(items, 0);
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne l'élément de liste déroulante sélectionné par l'utilisateur.
		/// </summary>
		/// <param name="items">Eléments affichés dans le contrôle de saisie.</param>
		/// <param name="selectedIndex">Index spécifiant l'élément actuellement sélectionné.</param>
		/// <returns>Elément de liste déroulante sélectionné par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public ComboBoxItem GetItem(IList<ComboBoxItem> items, int selectedIndex)
		{
			return this.ShowDialog()==true ? this.Value as ComboBoxItem : null;
		}
			
		//// =====================================================================================
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le mot de passe saisi par l'utilisateur.
		/// </summary>
		/// <returns>Mot de passe saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public string GetPassword()
		{
			return this.ShowDialog()==true ? this.Value as string : null;
		}
		
		/// <summary>
		/// Affiche la boîte de dialogue et retourne le texte saisi par l'utilisateur.
		/// </summary>
		/// <returns>Texte saisi par l'utilisateur, ou une référence null si la boîte de dialogue a été rejetée.</returns>
		public string GetText()
		{
			return this.ShowDialog()==true ? this.Value as string : null;
		}

		//// =====================================================================================
		
		/// <summary>
		/// Rejette la boîte de dialogue.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.DialogResult=false;
			this.Close();
			e.Handled=true;
		}
		
		/// <summary>
		/// Accepte la boîte de dialogue.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnSaveExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.DialogResult=true;
			this.Close();
			e.Handled=true;
		}
	}
}