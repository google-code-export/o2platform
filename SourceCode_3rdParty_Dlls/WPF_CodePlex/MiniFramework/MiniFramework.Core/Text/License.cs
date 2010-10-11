// <copyright file="License.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Text.License</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 20:13:52 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1984 $</version>

namespace MiniFramework.Text
{
	using System;
	using System.Linq;

	//// ========================================================================================

	/// <summary>
	/// Représente un accord de licence.
	/// </summary>
	public class License: IEquatable<License>
	{
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="License" />.
		/// </summary>
		public License() {}

		//// =====================================================================================

		/// <summary>
		/// Obtient ou définit le nom de l'accord de licence.
		/// </summary>
		/// <value>Nom de la licence.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Obtient ou définit le texte de l'accord de licence.
		/// </summary>
		/// <value>Texte de la licence.</value>
		public string Text
		{
			get;
			set;
		}

		//// =====================================================================================

		/// <summary>
		/// Détermine si l'objet spécifié est égal à l'objet en cours.
		/// </summary>
		/// <remarks>
		/// Deux accords de licence sont considérés comme égaux s'ils portent le même nom, indépendamment de leur texte.
		/// </remarks>
		/// <param name="other">Objet à comparer.</param>
		/// <returns><see langword="true" /> si l'objet spécifié est égal à l'objet en cours, sinon <see langword="false" />.</returns>
		public bool Equals(License other)
		{
			if(other==null) return false;
			return this.Name==other.Name;
		}

		/// <summary>
		/// Détermine si l'objet spécifié est égal à l'objet en cours.
		/// </summary>
		/// <param name="obj">Objet à comparer.</param>
		/// <returns><see langword="true" /> si l'objet spécifié est égal à l'objet en cours, sinon <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			return this.Equals(obj as License);
		}

		//// =====================================================================================

		/// <summary>
		/// Génère un code de hachage correspondant à la valeur de l'objet en cours.
		/// </summary>
		/// <returns>Code de hachage pour l'objet en cours.</returns>
		public override int GetHashCode()
		{
			return this.Name!=null ? this.Name.GetHashCode() : 0;
		}
		
		/// <summary>
		/// Obtient une chaîne qui représente l'objet en cours.
		/// </summary>
		/// <returns>Valeur de la propriété <see cref="Text" />.</returns>
		public override string ToString()
		{
			return this.Text ?? string.Empty;
		}
	}
}