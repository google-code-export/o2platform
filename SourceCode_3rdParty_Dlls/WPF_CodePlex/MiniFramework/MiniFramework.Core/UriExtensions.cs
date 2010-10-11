// <copyright file="UriExtensions.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.UriExtensions</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-28 09:02:01 +0200 (lun. 28 sept. 2009) $</date>
// <version>$Revision: 1906 $</version>

namespace MiniFramework
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Text.RegularExpressions;

	using MiniFramework.Reflection;
	using MiniFramework.Text;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes d'extension pour la gestion des URI.
	/// </summary>
	public static class UriExtensions
	{
		/// <summary>
		/// Crée une URI à en-tête &quot;pack&quot; qui pointe sur une ressource de l'assemblage du type appelant.
		/// </summary>
		/// <param name="uri">URI de la ressource référencée.</param>
		/// <returns>URI à en-tête &quot;pack&quot; qui pointe sur la ressource référencée de l'assemblage du type appelant.</returns>
		public static Uri MakePack(this Uri uri)
		{
			return MakePack(uri, Reflector.CallingType.Assembly);
		}

		/// <summary>
		/// Crée une URI à en-tête &quot;pack&quot; qui pointe sur une ressource de l'assemblage spécifié.
		/// </summary>
		/// <param name="uri">URI de la ressource référencée.</param>
		/// <param name="assembly">Assemblage contenant la ressource référencée.</param>
		/// <returns>URI à en-tête &quot;pack&quot; qui pointe sur la ressource référencée de l'assemblage spécifié.</returns>
		public static Uri MakePack(this Uri uri, Assembly assembly)
		{
			return MakePack(uri, assembly, false);
		}

		/// <summary>
		/// Crée une URI à en-tête &quot;pack&quot; qui pointe sur une ressource de l'assemblage spécifié.
		/// </summary>
		/// <param name="uri">URI de la ressource référencée.</param>
		/// <param name="assembly">Assemblage contenant la ressource référencée.</param>
		/// <param name="useStrongName">Valeur indiquant si l'URI retournée utilise le nom fort (i.e. avec le numéro de version et le jeton de clé publique) de l'assemblage spécifié.</param>
		/// <returns>URI à en-tête &quot;pack&quot; qui pointe sur la ressource référencée de l'assemblage spécifié.</returns>
		/// <exception cref="ArgumentNullException">L'URI ou l'assemblage spécifié est une référence null.</exception>
		public static Uri MakePack(this Uri uri, Assembly assembly, bool useStrongName)
		{
			// Validation des arguments
			if(uri==null) throw new ArgumentNullException("uri");
			if(assembly==null) throw new ArgumentNullException("assembly");

			// En-tête "pack"
			var builder=new StringBuilder("pack://application:,,,/");

			// Nom de l'assemblage
			if(!useStrongName) builder.Append(assembly.FullName.Split(',')[0]);
			else
			{
				var assemblyName=assembly.GetName();
				builder.Append(assemblyName.Name);
				builder.Append(";v");
				builder.Append(assemblyName.Version);
				builder.Append(';');
				builder.Append(HexCodec.GetString(assemblyName.GetPublicKeyToken()));
			}

			// Chemin de la ressource
			var absolutePath=(uri.IsAbsoluteUri) ? uri.AbsolutePath : uri.OriginalString.Split('?')[0];
			if(!absolutePath.StartsWith("/", StringComparison.Ordinal)) absolutePath='/'+absolutePath;

			builder.Append(";component");
			builder.Append(absolutePath);

			return new Uri(builder.ToString());
		}

		//// ========================================================================================

		/// <summary>
		/// Ouvre l'URI spécifiée à l'aide du programme associé par défaut.
		/// </summary>
		/// <param name="uri">URI à ouvrir avec le programme associé par défaut.</param>
		/// <exception cref="ArgumentNullException">L'URI spécifiée est une référence null.</exception>
		public static void Open(this Uri uri)
		{
			if(uri==null) throw new ArgumentNullException("uri");

			try { Process.Start(uri.ToString()); }
			catch(Win32Exception) {}
		}
	}
}