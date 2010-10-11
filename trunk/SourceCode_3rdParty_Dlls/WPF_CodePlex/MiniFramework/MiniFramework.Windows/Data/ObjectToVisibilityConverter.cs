// <copyright file="ObjectToVisibilityConverter.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Data.ObjectToVisibilityConverter</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-14 17:51:09 +0200 (lun. 14 sept. 2009) $</date>
// <version>$Revision: 1838 $</version>

namespace MiniFramework.Windows.Data
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using System.Windows.Data;

	//// ========================================================================================

	/// <summary>
	/// Représente le convertisseur qui convertit des objets en valeurs d'énumération <see cref="Visibility" />.
	/// </summary>
	/// <remarks>
	/// Cette classe convertit une référence null en valeur <see cref="Visibility.Collapsed" />. Toute référence non null est convertie en valeur <see cref="Visibility.Visible" />.
	/// </remarks>
	[ValueConversion(typeof(object), typeof(Visibility))]
	public class ObjectToVisibilityConverter: IValueConverter
	{
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="ObjectToVisibilityConverter" />.
		/// </summary>
		public ObjectToVisibilityConverter() {}

		//// =====================================================================================

		/// <summary>
		/// Convertit une valeur.
		/// </summary>
		/// <param name="value">Valeur produite par la source de liaison.</param>
		/// <param name="targetType">Type de la propriété de cible de liaison.</param>
		/// <param name="parameter">Paramètre de convertisseur à utiliser.</param>
		/// <param name="culture">Culture à utiliser dans le convertisseur.</param>
		/// <returns>Une valeur convertie.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value!=null ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// Convertit une valeur.
		/// </summary>
		/// <param name="value">Valeur produite par la source de liaison.</param>
		/// <param name="targetType">Type dans lequel convertir.</param>
		/// <param name="parameter">Paramètre de convertisseur à utiliser.</param>
		/// <param name="culture">Culture à utiliser dans le convertisseur.</param>
		/// <returns>Une valeur convertie.</returns>
		/// <exception cref="NotSupportedException">Cette méthode n'est pas supportée.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}