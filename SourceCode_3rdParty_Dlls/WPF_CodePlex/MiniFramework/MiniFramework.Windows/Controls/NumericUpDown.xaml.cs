// <copyright file="NumericUpDown.xaml.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Controls.NumericUpDown</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Windows.Controls
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	//// ========================================================================================

	/// <summary>
	/// Représente une zone de sélection qui affiche des valeurs numériques.
	/// </summary>
	/// TODO: le formatage des décimales ne fonctionne pas !!! Ainsi que les touches fléchées Haut/Bas
	public partial class NumericUpDown: UserControl
	{
		/// <summary>
		/// Identifie la propriété de dépendance <see cref="DecimalPlaces" />.
		/// </summary>
		public static readonly DependencyProperty DecimalPlacesProperty=DependencyProperty.Register
		(
			"DecimalPlaces",
			typeof(int),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(OnDecimalPlacesPropertyChanged),
			OnValidateDecimalPlaces
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Increment" />.
		/// </summary>
		public static readonly DependencyProperty IncrementProperty=DependencyProperty.Register
		(
			"Increment",
			typeof(decimal),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(1m, null, OnCoerceIncrement),
			OnValidateIncrement
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Maximum" />.
		/// </summary>
		public static readonly DependencyProperty MaximumProperty=DependencyProperty.Register
		(
			"Maximum",
			typeof(decimal),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(100m, OnMaximumPropertyChanged, OnCoerceMaximum)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Minimum" />.
		/// </summary>
		public static readonly DependencyProperty MinimumProperty=DependencyProperty.Register
		(
			"Minimum",
			typeof(decimal),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(0m, OnMinimumPropertyChanged, OnCoerceMinimum)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="TextAlign" />.
		/// </summary>
		public static readonly DependencyProperty TextAlignProperty=DependencyProperty.Register("TextAlign", typeof(HorizontalAlignment), typeof(NumericUpDown));

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="UpDownAlign" />.
		/// </summary>
		public static readonly DependencyProperty UpDownAlignProperty=DependencyProperty.Register
		(
			"UpDownAlign",
			typeof(Dock),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(Dock.Right)
		);

		/// <summary>
		/// Identifie la propriété de dépendance <see cref="Value" />.
		/// </summary>
		public static readonly DependencyProperty ValueProperty=DependencyProperty.Register
		(
			"Value",
			typeof(decimal),
			typeof(NumericUpDown),
			new FrameworkPropertyMetadata(0m, OnValuePropertyChanged, OnCoerceValue)
		);

		/// <summary>
		/// Informations sur le format des nombres.
		/// </summary>
		private NumberFormatInfo numberFormat;

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="NumericUpDown" />.
		/// </summary>
		public NumericUpDown()
		{
			this.numberFormat=(NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
			this.InitializeComponent();
			this.UpdateValueString();
		}

		//// =====================================================================================

		/// <summary>
		/// Se produit lorsque la propriété <see cref="Value" /> est modifiée.
		/// </summary>
		public event DependencyPropertyChangedEventHandler ValueChanged;

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit le nombre de décimales à afficher dans la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Nombre de décimales à afficher dans la zone de sélection numérique. La valeur par défaut est zéro.</value>
		public int DecimalPlaces
		{
			get { return (int) this.GetValue(DecimalPlacesProperty); }
			set { this.SetValue(DecimalPlacesProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la valeur pour incrémenter ou décrémenter la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur pour incrémenter ou décrémenter la zone de sélection numérique. La valeur par défaut est 1 (un).</value>
		public decimal Increment
		{
			get { return (decimal) this.GetValue(IncrementProperty); }
			set { this.SetValue(IncrementProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la valeur maximale de la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur maximale de la zone de sélection numérique. La valeur par défaut est 100.</value>
		public decimal Maximum
		{
			get { return (decimal) this.GetValue(MaximumProperty); }
			set { this.SetValue(MaximumProperty, value); }
		}

		/// <summary>
		/// Obtient ou définit la valeur minimale de la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur minimale de la zone de sélection numérique. La valeur par défaut est zéro.</value>
		public decimal Minimum
		{
			get { return (decimal) this.GetValue(MinimumProperty); }
			set { this.SetValue(MinimumProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit l'alignement de texte dans la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Alignement du texte. La valeur par défaut est <see cref="HorizontalAlignment.Left" />.</value>
		public HorizontalAlignment TextAlign
		{
			get { return (HorizontalAlignment) this.GetValue(TextAlignProperty); }
			set { this.SetValue(TextAlignProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit l'alignement des boutons Haut et Bas sur la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Alignement des boutons Haut et Bas. La valeur par défaut est <see cref="Dock.Right" />.</value>
		public Dock UpDownAlign
		{
			get { return (Dock) this.GetValue(UpDownAlignProperty); }
			set { this.SetValue(UpDownAlignProperty, value); }
		}
		
		/// <summary>
		/// Obtient ou définit la valeur assignée à la zone de sélection numérique.
		/// </summary>
		/// <remarks>
		/// Il s'agit d'une propriété de dépendance.
		/// </remarks>
		/// <value>Valeur numérique du contrôle.</value>
		public decimal Value
		{
			get { return (decimal) this.GetValue(ValueProperty); }
			set { this.SetValue(ValueProperty, value); }
		}

		//// =====================================================================================

		/// <summary>
		/// Déclenche l'événement <see cref="ValueChanged" />.
		/// </summary>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e)
		{
			var handler=this.ValueChanged;
			if(handler!=null) handler(this, e);
		}

		//// =====================================================================================

		/// <summary>
		/// Vérifie si on peut décrémenter la valeur.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnMoveDownCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=(this.Value>this.Minimum);
			e.Handled=true;
		}

		/// <summary>
		/// Décrémente la valeur.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnMoveDownExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.Value-=this.Increment;
		}

		/// <summary>
		/// Vérifie si on peut incrémenter la valeur.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnMoveUpCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=(this.Value<this.Maximum);
			e.Handled=true;
		}

		/// <summary>
		/// Incrémente la valeur.
		/// </summary>
		/// <param name="sender">Objet source de l'événement.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private void OnMoveUpExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.Value+=this.Increment;
		}

		//// =====================================================================================

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Increment" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceIncrement(DependencyObject sender, object baseValue)
		{
			var control=sender as NumericUpDown;
			if(control!=null)
			{
				var value=(decimal) baseValue;

				// Si l'incrément est 0.1 et que le nombre de décimales passe de 1 à 0,
				// le nouvel incrément doit être 1, pas 0. L'incrément doit toujours
				// être arrondi au nombre de décimales, mais jamais plus petit
				// que l'incrément précédent
				var coercedValue=Decimal.Round(value, control.DecimalPlaces);
				if(coercedValue<value)
				{
					var temp=1m;
					for(int i=0; i<control.DecimalPlaces; i++) temp/=10;
					coercedValue=temp;
				}

				return coercedValue;
			}

			return IncrementProperty.DefaultMetadata.DefaultValue;
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Maximum" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceMaximum(DependencyObject sender, object baseValue)
		{
			var control=sender as NumericUpDown;
			return control!=null
				? Decimal.Round(Math.Max((decimal) baseValue, control.Minimum), control.DecimalPlaces)
				: MaximumProperty.DefaultMetadata.DefaultValue;
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Minimum" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceMinimum(DependencyObject sender, object baseValue)
		{
			var control=sender as NumericUpDown;
			return control!=null ? Decimal.Round((decimal) baseValue, control.DecimalPlaces) : MinimumProperty.DefaultMetadata.DefaultValue;
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Value" />.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="baseValue">Valeur de la propriété, avant toute tentative de contrainte.</param>
		private static object OnCoerceValue(DependencyObject sender, object baseValue)
		{
			var control=sender as NumericUpDown;
			return control!=null
				? Decimal.Round(Math.Max(control.Minimum, Math.Min(control.Maximum, (decimal) baseValue)), control.DecimalPlaces)
				: ValueProperty.DefaultMetadata.DefaultValue;
		}

		//// =====================================================================================

		/// <summary>
		/// Met à jour l'affichage de la valeur lorsque la propriété <see cref="DecimalPlaces" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnDecimalPlacesPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			sender.CoerceValue(IncrementProperty);
			sender.CoerceValue(MinimumProperty);
			sender.CoerceValue(MaximumProperty);
			sender.CoerceValue(ValueProperty);

         var control=sender as NumericUpDown;
			if(control!=null) control.UpdateValueString();
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Value" /> lorsque la propriété <see cref="Maximum" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnMaximumPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
         sender.CoerceValue(ValueProperty);
		}

		/// <summary>
		/// Contraint la valeur de la propriété <see cref="Value" /> lorsque la propriété <see cref="Minimum" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnMinimumPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
         sender.CoerceValue(MaximumProperty);
         sender.CoerceValue(ValueProperty);
		}

		/// <summary>
		/// Déclenche l'événement <see cref="ValueChanged" /> lorsque la valeur de la propriété <see cref="Value" /> change.
		/// </summary>
		/// <param name="sender">Objet dans lequel la propriété a été modifiée.</param>
		/// <param name="e">Objet contenant les données de l'événement.</param>
		private static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control=sender as NumericUpDown;
			if(control!=null)
			{
				control.OnValueChanged(e);
				control.UpdateValueString();
			}
		}

		//// =====================================================================================

		/// <summary>
		/// Valide la valeur de la propriété <see cref="DecimalPlaces" />.
		/// </summary>
		/// <param name="value">La valeur à valider.</param>
		/// <returns><see langword="true" /> si la valeur a été validée ; <see langword="false" /> si la valeur soumise n'était pas valide.</returns>
		private static bool OnValidateDecimalPlaces(object value)
		{
			return (int) value>=0;
		}

		/// <summary>
		/// Valide la valeur de la propriété <see cref="Increment" />.
		/// </summary>
		/// <param name="value">La valeur à valider.</param>
		/// <returns><see langword="true" /> si la valeur a été validée ; <see langword="false" /> si la valeur soumise n'était pas valide.</returns>
		private static bool OnValidateIncrement(object value)
		{
			return (decimal) value>0;
		}

		//// =====================================================================================

		/// <summary>
		/// Met à jour l'affichage de la valeur.
		/// </summary>
		private void UpdateValueString()
		{
			this.numberFormat.NumberDecimalDigits=this.DecimalPlaces;
			this.ValueString.Text=this.Value.ToString("f", this.numberFormat);
		}
	}
}