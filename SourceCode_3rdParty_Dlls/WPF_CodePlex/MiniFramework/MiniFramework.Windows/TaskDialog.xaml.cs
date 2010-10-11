// <copyright file="TaskDialog.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.TaskDialog</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-06 10:04:46 +0200 (mar. 06 oct. 2009) $</date>
// <version>$Revision: 2039 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	using MiniFramework.Resources;
	using MiniFramework.Windows.Controls;

	using Messages=MiniFramework.Windows.Properties.Resources;
	
	//// ========================================================================================

	/// <summary>
	/// Affiche un message.
	/// </summary>
	public partial class TaskDialog: Window
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="DetailsText" />.
		/// </summary>
		public static readonly DependencyProperty DetailsTextProperty=DependencyProperty.Register("DetailsText", typeof(string), typeof(TaskDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="FooterImage" />.
		/// </summary>
		public static readonly DependencyProperty FooterImageProperty=DependencyProperty.Register("FooterImage", typeof(ImageSource), typeof(TaskDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="FooterText" />.
		/// </summary>
		public static readonly DependencyProperty FooterTextProperty=DependencyProperty.Register("FooterText", typeof(string), typeof(TaskDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Image" />.
		/// </summary>
		public static readonly DependencyProperty ImageProperty=DependencyProperty.Register("Image", typeof(ImageSource), typeof(TaskDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="InstructionText" />.
		/// </summary>
		public static readonly DependencyProperty InstructionTextProperty=DependencyProperty.Register("InstructionText", typeof(string), typeof(TaskDialog));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Text" />.
		/// </summary>
		public static readonly DependencyProperty TextProperty=DependencyProperty.Register("Text", typeof(string), typeof(TaskDialog));

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="TaskDialog" />.
		/// </summary>
		public TaskDialog()
		{
			this.InitializeComponent();
			this.InstructionTextBlock.FontSize=this.FontSize*1.5;
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit le bouton d'annulation de la boîte de dialogue.
		/// </summary>
		/// <value>Bouton d'annulation de la boîte de dialogue, ou une référence null s'il n'y a aucun bouton d'annulation défini.</value>
		/// <exception cref="ArgumentNullException">Le bouton spécifié est une référence null.</exception>
		public Button CancelButton
		{
			get { return this.ButtonsPanel.Children.OfType<Button>().FirstOrDefault(x=>x.IsCancel); }

			set
			{
				if(value==null) throw new ArgumentNullException("value");

				var index=this.ButtonsPanel.Children.IndexOf(value);
				if(index>=0) ((Button) this.ButtonsPanel.Children[index]).IsCancel=true;
			}
		}

		/// <summary>
		/// Obtient ou définit le bouton par défaut de la boîte de dialogue.
		/// </summary>
		/// <value>Bouton par défaut de la boîte de dialogue, ou une référence null s'il n'y a aucun bouton par défaut défini.</value>
		/// <exception cref="ArgumentNullException">Le bouton spécifié est une référence null.</exception>
		public Button DefaultButton
		{
			get { return this.ButtonsPanel.Children.OfType<Button>().FirstOrDefault(x=>x.IsDefault); }

			set
			{
				if(value==null) throw new ArgumentNullException("value");

				var index=this.ButtonsPanel.Children.IndexOf(value);
				if(index>=0) ((Button) this.ButtonsPanel.Children[index]).IsDefault=true;
			}
		}

		/// <summary>
		/// Obtient une valeur énumérée spécifiant sur quel bouton du message l'utilisateur a cliqué.
		/// </summary>
		/// <value>Valeur qui spécifie sur quel bouton du message l'utilisateur a cliqué.</value>
		public new MessageBoxResult DialogResult
		{
			get;
			private set;
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit l'icône du pied de page.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Icône du pied de page.</value>
		public ImageSource FooterImage
		{
			get { return (ImageSource) this.GetValue(FooterImageProperty); }
			set { this.SetValue(FooterImageProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit l'icône affichée par la boîte de dialogue.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Icône affichée par la boîte de dialogue.</value>
		public ImageSource Image
		{
			get { return (ImageSource) this.GetValue(ImageProperty); }
			set { this.SetValue(ImageProperty, value); }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient ou définit le texte des détails.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte des détails.</value>
		public string DetailsText
		{
			get { return (string) this.GetValue(DetailsTextProperty); }
			set { this.SetValue(DetailsTextProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit le texte du pied de page.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Texte du pied de page.</value>
		public string FooterText
		{
			get { return (string) this.GetValue(FooterTextProperty); }
			set { this.SetValue(FooterTextProperty, value); }
		}
		
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

		//// =====================================================================================
		
		/// <summary>
		/// Ajoute à la boîte de dialogue un bouton affichant le texte spécifié.
		/// </summary>
		/// <param name="text">Texte affiché sur le bouton.</param>
		/// <param name="result">Valeur énumérée spécifiant le résultat associé au bouton.</param>
		/// <returns>Bouton ajouté.</returns>
		public Button AddButton(string text, MessageBoxResult result)
		{
			return this.AddButton(text, result, null);
		}

		/// <summary>
		/// Ajoute à la boîte de dialogue un bouton affichant le texte et l'icône spécifiés.
		/// </summary>
		/// <param name="text">Texte affiché sur le bouton.</param>
		/// <param name="result">Valeur énumérée spécifiant le résultat associé au bouton.</param>
		/// <param name="icon">Icône affichée sur le bouton.</param>
		/// <returns>Bouton ajouté.</returns>
		public Button AddButton(string text, MessageBoxResult result, ImageSource icon)
		{
			var button=new PushButton
			{
				Icon=icon,
				Tag=result,
				Text=text
			};

			this.AddButton(button);
			return button;
		}
		
		/// <summary>
		/// Ouvre la fenêtre et retourne uniquement lorsqu'elle est fermée.
		/// </summary>
		/// <returns>Valeur qui spécifie sur quel bouton du message l'utilisateur a cliqué.</returns>
		public new MessageBoxResult ShowDialog()
		{
			base.ShowDialog();
			return this.DialogResult;
		}

		//// =====================================================================================

		/// <summary>
		/// Affiche un message avec le texte spécifié.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text)
		{
			return Show(owner, text, null);
		}

		/// <summary>
		/// Affiche un message avec le texte et l'instruction principale spécifiés.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <param name="instructionText">Texte de l'instruction principale.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text, string instructionText)
		{
			return Show(owner, text, instructionText, null);
		}

		/// <summary>
		/// Affiche un message avec le texte, l'instruction principale et la légende de barre de titre spécifiés.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <param name="instructionText">Texte de l'instruction principale.</param>
		/// <param name="caption">Texte à afficher dans la barre de titre du message.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text, string instructionText, string caption)
		{
			return Show(owner, text, instructionText, caption, MessageBoxButton.OK);
		}

		/// <summary>
		/// Affiche un message d'erreur avec le texte, l'instruction principale, la légende de barre de titre et les boutons spécifiés.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <param name="instructionText">Texte de l'instruction principale.</param>
		/// <param name="caption">Texte à afficher dans la barre de titre du message.</param>
		/// <param name="buttons">Boutons à afficher dans le message.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text, string instructionText, string caption, MessageBoxButton buttons)
		{
			return Show(owner, text, instructionText, caption, buttons, MessageBoxImage.None);
		}

		/// <summary>
		/// Affiche un message d'erreur avec le texte, l'instruction principale, la légende de barre de titre, les boutons et l'icône spécifiés.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <param name="instructionText">Texte de l'instruction principale.</param>
		/// <param name="caption">Texte à afficher dans la barre de titre du message.</param>
		/// <param name="buttons">Boutons à afficher dans le message.</param>
		/// <param name="icon">Valeur énumérée spécifiant l'icône à afficher.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text, string instructionText, string caption, MessageBoxButton buttons, MessageBoxImage icon)
		{
			return Show(owner, text, instructionText, caption, buttons, icon, MessageBoxResult.OK);
		}

		/// <summary>
		/// Affiche un message d'erreur avec le texte, l'instruction principale, la légende de barre de titre, les boutons, l'icône et le résultat par défaut spécifiés.
		/// </summary>
		/// <param name="owner">Fenêtre qui représente le propriétaire de cette boîte de dialogue.</param>
		/// <param name="text">Texte du message.</param>
		/// <param name="instructionText">Texte de l'instruction principale.</param>
		/// <param name="caption">Texte à afficher dans la barre de titre du message.</param>
		/// <param name="buttons">Boutons à afficher dans le message.</param>
		/// <param name="icon">Valeur énumérée spécifiant l'icône à afficher.</param>
		/// <param name="defaultResult">Valeur énumérée spécifiant le résultat par défaut du message.</param>
		/// <returns>Valeur énumérée indiquant le résultat de la boîte de dialogue, ou <see cref="MessageBoxResult.None" /> si l'utilisateur a rejeté le message.</returns>
		public static MessageBoxResult Show(Window owner, string text, string instructionText, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defaultResult)
		{
			// Construction de la boîte de dialogue
			var window=new TaskDialog
			{
				Owner=owner,
				InstructionText=instructionText,
				Text=text,
				Title=caption ?? string.Empty
			};

			// Ajout de l'icône et d'une légende de barre de titre
			switch(icon)
			{
				case MessageBoxImage.Error:
					window.Image=StockIcons.Error.LargeBitmapImage;
					if(caption==null) window.Title=Messages.Error;
					break;

				case MessageBoxImage.Information:
					window.Image=StockIcons.Information.LargeBitmapImage;
					if(caption==null) window.Title=Messages.Information;
					break;

				case MessageBoxImage.Question:
					window.Image=StockIcons.Question.LargeBitmapImage;
					if(caption==null) window.Title=Messages.Question;
					break;

				case MessageBoxImage.Warning:
					window.Image=StockIcons.Warning.LargeBitmapImage;
					if(caption==null) window.Title=Messages.Warning;
					break;
			}

			// Ajout des boutons
			Button button;

			if(buttons==MessageBoxButton.OK || buttons==MessageBoxButton.OKCancel)
			{
				button=window.AddButton(Messages.OkButton, MessageBoxResult.OK, StockIcons.Accept.SmallBitmapImage);
				if(defaultResult==MessageBoxResult.OK) window.DefaultButton=button;
			}
			
			if(buttons==MessageBoxButton.YesNo || buttons==MessageBoxButton.YesNoCancel)
			{
				button=window.AddButton(Messages.YesButton, MessageBoxResult.Yes, StockIcons.Accept.SmallBitmapImage);
				if(defaultResult==MessageBoxResult.Yes) window.DefaultButton=button;

				button=window.AddButton(Messages.NoButton, MessageBoxResult.No, StockIcons.Delete.SmallBitmapImage);
				if(defaultResult==MessageBoxResult.No) window.DefaultButton=button;
			}

			if(buttons==MessageBoxButton.OKCancel || buttons==MessageBoxButton.YesNoCancel)
			{
				button=window.AddButton(Messages.CancelButton, MessageBoxResult.Cancel, StockIcons.Cancel.SmallBitmapImage);
				window.CancelButton=button;
				if(defaultResult==MessageBoxResult.Cancel) window.DefaultButton=button;
			}

			// Exécution de la boîte de dialogue
			return window.ShowDialog();
		}

		//// =====================================================================================
		
		/// <summary>
		/// Ajoute à la boîte de dialogue le bouton spécifié.
		/// </summary>
		/// <param name="button">Bouton à ajouter.</param>
		/// <exception cref="ArgumentNullException">Le bouton spécifié est une référence null.</exception>
		private void AddButton(Button button)
		{
			if(button==null) throw new ArgumentNullException("button");
			
			// Gestionnaires d'événements
			button.Command=ApplicationCommands.Close;
			button.CommandParameter=button.Tag;
			DockPanel.SetDock(button, Dock.Right);

			// Le bouton doit être inséré en première position : le flux de la mise en page
			// est aligné sur la droite, ce qui inverse l'ordre d'affichage des boutons
			var buttons=new Button[this.ButtonsPanel.Children.Count];
			buttons[0]=button;
			this.ButtonsPanel.Children.OfType<Button>().ToArray().CopyTo(buttons, 1);

			// Ajout des boutons à la boîte de dialogue
			this.ButtonsPanel.Children.Clear();
			foreach(var item in buttons) this.ButtonsPanel.Children.Add(item);
			this.ButtonsPanel.Children.Add(this.Expander);
		}

		/// <summary>
		/// Ferme la fenêtre.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.DialogResult=(MessageBoxResult) e.Parameter;
			this.Close();
			e.Handled=true;
		}

		/// <summary>
		/// Ajoute un bouton par défaut à la boîte de dialogue lors de son affichage si celle-ci ne possède aucun bouton.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnWindowLoaded(object sender, RoutedEventArgs e)
		{
			if(this.ButtonsPanel.Children.Count<=1)
				this.DefaultButton=this.AddButton(Messages.OkButton, MessageBoxResult.OK, (BitmapImage) this.Resources["AcceptButton"]);
			e.Handled=true;
		}
	}
}