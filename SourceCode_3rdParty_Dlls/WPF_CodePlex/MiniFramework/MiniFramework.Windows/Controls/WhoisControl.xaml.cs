// <copyright file="WhoisControl.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Controls.WhoisControl</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-06 10:05:49 +0200 (mar. 06 oct. 2009) $</date>
// <version>$Revision: 2041 $</version>

namespace MiniFramework.Windows.Controls
{
	using System;
	using System.ComponentModel;
	using System.Globalization;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;
	using System.Windows.Input;
	using System.Windows.Media.Imaging;
	using System.Windows.Threading;

	using MiniFramework.Collections;
	using MiniFramework.Net;
	using MiniFramework.Resources;

	using Messages=MiniFramework.Windows.Properties.Resources;

	//// ========================================================================================

	/// <summary>
	/// Contrôle d'interrogation d'un service de recherche Whois.
	/// </summary>
	public partial class WhoisControl: UserControl
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="DomainName" />.
		/// </summary>
		public static readonly DependencyProperty DomainNameProperty=DependencyProperty.Register
		(
			"DomainName",
			typeof(string),
			typeof(WhoisControl),
			new FrameworkPropertyMetadata(string.Empty, OnDomainNamePropertyChanged, OnCoerceValue)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="LookupResult" />.
		/// </summary>
		public static readonly DependencyProperty LookupResultProperty=DependencyProperty.Register
		(
			"LookupResult",
			typeof(string),
			typeof(WhoisControl),
			new FrameworkPropertyMetadata(string.Empty, OnLookupResultPropertyChanged)
		);

		/// <summary>
		/// Processus utilisé pour mettre en arrière-plan l'interrogation d'un serveur.
		/// </summary>
		private BackgroundWorker worker;

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="WhoisControl" />.
		/// </summary>
		public WhoisControl()
		{
			this.worker=new BackgroundWorker();

			// Initialisation de l'interface graphique
			this.InitializeComponent();

			// Gestionnaires d'événements
			this.worker.DoWork+=(sender, e)=>e.Result=WhoisClient.GetHostInfo(e.Argument.ToString());
			this.worker.RunWorkerCompleted+=this.OnWorkCompleted;

			// Laison des données
			var defaultIcon=StockIcons.Internet.SmallBitmapImage;

			this.InputComboBox.Items.Add(CreateComboBoxItem(".com", defaultIcon));
			this.InputComboBox.Items.Add(CreateComboBoxItem(".net", defaultIcon));
			this.InputComboBox.Items.Add(CreateComboBoxItem(".org", defaultIcon));
			this.InputComboBox.Items.Add(new Separator { Tag=string.Empty });

			foreach(var item in WhoisClient.KnownServers.Keys)
			{
				var icon=CountryFlags.GetCountryFlag(item);
				this.InputComboBox.Items.Add(CreateComboBoxItem('.'+item, icon!=null ? icon.SmallBitmapImage : defaultIcon));
			}

			// On force le rafraîchissement du rendu de la liste déroulante
			Action emptyDelegate=delegate {};
			this.InputComboBox.Dispatcher.Invoke(emptyDelegate, DispatcherPriority.Render);
		}

		//// =====================================================================================

		/// <summary>
		/// Se produit lorsque la propriété <see cref="DomainName" /> est modifiée.
		/// </summary>
		public event DependencyPropertyChangedEventHandler DomainNameChanged;

		/// <summary>
		/// Se produit lorsque la propriété <see cref="LookupResult" /> est modifiée.
		/// </summary>
		public event DependencyPropertyChangedEventHandler LookupResultChanged;

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit le nom de domaine à rechercher.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Nom de domaine à rechercher.</value>
		public string DomainName
		{
			get { return (string) this.GetValue(DomainNameProperty); }
			set { this.SetValue(DomainNameProperty, value); }
		}

		/// <summary>
		/// Obtient le résultat de la dernière requête Whois.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Résultat de la dernière requête Whois.</value>
		public string LookupResult
		{
			get { return (string) this.GetValue(LookupResultProperty); }
			private set { this.SetValue(LookupResultProperty, value); }
		}

		//// =====================================================================================

		/// <summary>
		/// Exécute la recherche du nom de domaine dans le service Whois approprié.
		/// </summary>
		public void PerformLookup()
		{
			var topLevelDomain=((ComboBoxItem) this.InputComboBox.SelectedItem).Tag.ToString();
			
			// Validation du nom de domaine
			this.DomainName=Regex.Replace(this.DomainName, Regex.Escape(topLevelDomain)+'$', string.Empty);
			if(!Regex.IsMatch(this.DomainName, @"^[a-z\d]+([\.-]?[a-z\d]+)*$"))
			{
				var message=Messages.WhoisHostError.Split('|');
				TaskDialog.Show(Window.GetWindow(this), message[1], message[0], null, MessageBoxButton.OK, MessageBoxImage.Error);

				this.InputTextBox.Focus();
				return;
			}
			
			// Mise à jour de l'affichage
			this.OutputTextBox.FontWeight=FontWeights.Bold;
			this.Cursor=Cursors.Wait;
			this.LookupResult=Messages.SearchInProgress;

			// Interrogation du serveur Whois
			this.worker.RunWorkerAsync(this.DomainName+topLevelDomain);
		}

		//// =====================================================================================

		/// <summary>
		/// Déclenche l'événement <see cref="DomainNameChanged" />.
		/// </summary>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		protected virtual void OnDomainNameChanged(DependencyPropertyChangedEventArgs e)
		{
			var handler=this.DomainNameChanged;
			if(handler!=null) handler(this, e);
		}

		/// <summary>
		/// Déclenche l'événement <see cref="LookupResultChanged" />.
		/// </summary>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		protected virtual void OnLookupResultChanged(DependencyPropertyChangedEventArgs e)
		{
			var handler=this.LookupResultChanged;
			if(handler!=null) handler(this, e);
		}

		//// =====================================================================================

		/// <summary>
		/// Sélectionne un domaine de haut niveau selon la touche appuyée par l'utilisateur.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnInputComboBoxKeyUp(object sender, KeyEventArgs e)
		{
			var search='.'+e.Key.ToString().ToLowerInvariant();

			// Recherche des domaines de haut niveau commençant par le caractère saisi
			var query=
				from Control item in this.InputComboBox.Items
				where item.Tag.ToString().StartsWith(search)
				select this.InputComboBox.Items.IndexOf(item);

			var indexes=query.ToArray();
			if(indexes.Length==0) return;

			// Sélection du domaine de haut niveau trouvé
			this.InputComboBox.SelectedIndex=Math.Max(indexes[0], indexes.FirstOrDefault(x=>x>this.InputComboBox.SelectedIndex));
			e.Handled=true;
		}

		/// <summary>
		/// Vérifie si on peut exécuter la recherche du nom de domaine dans le service Whois approprié.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnFindCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=(this.DomainName.Length>0 && !this.worker.IsBusy);
			e.Handled=true;
		}

		/// <summary>
		/// Exécute la recherche du nom de domaine dans le service Whois approprié.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnFindExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.PerformLookup();
			e.Handled=true;
		}

		/// <summary>
		/// Exécute la recherche du nom de domaine dans le service Whois approprié lorsque l'utilisateur appuie sur la touche "Entrée" ou "Retour".
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnInputTextBoxKeyUp(object sender, KeyEventArgs e)
		{
			if(e.Key==Key.Enter || e.Key==Key.Return)
			{
				ApplicationCommands.Find.Execute(null, this);
				this.InputTextBox.Text=this.InputTextBox.Text.Trim();
			}

			e.Handled=true;
		}

		/// <summary>
		/// Supprime les espaces blancs du champ de saisie du nom de domaine lorsque le champ perd le focus.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnInputTextBoxLostFocus(object sender, RoutedEventArgs e)
		{
			this.InputTextBox.Text=this.InputTextBox.Text.Trim();
			e.Handled=true;
		}

		/// <summary>
		/// Affiche le résultat d'une interrogation de serveur Whois.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			CommandManager.InvalidateRequerySuggested();
			
			// Restauration du champ de résultat à son état initial
			this.OutputTextBox.FontWeight=FontWeights.Normal;
			this.Cursor=null;
			this.LookupResult=string.Empty;

			// On détermine si une erreur s'est produite
			var response=(e.Error!=null) ? string.Empty : e.Result.ToString();
			if(response.Length==0)
			{
				var message=Messages.WhoisQueryError.Split('|');
				TaskDialog.Show(Window.GetWindow(this), message[1], message[0], null, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			
			// Affichage du résultat
			this.LookupResult=response;
		}

		//// =====================================================================================

		/// <summary>
		/// Crée un élément de liste déroulante qui affiche le texte et l'icône spécifiés.
		/// </summary>
		/// <param name="text">Texte affiché par l'élément.</param>
		/// <param name="icon">Icône affichée par l'élément.</param>
		/// <returns>Elément de liste déroulante affichant le texte et l'icône spécifiés.</returns>
		private static ComboBoxItem CreateComboBoxItem(string text, BitmapImage icon)
		{
			var panel=new StackPanel { Orientation=Orientation.Horizontal };
			panel.Children.Add(new Image { Source=icon });
			panel.Children.Add(new TextBlock { Text=text });

			return new ComboBoxItem { Content=panel, Tag=text };
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="DomainName" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceValue(DependencyObject sender, object baseValue)
		{
			return baseValue!=null ? baseValue.ToString().Trim().ToLowerInvariant() : DomainNameProperty.DefaultMetadata.DefaultValue;
		}

		/// <summary>
		/// Déclenche l'événement <see cref="DomainNameChanged" /> lorsque la valeur de la propriété <see cref="DomainName" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnDomainNamePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control=sender as WhoisControl;
			if(control!=null) control.OnDomainNameChanged(e);
		}

		/// <summary>
		/// Déclenche l'événement <see cref="LookupResultChanged" /> lorsque la valeur de la propriété <see cref="LookupResult" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnLookupResultPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control=sender as WhoisControl;
			if(control!=null) control.OnLookupResultChanged(e);
		}
	}
}