// <copyright file="WindowExtensions.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Windows.WindowExtensions</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 20:14:51 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1985 $</version>

namespace MiniFramework.Windows
{
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Interop;

	using MiniFramework.Windows.Interop;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes d'extension pour la gestion des fenêtres et boîtes de dialogue.
	/// </summary>
	public static class WindowExtensions
	{
		/// <summary>
		/// Désactive le bouton de réduction de la fenêtre spécifiée.
		/// </summary>
		/// <remarks>
		/// Cette méthode doit être appelée avant que l'événement <see cref="Window.SourceInitialized" /> ne soit déclenché.
		/// </remarks>
		/// <param name="window">Fenêtre dont le bouton de réduction doit être désactivé.</param>
		/// <exception cref="ArgumentNullException">La fenêtre spécifiée est une référence null.</exception>
		public static void DisableMinimizeBox(this Window window)
		{
			if(window==null) throw new ArgumentNullException("window");
			
			window.SourceInitialized+=delegate
			{
				var handle=GetHandle(window);
				var style=NativeMethods.GetWindowLong(handle, NativeMethods.GwlStyle) & ~NativeMethods.WSMinimizeBox;
				NativeMethods.SetWindowLong(handle, NativeMethods.GwlStyle, style);
			};
		}

		/// <summary>
		/// Obtient le pointeur de fenêtre Win32 de la fenêtre spécifiée.
		/// </summary>
		/// <param name="window">Fenêtre dont on veut obtenir le pointeur de fenêtre.</param>
		/// <returns>Pointeur de fenêtre Win32, ou <see cref="IntPtr.Zero" /> si la fenêtre spécifiée est une référence null.</returns>
		public static IntPtr GetHandle(this Window window)
		{
			if(window==null) return IntPtr.Zero;
			return new WindowInteropHelper(window).Handle;
		}
	}
}