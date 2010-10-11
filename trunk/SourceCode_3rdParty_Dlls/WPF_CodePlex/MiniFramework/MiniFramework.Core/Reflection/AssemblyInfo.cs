// <copyright file="AssemblyInfo.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Reflection.AssemblyInfo</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Reflection
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Reflection;

	//// ========================================================================================

	/// <summary>
	/// Représente les informations de description d'un assemblage.
	/// </summary>
	public class AssemblyInfo
	{
		/// <summary>
		/// Assemblage dont on extrait les informations.
		/// </summary>
		private Assembly assembly;

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="AssemblyInfo" />.
		/// </summary>
		/// <param name="assembly">Assemblage dont on doit extraire les informations.</param>
		/// <exception cref="ArgumentNullException">L'assemblage spécifié est une référence null.</exception>
		public AssemblyInfo(Assembly assembly)
		{
			if(assembly==null) throw new ArgumentNullException("assembly");
			this.assembly=assembly;
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient les informations relatives au nom de la société.
		/// </summary>
		/// <value>Chaîne contenant le nom de la société.</value>
		public string Company
		{
			get { return this.GetAssemblyAttribute("Company"); }
		}

		/// <summary>
		/// Obtient des informations se rapportant à la configuration de l'assemblage.
		/// </summary>
		/// <value>Chaîne qui contient les informations se rapportant à la configuration de l'assemblage.</value>
		public string Configuration
		{
			get { return this.GetAssemblyAttribute("Configuration"); }
		}

		/// <summary>
		/// Obtient les informations de copyright.
		/// </summary>
		/// <value>Chaîne contenant les informations de copyright.</value>
		public string Copyright
		{
			get { return this.GetAssemblyAttribute("Copyright"); }
		}

		/// <summary>
		/// Obtient la culture prise en charge par l'assemblage.
		/// </summary>
		/// <value>Culture prise en charge.</value>
		public CultureInfo Culture
		{
			get { return this.assembly.GetName().CultureInfo; }
		}

		/// <summary>
		/// Obtient des informations se rapportant à la description de l'assemblage.
		/// </summary>
		/// <value>Chaîne contenant la description de l'assemblage.</value>
		public string Description
		{
			get { return this.GetAssemblyAttribute("Description"); }
		}

		/// <summary>
		/// Obtient le numéro de version du fichier de l'assemblage.
		/// </summary>
		/// <value>Numéro de version du fichier de l'assemblage, ou version de l'assemblage si la version du fichier n'est pas fournie.</value>
		public Version FileVersion
		{
			get
			{
				var attribute=this.GetAssemblyAttribute("FileVersion");
				return string.IsNullOrEmpty(attribute) ? this.Version : new Version(attribute);
			}
		}

		/// <summary>
		/// Obtient les informations relatives au nom du produit.
		/// </summary>
		/// <value>Chaîne contenant le nom du produit.</value>
		public string Product
		{
			get
			{
				var attribute=this.GetAssemblyAttribute("Product");
				return string.IsNullOrEmpty(attribute) ? this.Title : attribute;
			}
		}

		/// <summary>
		/// Obtient des informations relatives au titre de l'assemblage.
		/// </summary>
		/// <value>Chaîne contenant le titre de l'assemblage.</value>
		public string Title
		{
			get
			{
				var attribute=this.GetAssemblyAttribute("Title");
				return string.IsNullOrEmpty(attribute) ? Path.GetFileNameWithoutExtension(this.assembly.CodeBase) : attribute;
			}
		}

		/// <summary>
		/// Obtient les informations relatives à la marque.
		/// </summary>
		/// <value>Chaîne contenant les informations relatives à la marque.</value>
		public string Trademark
		{
			get { return this.GetAssemblyAttribute("Trademark"); }
		}

		/// <summary>
		/// Obtient le numéro de version de l'assemblage.
		/// </summary>
		/// <value>Numéro de version de l'assemblage.</value>
		public Version Version
		{
			get { return this.assembly.GetName().Version; }
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient la valeur du premier attribut d'assemblage correspondant au nom d'attribut spécifié.
		/// </summary>
		/// <param name="name">Nom de l'attribut d'assemblage recherché.</param>
		/// <returns>Valeur du premier attribut d'assemblage correspondant au nom spécifié, ou un référence null s'il n'y en a aucun.</returns>
		private string GetAssemblyAttribute(string name)
		{
			var type=Type.GetType(string.Format(CultureInfo.InvariantCulture, "System.Reflection.Assembly{0}Attribute", name));
			var attributes=this.assembly.GetCustomAttributes(type, false);
			
			return attributes.Length==0 ? null : Reflector.GetPropertyValue(attributes[0], name).ToString();
		}
	}
}