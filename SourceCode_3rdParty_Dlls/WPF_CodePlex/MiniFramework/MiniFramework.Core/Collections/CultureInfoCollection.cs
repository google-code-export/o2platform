// <copyright file="CultureInfoCollection.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPL) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Collections.CultureInfoCollection</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 20:13:52 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1984 $</version>

namespace MiniFramework.Collections
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;

	using MiniFramework.Properties;

	//// ========================================================================================

	/// <summary>
	/// Représente une collection d'objets <see cref="CultureInfo" />.
	/// </summary>
	public class CultureInfoCollection: Collection<CultureInfo>
	{
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="CultureInfoCollection" />.
		/// </summary>
		public CultureInfoCollection(): this(null) {}

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="CultureInfoCollection" /> qui contient des éléments copiés de la liste spécifiée.
		/// </summary>
		/// <param name="list">Liste dont les éléments sont copiés dans cette collection.</param>
		public CultureInfoCollection(IEnumerable<CultureInfo> list)
		{
			if(list!=null) foreach(var item in list) this.Add(item);
		}

		//// =====================================================================================

		/// <summary>
		/// Ajoute à la collection toutes les cultures correspondant à la langue spécifiée.
		/// </summary>
		/// <param name="isoCode">Code à 2 lettres ISO 639-1 indiquant la langue à ajouter.</param>
		/// <exception cref="ArgumentException">Le code spécifié est une chaîne vide ou une référence null.</exception>
		public void AddLanguage(string isoCode)
		{
			this.AddLanguage(isoCode, true);
		}

		/// <summary>
		/// Ajoute à la collection les cultures correspondant à la langue spécifiée.
		/// </summary>
		/// <param name="isoCode">Code à 2 lettres ISO 639-1 indiquant la langue à ajouter.</param>
		/// <param name="addAllCultures">Valeur indiquant si on ajoute toutes les cultures ou uniquement la culture principale de la langue spécifiée.</param>
		/// <exception cref="ArgumentException">Le code spécifié est une chaîne vide ou une référence null.</exception>
		public void AddLanguage(string isoCode, bool addAllCultures)
		{
			if(string.IsNullOrEmpty(isoCode)) throw new ArgumentException(Resources.EmptyIsoCode, "isoCode");
			isoCode=isoCode.ToLowerInvariant();

			if(!addAllCultures) this.Add(CultureInfo.CreateSpecificCulture(isoCode));
			else
			{
				foreach(var item in CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(x=>x.TwoLetterISOLanguageName==isoCode))
					this.Add(item);
			}
		}

		/// <summary>
		/// Ajoute à la collection toutes les cultures correspondant à la région spécifiée.
		/// </summary>
		/// <param name="isoCode">Code à 2 lettres ISO 3166 indiquant la région à ajouter.</param>
		/// <exception cref="ArgumentException">Le code spécifié est une chaîne vide ou une référence null.</exception>
		public void AddRegion(string isoCode)
		{
			if(string.IsNullOrEmpty(isoCode)) throw new ArgumentException(Resources.EmptyIsoCode, "isoCode");
			isoCode=isoCode.ToUpperInvariant();
			
			foreach(var item in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				if(item.Name.Split('-')[1]==isoCode) this.Add(item);
		}

		//// =======================================================================================

		/// <summary>
		/// Obtient une valeur indiquant si une culture utilisant le langage spécifié est présente dans la collection.
		/// </summary>
		/// <param name="isoCode">Code à 2 lettres ISO 639-1 indiquant la langue à ajouter.</param>
		/// <returns><see langword="true" /> si la collection contient une culture utilisant le langage spécifié, sinon <see langword="false" />.</returns>
		public bool ContainsLanguage(string isoCode)
		{
			return this.Any(culture=>culture.TwoLetterISOLanguageName.EquivalentToInvariant(isoCode));
		}

		/// <summary>
		/// Obtient une valeur indiquant si une culture utilisant la région spécifiée est présente dans la collection.
		/// </summary>
		/// <param name="isoCode">Code à 2 lettres ISO 3166 indiquant la région.</param>
		/// <returns><see langword="true" /> si la collection contient une culture utilisant la région spécifiée, sinon <see langword="false" />.</returns>
		public bool ContainsRegion(string isoCode)
		{
			return this.Any(culture=>new RegionInfo(culture.LCID).TwoLetterISORegionName.EquivalentToInvariant(isoCode));
		}

		//// =======================================================================================

		/// <summary>
		/// Insère un élément dans la collection à l'index spécifié.
		/// </summary>
		/// <param name="index">Index de base zéro au niveau duquel l'élément doit être inséré.</param>
		/// <param name="item">Objet à insérer.</param>
		/// <exception cref="ArgumentNullException">L'élément spécifié est une référence null.</exception>
		protected override void InsertItem(int index, CultureInfo item)
		{
			if(item==null) throw new ArgumentNullException("item");
			if(!this.Contains(item)) base.InsertItem(index, item);
		}

		/// <summary>
		/// Remplace l'élément au niveau de l'index spécifié. 
		/// </summary>
		/// <param name="index">Index de base zéro de l'élément à remplacer.</param>
		/// <param name="item">Nouvelle valeur de l'élément à l'index spécifié.</param>
		/// <exception cref="NotSupportedException">Cette méthode n'est pas supportée.</exception>
		protected override void SetItem(int index, CultureInfo item)
		{
			throw new NotSupportedException();
		}
	}
}