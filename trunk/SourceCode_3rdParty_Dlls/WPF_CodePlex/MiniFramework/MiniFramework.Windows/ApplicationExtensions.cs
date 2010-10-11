// <copyright file="ApplicationExtensions.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.ApplicationExtensions</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 23:55:02 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1990 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Windows;

	using MiniFramework.Reflection;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes d'extension pour la gestion des applications.
	/// </summary>
	public static class ApplicationExtensions
	{
		/// <summary>
		/// Obtient le chemin d'accès au fichier exécutable ayant démarré l'application spécifiée, y compris le nom de l'exécutable.
		/// </summary>
		/// <param name="application">Application dont on veut obtenir le chemin d'accès au fichier exécutable.</param>
		/// <returns>Chemin d'accès au fichier exécutable ayant démarré l'application spécifiée.</returns>
		/// <exception cref="ArgumentNullException">L'application spécifiée est une référence null.</exception>
		public static string GetExecutablePath(this Application application)
		{
			if(application==null) throw new ArgumentNullException("application");
			return application.GetType().Assembly.Location;
		}

		/// <summary>
		/// Obtient le chemin d'accès au fichier exécutable ayant démarré l'application spécifiée, y compris le nom de l'exécutable.
		/// </summary>
		/// <param name="application">Application dont on veut obtenir le chemin d'accès au fichier exécutable.</param>
		/// <returns>Chemin d'accès au fichier exécutable ayant démarré l'application spécifiée.</returns>
		public static string GetStartupPath(this Application application)
		{
			return Path.GetDirectoryName(GetExecutablePath(application));
		}

		//// =====================================================================================

		/// <summary>
		/// Arrête l'application spécifiée et démarre immédiatement une nouvelle instance.
		/// </summary>
		/// <remarks>
		/// Si l'application a été initialement fournie avec des options de ligne de commande lors de sa première exécution, la méthode lance à nouveau l'application avec les mêmes options.
		/// </remarks>
		/// <param name="application">Application à redémarrer.</param>
		public static void Restart(this Application application)
		{
			var startInfo=new ProcessStartInfo(GetExecutablePath(application));
			
			// Construction des arguments de ligne de commande
			var args=Environment.GetCommandLineArgs();
			if(args.Length>1)
			{
				var builder=new StringBuilder();
				
				builder.Append('"');
				builder.Append(string.Join("\" \"", args, 1, args.Length-1));
				builder.Append('"');

				startInfo.Arguments=builder.ToString();
			}

			// Redémarrage de l'application
			Process.Start(startInfo);
			application.Shutdown();
		}
	}
}