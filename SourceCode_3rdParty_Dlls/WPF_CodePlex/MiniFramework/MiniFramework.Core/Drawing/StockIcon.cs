// <copyright file="StockIcon.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Drawing.StockIcon</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-05 11:16:06 +0200 (lun. 05 oct. 2009) $</date>
// <version>$Revision: 2020 $</version>

namespace MiniFramework.Drawing
{
	using System;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Media.Imaging;

	//// ========================================================================================

	/// <summary>
	/// Représente une icône standard.
	/// </summary>
	public class StockIcon
	{
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="StockIcon" />.
		/// </summary>
		/// <param name="icon">Image de type <see cref="Icon" /> représentant l'icône standard.</param>
		/// <exception cref="ArgumentNullException">L'image spécifiée est une référence null.</exception>
		public StockIcon(Icon icon)
		{
			if(icon==null) throw new ArgumentNullException("icon");

			this.Icon=icon;
			this.Name=string.Empty;
		}

		//// =====================================================================================
		
		/// <summary>
		/// Obtient l'image de type <see cref="Icon" /> correspondant à cette icône.
		/// </summary>
		/// <value><see cref="Icon" /> correspondant à cette icône</value>
		public Icon Icon
		{
			get;
			private set;
		}

		/// <summary>
		/// Obtient ou définit le nom de l'icône.
		/// </summary>
		/// <value>Nom de l'icône.</value>
		public string Name
		{
			get;
			set;
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient la grande image de type <see cref="Bitmap" /> correspondant à cette icône.
		/// </summary>
		/// <value><see cref="Bitmap" /> en version grande correspondant à cette icône.</value>
		public Bitmap LargeBitmap
		{
			get { return this.Icon.ToBitmap(32); }
		}

		/// <summary>
		/// Obtient la petite image de type <see cref="Bitmap" /> correspondant à cette icône.
		/// </summary>
		/// <value><see cref="Bitmap" /> en version petite correspondant à cette icône.</value>
		public Bitmap SmallBitmap
		{
			get { return this.Icon.ToBitmap(16); }
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient la grande image de type <see cref="BitmapImage" /> correspondant à cette icône.
		/// </summary>
		/// <value><see cref="BitmapImage" /> en version grande correspondant à cette icône.</value>
		public BitmapImage LargeBitmapImage
		{
			get { return this.Icon.ToBitmapImage(32); }
		}

		/// <summary>
		/// Obtient la petite image de type <see cref="BitmapImage" /> correspondant à cette icône.
		/// </summary>
		/// <value><see cref="BitmapImage" /> en version petite correspondant à cette icône.</value>
		public BitmapImage SmallBitmapImage
		{
			get { return this.Icon.ToBitmapImage(16); }
		}
	}
}