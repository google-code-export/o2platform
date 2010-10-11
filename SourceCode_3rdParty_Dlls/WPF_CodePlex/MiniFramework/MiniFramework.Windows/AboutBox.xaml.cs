// <copyright file="AboutBox.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.AboutBox</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 23:55:02 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1990 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Navigation;

	using MiniFramework.Drawing;
	using MiniFramework.Reflection;
	using MiniFramework.Text;
	using MiniFramework.Windows;

	using Messages=MiniFramework.Windows.Properties.Resources;
	
	//// ========================================================================================

	/// <summary>
	/// Boîte de dialogue affichant des informations relatives à un programme.
	/// </summary>
	public partial class AboutBox: Window
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Authors" />.
		/// </summary>
		public static readonly DependencyProperty AuthorsProperty=DependencyProperty.Register("Authors", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Copyright" />.
		/// </summary>
		public static readonly DependencyProperty CopyrightProperty=DependencyProperty.Register("Copyright", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Credits" />.
		/// </summary>
		public static readonly DependencyProperty CreditsProperty=DependencyProperty.Register("Credits", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Description" />.
		/// </summary>
		public static readonly DependencyProperty DescriptionProperty=DependencyProperty.Register("Description", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="License" />.
		/// </summary>
		public static readonly DependencyProperty LicenseProperty=DependencyProperty.Register("License", typeof(License), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Logo" />.
		/// </summary>
		public static readonly DependencyProperty LogoProperty=DependencyProperty.Register("Logo", typeof(ImageSource), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Product" />.
		/// </summary>
		public static readonly DependencyProperty ProductProperty=DependencyProperty.Register
		(
			"Product",
			typeof(string),
			typeof(AboutBox),
			new FrameworkPropertyMetadata(OnProductChanged)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Translators" />.
		/// </summary>
		public static readonly DependencyProperty TranslatorsProperty=DependencyProperty.Register("Translators", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Version" />.
		/// </summary>
		public static readonly DependencyProperty VersionProperty=DependencyProperty.Register("Version", typeof(string), typeof(AboutBox));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="WebSite" />.
		/// </summary>
		public static readonly DependencyProperty WebSiteProperty=DependencyProperty.Register("WebSite", typeof(Uri), typeof(AboutBox));

		//// =====================================================================================
		
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="AboutBox" />.
		/// </summary>
		public AboutBox()
		{
			var info=new AssemblyInfo(Application.Current.GetType().Assembly);
			var version=info.Version;

			// Initialisation de l'interface graphique
			this.InitializeComponent();
			this.ProductTextBlock.FontSize=this.FontSize*1.5;

			// Personnalisation de l'affichage
			this.Copyright=info.Copyright;
			this.Description=info.Description;
			this.Logo=System.Drawing.Icon.ExtractAssociatedIcon(Application.Current.GetExecutablePath()).ToBitmapImage(48);
			this.Product=info.Product;
			this.Version=version.ToString(version.Build>0 ? 3 : 2);
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit l'image du logo du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Image du logo du produit.</value>
		public ImageSource Logo
		{
			get { return (ImageSource) this.GetValue(LogoProperty); }
			set { this.SetValue(LogoProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit le nom du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Nom du produit.</value>
		public string Product
		{
			get { return (string) this.GetValue(ProductProperty); }
			set { this.SetValue(ProductProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le numéro de version du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Numéro de version du produit.</value>
		public string Version
		{
			get { return (string) this.GetValue(VersionProperty); }
			set { this.SetValue(VersionProperty, value); }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit les informations de droits d'auteur du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Informations de droits d'auteur.</value>
		public string Copyright
		{
			get { return (string) this.GetValue(CopyrightProperty); }
			set { this.SetValue(CopyrightProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la description du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Description du produit.</value>
		public string Description
		{
			get { return (string) this.GetValue(DescriptionProperty); }
			set { this.SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit l'accord de licence du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Accord de licence.</value>
		public License License
		{
			get { return (License) this.GetValue(LicenseProperty); }
			set { this.SetValue(LicenseProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit l'URL du site Web du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>URL du site Web.</value>
		public Uri WebSite
		{
			get { return (Uri) this.GetValue(WebSiteProperty); }
			set { this.SetValue(WebSiteProperty, value); }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit le texte indiquant les personnes qui ont écrit le produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Personnes qui ont écrit le produit.</value>
		public string Authors
		{
			get { return (string) this.GetValue(AuthorsProperty); }
			set { this.SetValue(AuthorsProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le texte indiquant les personnes qui ont contribué à la conception du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Personnes qui ont contribué à la conception du produit.</value>
		public string Credits
		{
			get { return (string) this.GetValue(CreditsProperty); }
			set { this.SetValue(CreditsProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit le texte indiquant les personnes qui ont contribué à la traduction du produit.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Personnes qui ont contribué à la traduction du produit.</value>
		public string Translators
		{
			get { return (string) this.GetValue(TranslatorsProperty); }
			set { this.SetValue(TranslatorsProperty, value); }
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
		/// Affiche l'accord de licence.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnLicenseRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			if(this.License!=null)
			{
				var window=new TextDialog
				{
					Owner=this,
					Text=this.License.Text,
					Title=Messages.LicenseAgreement,
					WordWrap=TextWrapping.NoWrap
				};

				window.ShowDialog();
				e.Handled=true;
			}
 		}

		/// <summary>
		/// Ouvre le site Web.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnWebSiteRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			e.Uri.Open();
			e.Handled=true;
		}

		//// =====================================================================================

		/// <summary>
		/// Met à jour la barre de titre lorsque la valeur de la propriété <see cref="Product" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnProductChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var window=sender as Window;
			if(window!=null) window.Title=string.Format(CultureInfo.CurrentCulture, Messages.AboutBoxTitle, e.NewValue);
		}
	}
}