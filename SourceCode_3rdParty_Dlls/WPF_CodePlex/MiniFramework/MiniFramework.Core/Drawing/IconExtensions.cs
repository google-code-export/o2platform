// <copyright file="IconExtensions.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Drawing.IconExtensions</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Drawing
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Linq;
	using System.Windows.Media.Imaging;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes d'extension pour la gestion des icônes.
	/// </summary>
	public static class IconExtensions
	{
		/// <summary>
		/// Convertit l'icône spécifiée en <see cref="Bitmap" /> de la taille spécifiée.
		/// </summary>
		/// <param name="icon">Icône à convertir.</param>
		/// <param name="size">Entier spécifiant la hauteur et la largeur en pixels de l'image retournée.</param>
		/// <returns>Image de la taille spécifiée qui représente l'icône spécifiée.</returns>
		public static Bitmap ToBitmap(this Icon icon, int size)
		{
			return ToBitmap(icon, new Size(size, size));
		}

		/// <summary>
		/// Convertit l'icône spécifiée en <see cref="Bitmap" /> de la taille spécifiée.
		/// </summary>
		/// <param name="icon">Icône à convertir.</param>
		/// <param name="size">Taille en pixels de l'image retournée.</param>
		/// <returns>Image de la taille spécifiée qui représente l'icône spécifiée.</returns>
		/// <exception cref="ArgumentNullException">L'icône spécifiée est une référence null.</exception>
		public static Bitmap ToBitmap(this Icon icon, Size size)
		{
			if(icon==null) throw new ArgumentNullException("icon");

			var bitmap=new Icon(icon, size).ToBitmap();
			return bitmap.Size==size ? bitmap : new Bitmap(bitmap, size);
		}

		//// =====================================================================================

		/// <summary>
		/// Convertit l'icône spécifiée en <see cref="BitmapImage" /> de la taille spécifiée.
		/// </summary>
		/// <param name="icon">Icône à convertir.</param>
		/// <param name="size">Entier spécifiant la hauteur et la largeur en pixels de l'image retournée.</param>
		/// <returns>Image de la taille spécifiée qui représente l'icône spécifiée.</returns>
		public static BitmapImage ToBitmapImage(this Icon icon, int size)
		{
			return ToBitmapImage(icon, new Size(size, size));
		}

		/// <summary>
		/// Convertit l'icône spécifiée en <see cref="BitmapImage" /> de la taille spécifiée.
		/// </summary>
		/// <param name="icon">Icône à convertir.</param>
		/// <param name="size">Taille en pixels de l'image retournée.</param>
		/// <returns>Image de la taille spécifiée qui représente l'icône spécifiée.</returns>
		public static BitmapImage ToBitmapImage(this Icon icon, Size size)
		{
			// Conversion de l'icône en flux
			var stream=new MemoryStream();
			ToBitmap(icon, size).Save(stream, ImageFormat.Png);
			stream.Seek(0, SeekOrigin.Begin);

			// Construction de l'image à retourner
			var image=new BitmapImage();
			image.BeginInit();
			image.StreamSource=stream;
			image.EndInit();

			return image;
		}
	}
}