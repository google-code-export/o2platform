// <copyright file="NativeMethods.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.Interop.NativeMethods</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Windows.Interop
{
	using System;
	using System.Linq;
	using System.Runtime.InteropServices;
	
	//// ========================================================================================

	/// <summary>
	/// Fournit des appels de plate-forme permettant d'appeler des fonctions non gérées.
	/// </summary>
	internal static class NativeMethods
	{
		/// <summary>
		/// Valeur spécifiant les styles d'une fenêtre.
		/// </summary>
		public const int GwlStyle=-16;
		
		/// <summary>
		/// Valeur spécifiant le bouton de réduction d'une fenêtre.
		/// </summary>
		public const int WSMinimizeBox=0x00020000;

		//// =====================================================================================

		/// <summary>
		/// Obtient un attribut la fenêtre spécifiée.
		/// </summary>
		/// <param name="hWnd">Pointeur de la fenêtre.</param>
		/// <param name="nIndex">Décalage de base zéro de l'attribut à obtenir.</param>
		/// <returns>Valeur de l'attribut, ou zéro si une erreur s'est produite.</returns>
		[DllImport("User32")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		/// <summary>
		/// Définit un attribut de la fenêtre spécifiée.
		/// </summary>
		/// <param name="hWnd">Pointeur de la fenêtre à modifier.</param>
		/// <param name="nIndex">Décalage de base zéro de l'attribut à définir.</param>
		/// <param name="dwNewLong">Valeur de l'attribut.</param>
		/// <returns>Ancienne valeur de l'attribut, ou zéro si une erreur s'est produite.</returns>
		[DllImport("User32")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
	}
}