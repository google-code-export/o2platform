// <copyright file="TipDialog.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.TipDialog</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 02:44:38 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2008 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Documents;
	using System.Windows.Input;
	
	//// ========================================================================================

	/// <summary>
	/// Boîte de dialogue affichant les astuces du jour.
	/// </summary>
	public partial class TipDialog: Window
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="CurrentIndex" />.
		/// </summary>
		public static readonly DependencyProperty CurrentIndexProperty=DependencyProperty.Register
		(
			"CurrentIndex",
			typeof(int),
			typeof(TipDialog),
			new FrameworkPropertyMetadata(-1, OnCurrentIndexChanged, OnCoerceValue)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="ShowOnStartup" />.
		/// </summary>
		public static readonly DependencyProperty ShowOnStartupProperty=DependencyProperty.Register("ShowOnStartup", typeof(bool), typeof(TipDialog));

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="TipDialog" />.
		/// </summary>
		public TipDialog(): this(null) {}

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="TipDialog" />.
		/// </summary>
		/// <param name="tips">Liste des astuces affichées par la boîte de dialogue.</param>
		public TipDialog(IEnumerable<string> tips)
		{
			this.Tips=new List<string>();
			if(tips!=null) foreach(var item in tips) this.Tips.Add(item);

			// Initialisation de l'interface graphique
			this.InitializeComponent();
			this.InstructionTextBlock.FontSize=this.FontSize*1.5;

			// Affichage de la première astuce
			this.MoveNext();
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit l'index spécifiant l'astuce actuellement affichée.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Index de base zéro de l'astuce actuellement affichée. La valeur moins un (-1) est retournée si aucune astuce n'est affichée.</value>
		public int CurrentIndex
		{
			get { return (int) this.GetValue(CurrentIndexProperty); }
			set { this.SetValue(CurrentIndexProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit une valeur indiquant si la case &quot;Afficher au démarrage&quot; est cochée.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value><see langword="true" /> si la case &quot;Afficher au démarrage&quot; est cochée ; sinon <see langword="false" />. La valeur par défaut est <see langword="false" />.</value>
		public bool ShowOnStartup
		{
			get { return (bool) this.GetValue(ShowOnStartupProperty); }
			set { this.SetValue(ShowOnStartupProperty, value); }
		}

		/// <summary>
		/// Obtient la liste des astuces affichées par la boîte de dialogue.
		/// </summary>
		/// <value>Liste des astuces affichées par la boîte de dialogue.</value>
		public IList<string> Tips
		{
			get;
			private set;
		}

		//// =====================================================================================

		/// <summary>
		/// Affiche l'astuce suivante.
		/// </summary>
		public void MoveNext()
		{
			if(this.Tips.Count>0)
			{
				if(this.CurrentIndex>=this.Tips.Count-1) this.CurrentIndex=0;
				else this.CurrentIndex=this.CurrentIndex+1;
			}
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

		/// <summary>
		/// Vérifie si on peut afficher l'astuce suivante.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnNextTipCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=this.Tips.Count>1;
			e.Handled=true;
		}

		/// <summary>
		/// Affiche l'astuce suivante.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnNextTipExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.MoveNext();
			e.Handled=true;
		}

		//// =====================================================================================

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="CurrentIndex" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceValue(DependencyObject sender, object baseValue)
		{
			var window=sender as TipDialog;
			if(window!=null && window.Tips.Count>0)
			{
				var value=(int) baseValue;
				if(value<0) return 0;
				if(value>=window.Tips.Count) return window.Tips.Count-1;
				return value;
			}

			return CurrentIndexProperty.DefaultMetadata.DefaultValue;
		}

		/// <summary>
		/// Met à jour l'affichage lorsque la valeur de la propriété <see cref="CurrentIndex" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnCurrentIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var window=sender as TipDialog;
			if(window!=null) window.TipTextBox.Text=window.Tips[(int) e.NewValue];
		}
	}
}